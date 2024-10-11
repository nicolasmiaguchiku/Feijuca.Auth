using Domain.Interfaces;
using Domain.Services;

using Microsoft.Extensions.DependencyInjection;

namespace Infra.CrossCutting.Extensions
{
    public static class ServicesExtensions 
    {
        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.AddScoped<ITenantService, TenantService>();
            services.AddScoped<ILoginService, LoginService>();

            return services;
        }

    }
}
