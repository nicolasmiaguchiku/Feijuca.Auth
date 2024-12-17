using Feijuca.Auth.Common.Models;
using Feijuca.Auth.Domain.Entities;

namespace Feijuca.Auth.Domain.Interfaces
{
    public interface IGroupRolesRepository : IBaseRepository
    {
        Task<Result<bool>> AddRoleToGroupAsync(Guid groupId, Guid clientId, Guid roleId, string roleName, CancellationToken cancellationToken);
        Task<Result<IEnumerable<ClientMapping>>> GetGroupRolesAsync(Guid groupId, CancellationToken cancellationToken);
        Task<Result> RemoveRoleFromGroupAsync(Guid clientId, Guid groupId, Guid roleId, string roleName, CancellationToken cancellationToken);
    }
}
