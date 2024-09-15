using TokenManager.Common.Models;

namespace TokenManager.Domain.Interfaces
{
    public interface IGroupUsersRepository
    {
        Task<Result<bool>> AddUserToGroupAsync(string tenant, Guid userId, Guid groupId);
        Task<Result<bool>> RemoveUserFromGroupAsync(string tenant, Guid userId, Guid groupId);
    }
}
