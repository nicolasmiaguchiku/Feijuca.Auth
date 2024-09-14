using TokenManager.Common.Models;
using TokenManager.Domain.Entities;

namespace TokenManager.Domain.Interfaces
{
    public interface IUserRepository
    {
        Task<Result<TokenDetails>> LoginAsync(string tenant, User user);
        Task<Result<TokenDetails>> RefreshTokenAsync(string tenant, string refreshToken);
        Task<Result<IEnumerable<User>>> GetAllUsers(string tenant, string tokenAccess);
        Task<(bool result, string content)> CreateNewUserAsync(string tenant, User user, string accessToken);
        Task<Result<User>> GetUserAsync(string tenant, string userName, string accessToken);
        Task<Result> ResetPasswordAsync(string tenant, string userId, string password, string accessToken);
        Task<Result> SendEmailVerificationAsync(string tenant, string userId, string accessToken);
    }
}
