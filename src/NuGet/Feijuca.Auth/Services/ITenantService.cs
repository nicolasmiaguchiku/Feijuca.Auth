namespace Feijuca.Auth.Services
{
    public interface ITenantService
    {
        string Tenant { get; }
        Guid UserId { get; }

        string GetInfoFromToken(string infoName);
        string GetTenantFromToken();
        Guid GetUserIdFromToken();
        void SetTenant(string tenantId);
        void SetUser(Guid userId);
    }
}
