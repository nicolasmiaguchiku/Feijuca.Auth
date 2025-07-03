using Feijuca.Auth.Api.Tests.Models;
using Feijuca.Auth.Extensions;
using Feijuca.Auth.Models;

namespace Feijuca.Auth.Api.Tests.Extensions;

public static class AuthExtension
{
    public static IServiceCollection AddApiAuthentication(this IServiceCollection services, IEnumerable<Realm> realms)
    {
        services.AddHttpContextAccessor();
        services.AddKeyCloakAuth(realms);
        return services;
    }
}

