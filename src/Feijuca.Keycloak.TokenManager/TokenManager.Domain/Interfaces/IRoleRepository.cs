using TokenManager.Common.Models;
using TokenManager.Domain.Entities;

namespace TokenManager.Domain.Interfaces
{
    public interface IRoleRepository
    {
        Task<Result<IEnumerable<Role>>> GetRolesForClientAsync(string tenant, string clientId);
        Task<Result<bool>> AddRoleAsync(string tenant, string clientId, string name, string description);
    }
}
