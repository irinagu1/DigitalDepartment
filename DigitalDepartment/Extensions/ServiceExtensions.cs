using Contracts.RepositoryCore;
using Repository.Core;
using Service.Contracts;
using Service;
using Microsoft.EntityFrameworkCore;

namespace DigitalDepartment.Extensions
{
    public static class ServiceExtensions
    {
        public static void ConfigureCors(this IServiceCollection services) =>
            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy", builder =>
                builder.AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader()
                .WithExposedHeaders("X-Pagination"));
            });

        public static void ConfigureIISIntegration(this IServiceCollection services) =>
            services.Configure<IISOptions>(options =>
            {
            });

        public static void ConfigureRepositoryManager(this IServiceCollection services) =>
            services.AddScoped<IRepositoryManager, RepositoryManager>();

        public static void ConfigureServiceManager(this IServiceCollection services) =>
            services.AddScoped<IServiceManager, ServiceManager>();

        public static void ConfigureSqlContext(this IServiceCollection services,
            IConfiguration configuration) =>
            services.AddDbContext<RepositoryContext>(opts =>
            opts.UseSqlServer(configuration.GetConnectionString("sqlConnection")));

        public static void ConfigureChecker(this IServiceCollection services) =>
            services.AddScoped<ICheckerService, CheckerService>();

        public static void ConfigureFilesFolders(this IServiceCollection services, IConfiguration configuration) 
        {
            var folder = configuration.GetValue<string>("BaseFolder");
            if (folder is null)
                throw new ArgumentNullException("No appsettings.json variable for default folder");
            services.AddSingleton<IFilesService>(new FilesService(folder));
        }

    }

}
