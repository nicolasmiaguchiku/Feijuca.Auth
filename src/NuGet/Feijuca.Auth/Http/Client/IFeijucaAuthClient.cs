using Coderaw.Settings.Http;
using Coderaw.Settings.Models;
using Feijuca.Auth.Http.Responses;

namespace Feijuca.Auth.Http.Client
{
    public interface IFeijucaAuthClient
    {
        Task<Result<TokenDetailsResponse>> LoginAsync(CancellationToken cancellationToken);
        Task<Result<PagedResult<KeycloakUser>>> GetUsersAsync(int maxUsers, string jwtToken, CancellationToken cancellationToken);
        Task<Result<KeycloakUser>> GetUserAsync(string userame, string jwtToken, CancellationToken cancellationToken);
    }
}
