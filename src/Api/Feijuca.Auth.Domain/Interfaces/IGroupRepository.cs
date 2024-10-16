using Feijuca.Auth.Common.Models;
using Feijuca.Auth.Domain.Entities;
using Feijuca.Auth.Domain.Filters;

namespace Feijuca.Auth.Domain.Interfaces
{
    public interface IGroupRepository
    {
        Task<Result<IEnumerable<Group>>> GetAllAsync(string tenant);
        Task<Result> CreateAsync(string tenant, string name, Dictionary<string, string[]> attributes);
        Task<Result> DeleteAsync(string tenant, Guid id);
        Task<Result<IEnumerable<User>>> GetUsersInGroupAsync(string tenant, Guid id, UserFilters userFilters);
    }
}
