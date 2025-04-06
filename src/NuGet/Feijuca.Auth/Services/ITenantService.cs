using Feijuca.Auth.Models;

namespace Feijuca.Auth.Services;

public interface ITenantService
{
    Tenant Tenant { get; }
    User User { get; }

    string GetInfoFromToken(string infoName);
    Tenant GetTenantFromToken();
    User GetUserFromToken();
    void SetTenant(Tenant tenant);
    void SetUser(User user);
}
