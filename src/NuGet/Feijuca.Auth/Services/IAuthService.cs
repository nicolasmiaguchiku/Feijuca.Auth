using Feijuca.Auth.Models;

namespace Feijuca.Auth.Services
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
