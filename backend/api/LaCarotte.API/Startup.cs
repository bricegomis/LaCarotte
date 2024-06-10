using carotte.API.Manager;
using carotte.API.Services;
using Microsoft.OpenApi.Models;
using Serilog;

namespace carotte.API
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
            var dbName = Configuration["MongoDB:DbName"];
            dbName = "LaCarotte";// TODO remove
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
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "LaCarotte.API", Version = "v1" });
            });

            if (string.IsNullOrEmpty(host)
                || string.IsNullOrEmpty(port)
                || string.IsNullOrEmpty(dbName)
                || string.IsNullOrEmpty(login)
                || string.IsNullOrEmpty(password))
            {
                throw new Exception("MongoDB configuration not found");
            }

            var loggerFactory = LoggerFactory.Create(builder => builder.AddSerilog(Log.Logger));
            var datetimeProvider = new DateTimeProvider();

            var mongoDBService = new MongoDBService(loggerFactory.CreateLogger<MongoDBService>(),
                                                    datetimeProvider,
                                                    connectionString,
                                                    dbName);
            services.AddSingleton<IMongoDBService>(mongoDBService);

            var manager = new carotteManager(loggerFactory.CreateLogger<ICarotteManager>(),
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
