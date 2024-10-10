using Common.Errors;
using Common.Models;
using Domain.Interfaces;
using Flurl;
using System.Net.Http.Headers;

namespace Infra.Data.Repositories
{
    public class UserGroupRepository(IHttpClientFactory httpClientFactory, IAuthRepository authRepository) : IGroupUsersRepository
    {
        private readonly IHttpClientFactory _httpClientFactory = httpClientFactory;
        private readonly IAuthRepository _authRepository = authRepository;

        public async Task<Result<bool>> AddUserToGroupAsync(string tenant, Guid userId, Guid groupId)
        {
            var tokenDetails = await _authRepository.GetAccessTokenAsync(tenant);
            var httpClient = CreateHttpClientWithHeaders(tokenDetails.Response.Access_Token);

            var url = httpClient.BaseAddress
                    .AppendPathSegment("admin")
                    .AppendPathSegment("realms")
                    .AppendPathSegment(tenant)
                    .AppendPathSegment("users")
                    .AppendPathSegment(userId)
                    .AppendPathSegment("groups")
                    .AppendPathSegment(groupId);

            var response = await httpClient.PutAsync(url, default);

            if (response.IsSuccessStatusCode)
            {
                return Result<bool>.Success(true);
            }

            return Result<bool>.Failure(UserGroupErrors.ErrorAddUserToGroup);
        }

        public async Task<Result<bool>> RemoveUserFromGroupAsync(string tenant, Guid userId, Guid groupId)
        {
            var tokenDetails = await _authRepository.GetAccessTokenAsync(tenant);
            var httpClient = CreateHttpClientWithHeaders(tokenDetails.Response.Access_Token);

            var url = httpClient.BaseAddress
                    .AppendPathSegment("admin")
                    .AppendPathSegment("realms")
                    .AppendPathSegment(tenant)
                    .AppendPathSegment("users")
                    .AppendPathSegment(userId)
                    .AppendPathSegment("groups")
                    .AppendPathSegment(groupId);

            var response = await httpClient.DeleteAsync(url, default);

            if (response.IsSuccessStatusCode)
            {
                return Result<bool>.Success(true);
            }

            return Result<bool>.Failure(UserGroupErrors.ErrorAddUserToGroup);
        }

        private HttpClient CreateHttpClientWithHeaders(string accessToken)
        {
            var httpClient = _httpClientFactory.CreateClient("KeycloakClient");
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
            return httpClient;
        }
    }
}
