using Domain.Interfaces;

namespace Domain.Services
{
    public class TenantService : ITenantService
    {
        private string _tenantId = null!;

        public string Tenant => _tenantId;

        public void SetTenant(string tenantId)
        {
            _tenantId = tenantId;
        }
    }

}
