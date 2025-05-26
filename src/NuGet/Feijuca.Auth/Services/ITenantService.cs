using Feijuca.Auth.Models;

namespace Feijuca.Auth.Services;

public interface ITenantService
{
    IEnumerable<Tenant> Tenants { get; }

    Tenant Tenant { get; }

    User User { get; }

    string GetInfo(string infoName);

    IEnumerable<Tenant> GetTenants();

    Tenant GetTenant();

    void SetTenant(Tenant tenant);

    internal User GetUser();

    string GetToken();

    void SetTenants(IEnumerable<Tenant> tenants);

    void SetUser(User user);
}
