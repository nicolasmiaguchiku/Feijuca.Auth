using Coderaw.Settings.Http;
using Coderaw.Settings.Models;
using Feijuca.Auth.Errors;
using Feijuca.Auth.Http.Requests;
using Feijuca.Auth.Http.Responses;

namespace Feijuca.Auth.Http.Client
{
    public class FeijucaAuthClient(HttpClient httpClient) : BaseHttpClient(httpClient), IFeijucaAuthClient
    {
        public async Task<Result<TokenDetailsResponse>> LoginAsync(CancellationToken cancellationToken)
        {
            var result = await PostAsync<LoginUserRequest, TokenDetailsResponse>("api/v1/users/login", new LoginUserRequest("hangfire@coderaw.io", "hangfire@coderaw"), cancellationToken);

            if (string.IsNullOrEmpty(result.AccessToken))
            {
                return Result<TokenDetailsResponse>.Failure(FeijucaErrors.GenerateTokenError);
            }

            return Result<TokenDetailsResponse>.Success(result);
        }

        public async Task<Result<KeycloakUser>> GetUserAsync(string userame, string jwtToken, CancellationToken cancellationToken)
        {
            var url = $"api/v1/users?Usernames={userame}";
            var result = await GetAsync<PagedResult<KeycloakUser>>(url, jwtToken, cancellationToken);

            if (!result?.Results?.Any() ?? false)
            {
                return Result<KeycloakUser>.Failure(FeijucaErrors.GetUserErrors);
            }

            var user = result?.Results.FirstOrDefault(x => x.Email == userame);

            return Result<KeycloakUser>.Success(user!);
        }

        public async Task<Result<PagedResult<KeycloakUser>>> GetUsersAsync(int maxUsers, string jwtToken, CancellationToken cancellationToken)
        {
            var url = $"api/v1/users?PageFilter.Page=1&PageFilter.PageSize={maxUsers}";
            var result = await GetAsync<PagedResult<KeycloakUser>>(url, jwtToken, cancellationToken);

            if (result.TotalResults <= 1)
            {
                return Result<PagedResult<KeycloakUser>>.Failure(FeijucaErrors.GetUserErrors);
            }

            return Result<PagedResult<KeycloakUser>>.Success(result);
        }
    }
}
