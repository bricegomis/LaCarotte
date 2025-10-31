using System.Security.Cryptography.X509Certificates;
using Ati.API.Common.Constants;
using Ati.API.Common.Controllers;
using Ati.API.Common.Extensions;
using Ati.API.Common.Models.Documents.Interfaces;
using Ati.API.Common.Services.Interfaces;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.OpenApi;
using Microsoft.OpenApi.Models;
using Raven.Client.Documents;
using Raven.Client.Documents.Conventions;
using Raven.Client.Documents.Indexes;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

// Configure OpenAPI
builder.Services.AddOpenApi(options =>
{
    options.AddDocumentTransformer(
        (document, _, _) =>
        {
            document.Info = new()
            {
                Title = "La Carotte API",
                Version = "v1"
            };
            return Task.CompletedTask;
        }
    );
    options.AddDocumentTransformer<BearerSecuritySchemeTransformer>();
});

builder
    .Services.AddAuthentication(options =>
    {
        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    })
    .AddJwtBearer(options =>
    {
        options.Authority = builder.Configuration[EnvConstants.AuthSettingsJwtIssuer];
        options.Audience = builder.Configuration[EnvConstants.AuthSettingsJwtAudience];
        options.RequireHttpsMetadata = false;
        options.TokenValidationParameters =
            new Microsoft.IdentityModel.Tokens.TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidIssuer = builder.Configuration[EnvConstants.AuthSettingsJwtIssuer],
            };
    });

builder.Services.AddControllers();

#region RavenDB
var services = builder.Services;
var ravenDbServer = builder.Configuration["RavenDBServer"];
var ravenDbName = builder.Configuration["RavenDBDbName"];
var ravenDbCertBase64 = builder.Configuration["RavenDBCertificateBase64"];
var ravenDbCertPassword = builder.Configuration["RavenDBCertificatePassword"];
if (ravenDbCertBase64 == null || ravenDbCertPassword == null || ravenDbServer == null || ravenDbName == null)
{
    throw new InvalidOperationException("RavenDB configuration is missing");
}
var certBytes = Convert.FromBase64String(ravenDbCertBase64);
var ravenDbCertificate = new X509Certificate2(certBytes, ravenDbCertPassword);
services.AddSingleton<IDocumentStore>(sp =>
{
    var store = new DocumentStore
    {
        Urls = [ravenDbServer],
        Database = ravenDbName,
        Conventions =
        {
            MaxNumberOfRequestsPerSession = 10
        },
        Certificate = ravenDbCertificate,
    };

    store.Conventions.FindCollectionName = type =>
    {
        if (type.ToString().EndsWith("Document"))
        {
            return type.Name[..^"Document".Length] + "s";
        }
        return DocumentConventions.DefaultGetCollectionName(type);
    };

    var dateTimeService = sp.GetService<IDateTimeService>();
    store.OnBeforeStore += (sender, args) =>
    {
        if (args.Entity is IDateTracked dateTracked)
        {
            // If DateCreated is the default value, it's a new document
            if (dateTracked.DateCreated == DateTimeOffset.MinValue)
            {
                dateTracked.DateCreated = dateTimeService?.GetNow() ?? DateTimeOffset.UtcNow;
            }
            dateTracked.DateModified = dateTimeService?.GetNow() ?? DateTimeOffset.UtcNow;
        }
    };

    store.Initialize();

    return store;
});
services.AddScoped(sp =>
{
    var store = sp.GetRequiredService<IDocumentStore>();
    return store.OpenAsyncSession();
});
#endregion

builder.Services.AddAttributedServices(typeof(AuthController).Assembly);
builder.Services.AddAttributedServices(typeof(Program).Assembly);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi().CacheOutput();
    app.MapScalarApiReference();
    app.MapGet("/", () => Results.Redirect("/scalar/v1")).ExcludeFromDescription();
}

app.UseAuthentication();

app.UseRouting();
app.UseAuthorization();
app.MapControllers();

app.Run();

internal sealed class BearerSecuritySchemeTransformer(
    IAuthenticationSchemeProvider authenticationSchemeProvider
) : IOpenApiDocumentTransformer
{
    public async Task TransformAsync(
        OpenApiDocument document,
        OpenApiDocumentTransformerContext context,
        CancellationToken cancellationToken
    )
    {
        var authenticationSchemes = await authenticationSchemeProvider.GetAllSchemesAsync();
        if (authenticationSchemes.Any(authScheme => authScheme.Name == "Bearer"))
        {
            var requirements = new Dictionary<string, OpenApiSecurityScheme>
            {
                ["Bearer"] = new OpenApiSecurityScheme
                {
                    Type = SecuritySchemeType.Http,
                    Scheme = "bearer", // "bearer" refers to the header name here
                    In = ParameterLocation.Header,
                    BearerFormat = "Json Web Token",
                },
            };
            document.Components ??= new OpenApiComponents();
            document.Components.SecuritySchemes = requirements;
        }
    }
}