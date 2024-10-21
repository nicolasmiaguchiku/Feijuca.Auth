using Feijuca.Auth.Models;

namespace Feijuca.Auth.Services
{
    public interface IAuthService
    {
        string GetInfoFromToken(string infoName);
        string GetTenantFromToken();
        string GetServerUrl();
        Guid GetUserIdFromToken();
    }
}
