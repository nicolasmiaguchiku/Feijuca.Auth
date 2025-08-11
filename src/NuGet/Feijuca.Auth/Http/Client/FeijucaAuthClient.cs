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
            var result = await PostAsync<LoginUserRequest, TokenDetailsResponse>("users/login", new LoginUserRequest("hangfire@coderaw.io", "hangfire@coderaw"), cancellationToken);

            if (string.IsNullOrEmpty(result.AccessToken))
            {
                return Result<TokenDetailsResponse>.Failure(FeijucaErrors.GenerateTokenError);
            }

            return Result<TokenDetailsResponse>.Success(result);
        }

        public async Task<Result<TokenDetailsResponse>> AuthenticateUserAsync(string username, string password, CancellationToken cancellationToken)
        {
            var result = await PostAsync<LoginUserRequest, TokenDetailsResponse>("api/v1/users/login", new LoginUserRequest(username, password), cancellationToken);

            if (string.IsNullOrEmpty(result.AccessToken))
            {
                return Result<TokenDetailsResponse>.Failure(FeijucaErrors.GenerateTokenError);
            }

            return Result<TokenDetailsResponse>.Success(result);
        }


        public async Task<Result<UserResponse>> GetUserAsync(string userame, string jwtToken, CancellationToken cancellationToken)
        {
            var url = $"users?Usernames={userame}";
            var result = await GetAsync<PagedResult<UserResponse>>(url, jwtToken, cancellationToken);

            if (result.TotalResults == 0)
            {
                return Result<UserResponse>.Failure(FeijucaErrors.GetUserErrors);
            }

            var user = result?.Results.FirstOrDefault(x => x.Email == userame);

            return Result<UserResponse>.Success(user!);
        }

        public async Task<Result<PagedResult<UserResponse>>> GetUsersAsync(int maxUsers, string jwtToken, CancellationToken cancellationToken)
        {
            var url = $"users?PageFilter.Page=1&PageFilter.PageSize={maxUsers}";
            var result = await GetAsync<PagedResult<UserResponse>>(url, jwtToken, cancellationToken);

            if (result.TotalResults <= 1)
            {
                return Result<PagedResult<UserResponse>>.Failure(FeijucaErrors.GetUserErrors);
            }

            return Result<PagedResult<UserResponse>>.Success(result);
        }
    }
}
