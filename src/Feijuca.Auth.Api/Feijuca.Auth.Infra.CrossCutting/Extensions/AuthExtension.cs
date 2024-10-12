using Feijuca.Auth.Extensions;
using Feijuca.Auth.Services.Models;

using Microsoft.Extensions.DependencyInjection;

using System.IdentityModel.Tokens.Jwt;

namespace Feijuca.Auth.Infra.CrossCutting.Extensions
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
