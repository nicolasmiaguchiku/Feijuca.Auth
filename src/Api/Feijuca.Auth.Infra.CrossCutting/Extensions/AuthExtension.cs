using Feijuca.Auth.Domain.Entities;
using Feijuca.Auth.Domain.Interfaces;
using Feijuca.Auth.Extensions;
using Feijuca.Auth.Models;
using Feijuca.Auth.Services;
using Microsoft.Extensions.DependencyInjection;
using System.IdentityModel.Tokens.Jwt;

namespace Feijuca.Auth.Infra.CrossCutting.Extensions
{
    public static class AuthExtension
    {
        public static IServiceCollection AddApiAuthentication(this IServiceCollection services, out IServerSettings serverSettings)
        {
            var serviceProvider = services.BuildServiceProvider();
            var authSettingsRepository = serviceProvider.GetRequiredService<IConfigRepository>();
            serverSettings = authSettingsRepository.GetConfig();

            if (serverSettings is not null)
            {
                services.AddHttpContextAccessor();
                services.AddSingleton<JwtSecurityTokenHandler>();
                services.AddKeyCloakAuth(serverSettings!);

                serviceProvider = services.BuildServiceProvider();
                var authService = serviceProvider.GetRequiredService<IAuthService>();

                services.AddSingleton(new TokenCredentials()
                {
                    Client_Secret = authService.GetClientSecret(),
                    Client_Id = authService.GetClientId(),
                    ServerUrl = authService.GetServerUrl()
                });

                return services;
            }

            services.AddSingleton(new TokenCredentials());
            return services;
        }
    }
}
