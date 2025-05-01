using Feijuca.Auth.Api.Tests.Models;
using Feijuca.Auth.Extensions;

namespace Feijuca.Auth.Api.Tests.Extensions;

public static class AuthExtension
{
    public static IServiceCollection AddApiAuthentication(this IServiceCollection services, Settings authSettings)
    {
        services.AddHttpContextAccessor();
        services.AddKeyCloakAuth(authSettings.KeycloakSettings.Client, authSettings.KeycloakSettings.ServerSettings, authSettings.KeycloakSettings.Realms);
        return services;
    }
}

