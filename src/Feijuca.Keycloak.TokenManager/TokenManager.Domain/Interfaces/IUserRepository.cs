using TokenManager.Common.Models;
using TokenManager.Domain.Entities;

namespace TokenManager.Domain.Interfaces
{
    public interface IUserRepository
    {
        Task<Result<TokenDetails>> LoginAsync(string tenant, User user);
        Task<Result<TokenDetails>> RefreshTokenAsync(string tenant, string refreshToken);
        Task<Result<IEnumerable<User>>> GetAllAsync(string tenant);
        Task<Result<bool>> DeleteAsync(string tenant, Guid id);
        Task<(bool result, string content)> CreateAsync(string tenant, User user);
        Task<Result<User>> GetAsync(string tenant, string userName);
        Task<Result> ResetPasswordAsync(string tenant, string userId, string password);
        Task<Result> SendEmailVerificationAsync(string tenant, string userId);
    }
}
