using Feijuca.Auth.Common.Models;
using Feijuca.Auth.Domain.Entities;
using Feijuca.Auth.Domain.Filters;

namespace Feijuca.Auth.Domain.Interfaces
{
    public interface IUserRepository
    {
        Task<Result<IEnumerable<User>>> GetAllAsync(UserFilters userFilters, CancellationToken cancellationToken);
        Task<int> GetTotalAsync(CancellationToken cancellationToken);
        Task<Result<bool>> DeleteAsync(Guid id, CancellationToken cancellationToken);
        Task<Result<bool>> CreateAsync(User user, CancellationToken cancellationToken);
        Task<Result<bool>> ResetPasswordAsync(Guid id, string password, CancellationToken cancellationToken);
        Task<Result<bool>> RevokeSessionsAsync(Guid id, CancellationToken cancellationToken);
        Task<Result<bool>> AddUserAttributesAsync(Guid id, Dictionary<string, string[]> attributes, CancellationToken cancellationToken);
        Task<Result<User>> GetAsync(string username, CancellationToken cancellationToken);
        Task<Result> SendEmailVerificationAsync(string userId, CancellationToken cancellationToken);
        Task<Result<TokenDetails>> LoginAsync(string username, string password, CancellationToken cancellationToken);
        Task<Result<bool>> SignoutAsync(string refreshToken, CancellationToken cancellationToken);
        Task<Result<TokenDetails>> RefreshTokenAsync(string refreshToken, CancellationToken cancellationToken);
    }
}
