using Feijuca.Auth.Common.Models;
using Feijuca.Auth.Domain.Entities;
using Feijuca.Auth.Domain.Filters;

namespace Feijuca.Auth.Domain.Interfaces
{
    public interface IUserRepository
    {
        Task<Result<IEnumerable<User>>> GetAllAsync(UserFilters userFilters);
        Task<int> GetTotalAsync();
        Task<Result<bool>> DeleteAsync(Guid id);
        Task<Result<bool>> CreateAsync(User user);
        Task<Result<bool>> ResetPasswordAsync(Guid id, string password);
        Task<bool> RevokeSessionsAsync(Guid id);
        Task<Result<User>> GetAsync(string username);
        Task<Result> SendEmailVerificationAsync(string userId);
        Task<Result<TokenDetails>> LoginAsync(string user, string password);
        Task<Result<bool>> SignoutAsync(string refreshToken);
        Task<Result<TokenDetails>> RefreshTokenAsync(string refreshToken);
    }
}
