using Feijuca.Auth.Common.Models;
using Feijuca.Auth.Domain.Entities;

namespace Feijuca.Auth.Domain.Interfaces
{
    public interface IRoleRepository
    {
        Task<Result<IEnumerable<Role>>> GetRolesForClientAsync(string tenant, Guid clientId);
        Task<Result<bool>> AddRoleAsync(string tenant, Guid clientId, string name, string description);
    }
}
