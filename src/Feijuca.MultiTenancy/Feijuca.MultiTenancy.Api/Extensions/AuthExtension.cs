using Feijuca.MultiTenancy.Extensions;
using Feijuca.MultiTenancy.Services.Models;
using System.IdentityModel.Tokens.Jwt;

namespace Feijuca.MultiTenancy.Api.Extensions
{
    public static class AuthExtension
    {
        public static IServiceCollection AddApiAuthentication(this IServiceCollection services, AuthSettings authSettings)
        {
            services.AddHttpContextAccessor();
            services.AddSingleton<JwtSecurityTokenHandler>();
            services.AddKeyCloakAuth(authSettings!);

            return services;
        }
    }
}
