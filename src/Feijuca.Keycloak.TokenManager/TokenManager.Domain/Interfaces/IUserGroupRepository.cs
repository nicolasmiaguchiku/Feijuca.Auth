using TokenManager.Common.Models;

namespace TokenManager.Domain.Interfaces
{
    public interface IUserGroupRepository
    {
        Task<Result<bool>> AddUserToGroupAsync(string tenant, Guid userId, Guid groupId);
    }
}
