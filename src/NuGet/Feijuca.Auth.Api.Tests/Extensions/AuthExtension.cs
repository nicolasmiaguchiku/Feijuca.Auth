using Feijuca.Auth.Extensions;
using Feijuca.Auth.Models;
using System.IdentityModel.Tokens.Jwt;

namespace Feijuca.Auth.Api.Tests.Extensions
{
    public static class AuthExtension
    {
        public static IServiceCollection AddApiAuthentication(this IServiceCollection services, IServerSettings authSettings)
        {
            services.AddHttpContextAccessor();
            services.AddSingleton<JwtSecurityTokenHandler>();
            services.AddKeyCloakAuth(authSettings!);

            return services;
        }
    }
}
