using AVIVA.Application.Behaviours;
using AVIVA.Application.Helpers;
using AVIVA.Application.Interfaces;
using AVIVA.Application.Models;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace AVIVA.Application
{
    /// <summary>
    /// Service extensions
    /// </summary>
    public static class ServiceExtensions
    {
        /// <summary>
        /// Add application layer services
        /// </summary>
        /// <param name="services"></param>
        public static void AddApplicationLayer(this IServiceCollection services)
        {
            services.AddAutoMapper(Assembly.GetExecutingAssembly());

            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(Assembly.GetExecutingAssembly()));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
            services.AddScoped<IModelHelper, ModelHelper>();
            services.AddScoped<PaymentProviderSelector>();

        }
    }
}