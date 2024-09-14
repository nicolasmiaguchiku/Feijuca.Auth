
using TokenManager.Common.Models;
using TokenManager.Domain.Interfaces;

namespace TokenManager.Infra.Data.Repositories
{
    public class RoleRepository : IRoleRepository
    {
        public Task<Result> GetRolesForClientAsync(string tenant, string clientId)
        {
            throw new NotImplementedException();
        }
    }
}
