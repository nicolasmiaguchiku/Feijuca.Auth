namespace Feijuca.Auth.Domain.Interfaces
{
    public interface ITenantService
    {
        string Tenant { get; }

        void SetTenant(string tenantId);
    }

}
