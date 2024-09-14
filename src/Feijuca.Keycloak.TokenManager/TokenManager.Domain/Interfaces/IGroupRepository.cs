using TokenManager.Common.Models;
using TokenManager.Domain.Entities;

namespace TokenManager.Domain.Interfaces
{
    public interface IGroupRepository
    {
        Task<Result<IEnumerable<Group>>> GetAllGroups(string tenant);
        Task<Result> CreateGroupAsync(string tenant, string name, Dictionary<string, string[]> attributes);
    }
}
