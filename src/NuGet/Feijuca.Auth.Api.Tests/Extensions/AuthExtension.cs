using Coderaw.Settings.Models;

using Feijuca.Auth.Api.Tests.Models;
using Feijuca.Auth.Extensions;
using System.IdentityModel.Tokens.Jwt;

namespace Feijuca.Auth.Api.Tests.Extensions;

public static class AuthExtension
{
    public static IServiceCollection AddApiAuthentication(this IServiceCollection services, Settings authSettings)
    {
        services.AddHttpContextAccessor();
        services.AddSingleton<JwtSecurityTokenHandler>();
        services.AddKeyCloakAuth(authSettings.KeycloakSettings.Client, authSettings.KeycloakSettings.ServerSettings, authSettings.KeycloakSettings.Realms);
        return services;
    }
}

