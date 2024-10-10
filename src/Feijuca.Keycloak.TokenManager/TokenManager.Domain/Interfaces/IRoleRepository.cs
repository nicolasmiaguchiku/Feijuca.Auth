using Common.Models;
using Domain.Entities;

namespace Domain.Interfaces
{
    public interface IRoleRepository
    {
        Task<Result<IEnumerable<Role>>> GetRolesForClientAsync(string tenant, Guid clientId);
        Task<Result<bool>> AddRoleAsync(string tenant, Guid clientId, string name, string description);
    }
}
