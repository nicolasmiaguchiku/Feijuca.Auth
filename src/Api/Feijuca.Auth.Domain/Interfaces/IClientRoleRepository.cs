using Feijuca.Auth.Common.Models;
using Feijuca.Auth.Domain.Entities;

namespace Feijuca.Auth.Domain.Interfaces
{
    public interface IClientRoleRepository : IBaseRepository
    {
        Task<Result<IEnumerable<Role>>> GetRolesForClientAsync(string clientId, CancellationToken cancellationToken);
        Task<Result<bool>> AddClientRoleAsync(string clientId, string name, string description, CancellationToken cancellationToken);
    }
}
