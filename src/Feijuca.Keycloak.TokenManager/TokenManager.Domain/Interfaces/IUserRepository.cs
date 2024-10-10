using Common.Models;
using Domain.Entities;
using Domain.Filters;

namespace Domain.Interfaces
{
    public interface IUserRepository
    {
        Task<Result<IEnumerable<User>>> GetUsersAsync(string tenant, UserFilters userFilters);
        Task<int> GetTotalUsersAsync(string tenant);
        Task<Result<bool>> DeleteAsync(string tenant, Guid id);
        Task<Result<bool>> CreateAsync(string tenant, User user);
        Task<Result<User>> GetAsync(string tenant, string userName);
        Task<Result<bool>> ResetPasswordAsync(string tenant, Guid id, string password);
        Task<Result> SendEmailVerificationAsync(string tenant, string userId);
    }
}
