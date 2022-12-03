using Application.Common.Error;
using Application.Common.Interface;
using Infrastructure.LogCapture;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddPersistence(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ApplicationDBContext>(options =>
               options.UseSqlServer(configuration.GetConnectionString("AppDatabase"),
               b => b.MigrationsAssembly(typeof(ApplicationDBContext).Assembly.FullName)), ServiceLifetime.Scoped);
            services.AddScoped<IApplicationDBContext>(provider => provider.GetService<ApplicationDBContext>());
            services.AddScoped(typeof(IRepository<>), typeof(EfRepository<>));
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddSingleton<IErrorMessageLog, ErrorMessageLog>();
            //services.AddScoped<IConfigurationExtension, ConfigurationExtensions>();
            //services.AddScoped<IBlobStorageService, BlobStorageService>();

            return services;
        }
    }
}
