using Coderaw.Settings.Http;
using Coderaw.Settings.Models;
using Feijuca.Auth.Http.Responses;

namespace Feijuca.Auth.Http.Client
{
    public interface IFeijucaAuthClient
    {
        Task<Result<TokenDetailsResponse>> LoginAsync(CancellationToken cancellationToken);
        Task<Result<TokenDetailsResponse>> AuthenticateUserAsync(string username, string password, CancellationToken cancellationToken);
        Task<Result<PagedResult<UserResponse>>> GetUsersAsync(int maxUsers, string jwtToken, CancellationToken cancellationToken);
        Task<Result<UserResponse>> GetUserAsync(string userame, string jwtToken, CancellationToken cancellationToken);
    }
}
