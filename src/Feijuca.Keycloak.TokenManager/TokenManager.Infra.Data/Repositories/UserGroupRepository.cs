using Flurl;

using System.Net.Http;
using System.Net.Http.Headers;

using TokenManager.Common.Errors;
using TokenManager.Common.Models;
using TokenManager.Domain.Interfaces;

namespace TokenManager.Infra.Data.Repositories
{
    public class UserGroupRepository(IHttpClientFactory httpClientFactory, ITokenRepository tokenRepository) : IUserGroupRepository
    {
        private readonly IHttpClientFactory _httpClientFactory = httpClientFactory;
        private readonly ITokenRepository _tokenRepository = tokenRepository;

        public async Task<Result<bool>> AddUserToGroupAsync(string tenant, Guid userId, Guid groupId)
        {
            var tokenDetails = await _tokenRepository.GetAccessTokenAsync(tenant);
            var httpClient = CreateHttpClientWithHeaders(tokenDetails.Data.Access_Token);

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

        private HttpClient CreateHttpClientWithHeaders(string accessToken)
        {
            var httpClient = _httpClientFactory.CreateClient("KeycloakClient");
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
            return httpClient;
        }
    }
}
