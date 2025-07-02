
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace FinanzasPersonales.Infrastructure
{
   public static class ConfigurationInfrastructure
    {

        /// <summary>
        /// Configura Entity Framework Core
        /// </summary>
        public static IServiceCollection AddDatabaseConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<FinaciasPersonales>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("FinanciasPersonales"));
            });

            return services;
        }
    }
}
