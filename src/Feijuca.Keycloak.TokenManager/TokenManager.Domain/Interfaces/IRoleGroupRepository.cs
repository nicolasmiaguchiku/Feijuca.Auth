using TokenManager.Common.Models;

namespace TokenManager.Domain.Interfaces
{
    public interface IRoleGroupRepository
    {
        Task<Result<bool>> AddRoleToGroupAsync(string tenant, string groupId, string clientId, string roleId, string roleName);
    }
}
