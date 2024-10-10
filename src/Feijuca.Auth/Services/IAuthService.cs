using Feijuca.MultiTenancy.Services.Models;

namespace Feijuca.MultiTenancy.Services
{
    public interface IAuthService
    {
        string GetInfoFromToken(string infoName);
        string GetTenantFromToken();
        string GetClientId();
        string GetClientSecret();
        string GetServerUrl();
        Realm GetRealm(string realmName);
        Guid GetUserIdFromToken();
    }
}
