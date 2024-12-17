using Feijuca.Auth.Common.Models;
using Feijuca.Auth.Domain.Entities;

namespace Feijuca.Auth.Domain.Interfaces
{
    public interface IRoleRepository : IBaseRepository
    {
        Task<Result<IEnumerable<Role>>> GetRolesForClientAsync(Guid clientId, CancellationToken cancellationToken);
        Task<Result<bool>> AddRoleAsync(Guid clientId, string name, string description, CancellationToken cancellationToken);
    }
}
