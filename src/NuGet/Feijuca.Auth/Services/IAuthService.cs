namespace Feijuca.Auth.Services
{
    public interface IAuthService
    {
        string GetInfoFromToken(string infoName);
        string GetTenantFromToken();
        Guid GetUserIdFromToken();
    }
}
