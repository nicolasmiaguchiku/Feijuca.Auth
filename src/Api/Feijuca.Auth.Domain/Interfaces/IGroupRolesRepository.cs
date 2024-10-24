using Feijuca.Auth.Common.Models;
using Feijuca.Auth.Domain.Entities;

namespace Feijuca.Auth.Domain.Interfaces
{
    public interface IGroupRolesRepository
    {
        Task<Result<bool>> AddRoleToGroupAsync(Guid groupId, Guid clientId, Guid roleId, string roleName, CancellationToken cancellation);
        Task<Result<IEnumerable<ClientMapping>>> GetGroupRolesAsync(Guid groupId, CancellationToken cancellation);
        Task<Result> RemoveRoleFromGroupAsync(Guid clientId, Guid groupId, Guid roleId, string roleName, CancellationToken cancellation);
    }
}
