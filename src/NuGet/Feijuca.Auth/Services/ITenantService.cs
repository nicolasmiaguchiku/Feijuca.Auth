using Feijuca.Auth.Models;

namespace Feijuca.Auth.Services;

public interface ITenantService
{
    IEnumerable<Tenant> Tenants { get; }

    User User { get; }

    string GetInfoFromToken(string infoName);

    IEnumerable<Tenant> GetTenantsFromToken();

    User GetUserFromToken();

    void SetTenants(IEnumerable<Tenant> tenants);

    void SetUser(User user);
}
