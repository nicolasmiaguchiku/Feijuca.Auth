using TokenManager.Common.Models;
using TokenManager.Domain.Entities;

namespace TokenManager.Domain.Interfaces
{
    public interface IGroupRepository
    {
        Task<Result<IEnumerable<Group>>> GetAllAsync(string tenant);
        Task<Result> CreateAsync(string tenant, string name, Dictionary<string, string[]> attributes);
        Task<Result> DeleteAsync(string tenant, Guid id);
    }
}
