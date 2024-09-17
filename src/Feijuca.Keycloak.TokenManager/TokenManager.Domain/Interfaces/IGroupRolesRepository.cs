using TokenManager.Common.Models;
using TokenManager.Domain.Entities;

namespace TokenManager.Domain.Interfaces
{
    public interface IGroupRolesRepository
    {
        Task<Result<bool>> AddRoleToGroupAsync(string tenant, string groupId, string clientId, string roleId, string roleName);
        Task<Result<IEnumerable<ClientMapping>>> GetGroupRolesAsync(string tenant, string groupId);
        Task<Result> RemoveRoleFromGroupAsync(string tenant, string clientId, string groupId, string roleId);
    }
}
