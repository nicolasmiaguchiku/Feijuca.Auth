using TokenManager.Domain.Entities;

namespace TokenManager.Domain.Interfaces
{
    public interface IGroupRepository
    {
        Task<Result> CreateGroupAsync(string tenant, string groupName, Dictionary<string, string[]> attributes);
    }
}
