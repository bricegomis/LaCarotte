using LaCarotte.API.Manager;
using LaCarotte.API.Services;
using Microsoft.OpenApi.Models;
using Serilog;

namespace LaCarotte.API
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
            var mongoDbConnectionString = Configuration["MongoDB_ConnectionString"];
            var dbName = Configuration["MongoDB_DbName"];

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
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "LaCarotte.API", Version = "v1" });
            });

            if (string.IsNullOrEmpty(mongoDbConnectionString)
                || string.IsNullOrEmpty(dbName))
            {
                throw new Exception("MongoDB Connection string & DbName not found");
            }

            var loggerFactory = LoggerFactory.Create(builder => builder.AddSerilog(Log.Logger));
            var datetimeProvider = new DateTimeProvider();

            var mongoDBService = new MongoDBService(loggerFactory.CreateLogger<MongoDBService>(),
                                                    datetimeProvider,
                                                    mongoDbConnectionString,
                                                    dbName);
            services.AddSingleton<IMongoDBService>(mongoDBService);

            var manager = new CarotteManager(loggerFactory.CreateLogger<ICarotteManager>(),
                                            mongoDBService,
                                            datetimeProvider);
            services.AddSingleton<ICarotteManager>(manager);
            services.AddSingleton<IHostedService>(provider => manager);

            
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "LaCarotte.API v1"));
            }

            app.UseCors();

            var options = new DefaultFilesOptions();
            options.DefaultFileNames.Clear();
            options.DefaultFileNames.Add("index.html");
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
