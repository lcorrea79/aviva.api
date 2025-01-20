using AVIVA.Application.Interfaces;
using AVIVA.Infrastructure.Shared.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace AVIVA.Infrastructure.Shared
{
    public static class ServiceRegistration
    {
        public static void AddSharedInfrastructure(this IServiceCollection services, IConfiguration _config)
        {
            services.AddTransient<IDateTimeService, DateTimeService>();
            services.AddTransient<IMockService, MockService>();
            services.AddHttpClient<IPaymentProviderService, PaymentProviderService>();

        }
    }
}