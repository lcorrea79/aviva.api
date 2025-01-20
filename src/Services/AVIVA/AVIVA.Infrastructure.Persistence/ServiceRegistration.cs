using AVIVA.Application.Interfaces;
using AVIVA.Application.Interfaces.Repositories;
using AVIVA.Infrastructure.Persistence.Connections;
using AVIVA.Infrastructure.Persistence.Contexts;
using AVIVA.Infrastructure.Persistence.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace AVIVA.Infrastructure.Persistence
{
    /// <summary>
    /// Service registration for persistence infrastructure
    /// </summary>
    public static class ServiceRegistration
    {
        /// <summary>
        /// Add persistence infrastructure services
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configuration"></param>
        public static void AddPersistenceInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            #region Repositories

            services.AddTransient(typeof(IGenericRepositoryAsync<>), typeof(GenericRepositoryAsync<>));
            services.AddTransient<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IApplicationReadDbConnection, ApplicationReadDbConnection>();
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<IOrderRepository, OrderRepository>();

            #endregion Repositories
        }
    }
}