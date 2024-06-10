using DoList.API.Manager;
using DoList.API.Services;
using Microsoft.OpenApi.Models;
using Serilog;

namespace DoList.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            // Init configuration
            var host = Configuration["MongoDB:Host"];
            var port = Configuration["MongoDB:Port"];
            var login = Configuration["MongoDB:Login"];
            var password = Configuration["MongoDB:Password"];
            var connectionString = $"mongodb://{login}:{password}@{host}:{port}/";

            services.AddCors(options =>
            {
                options.AddDefaultPolicy(builder =>
                {
                    builder.AllowAnyOrigin()
                           .AllowAnyMethod()
                           .AllowAnyHeader();
                });
            });


            services.AddControllers();
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "DoList.API", Version = "v1" });
            });

            if (string.IsNullOrEmpty(host) || string.IsNullOrEmpty(port) || string.IsNullOrEmpty(login) || string.IsNullOrEmpty(password))
            {
                throw new Exception("MongoDB credentials not configured");
            }

            var loggerFactory = LoggerFactory.Create(builder => builder.AddSerilog(Log.Logger));
            var datetimeProvider = new DateTimeProvider();

            var mongoDBService = new MongoDBService(loggerFactory.CreateLogger<MongoDBService>(), datetimeProvider, connectionString, "DoList");
            services.AddSingleton<IMongoDBService>(mongoDBService);
            
            var manager = new DoListManager(loggerFactory.CreateLogger<IDoListManager>(),
                                            mongoDBService,
                                            datetimeProvider);
            services.AddSingleton<IDoListManager>(manager);
            services.AddSingleton<IHostedService>(provider => manager);
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "DoList.API v1"));
            }

            //app.UseHttpsRedirection();
            app.UseCors();
            //app.UseAuthorization();

            var options = new DefaultFilesOptions();
            options.DefaultFileNames.Clear();
            options.DefaultFileNames.Add("index.html");
            options.DefaultFileNames.Add("index.htm");
            app.UseDefaultFiles(options);
            app.UseStaticFiles();

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
