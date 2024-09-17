using Flurl;
using Newtonsoft.Json;

using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using TokenManager.Common.Errors;
using TokenManager.Common.Models;
using TokenManager.Domain.Entities;
using TokenManager.Domain.Interfaces;

namespace TokenManager.Infra.Data.Repositories
{
    public class GroupRepository(IHttpClientFactory httpClientFactory, ITokenRepository tokenRepository) : IGroupRepository
    {
        private readonly IHttpClientFactory _httpClientFactory = httpClientFactory;
        private readonly ITokenRepository _tokenRepository = tokenRepository;

        public async Task<Result<IEnumerable<Group>>> GetAllAsync(string tenant)
        {
            var tokenDetails = await _tokenRepository.GetAccessTokenAsync(tenant);
            var httpClient = CreateHttpClientWithHeaders(tokenDetails.Data.Access_Token);

            var url = httpClient.BaseAddress
                    .AppendPathSegment("admin")
                    .AppendPathSegment("realms")
                    .AppendPathSegment(tenant)
                    .AppendPathSegment("groups");

            var response = await httpClient.GetAsync(url);
            var groups = await response.Content.ReadAsStringAsync();
            var users = JsonConvert.DeserializeObject<IEnumerable<Group>>(groups)!;

            return Result<IEnumerable<Group>>.Success(users);
        }

        public async Task<Result> CreateAsync(string tenant, string name, Dictionary<string, string[]> attributes)
        {
            var tokenDetails = await _tokenRepository.GetAccessTokenAsync(tenant);
            var httpClient = CreateHttpClientWithHeaders(tokenDetails.Data.Access_Token);

            var url = httpClient.BaseAddress
                    .AppendPathSegment("admin")
                    .AppendPathSegment("realms")
                    .AppendPathSegment(tenant)
                    .AppendPathSegment("groups");

            var group = new
            {
                name,
                attributes
            };

            var jsonContent = JsonConvert.SerializeObject(group);
            var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

            var response = await httpClient.PostAsync(url, content);
            if (response.IsSuccessStatusCode)
            {
                return Result.Success();
            }

            GroupErrors.SetTechnicalMessage(response.ReasonPhrase!);
            return Result.Failure(GroupErrors.CreationGroupError);
        }

        public async Task<Result> DeleteAsync(string tenant, Guid id)
        {
            var tokenDetails = await _tokenRepository.GetAccessTokenAsync(tenant);
            var httpClient = CreateHttpClientWithHeaders(tokenDetails.Data.Access_Token);

            var url = httpClient.BaseAddress
                    .AppendPathSegment("admin")
                    .AppendPathSegment("realms")
                    .AppendPathSegment(tenant)
                    .AppendPathSegment("groups")
                    .AppendPathSegment(id);

            var response = await httpClient.DeleteAsync(url);
            if (response.IsSuccessStatusCode)
            {
                return Result.Success();
            }

            GroupErrors.SetTechnicalMessage(response.ReasonPhrase!);
            return Result.Failure(GroupErrors.DeletionGroupError);
        }

        public async Task<Result<IEnumerable<User>>> GetUsersInGroupAsync(string tenant, Guid id)
        {
            var tokenDetails = await _tokenRepository.GetAccessTokenAsync(tenant);
            var httpClient = CreateHttpClientWithHeaders(tokenDetails.Data.Access_Token);

            var url = httpClient.BaseAddress
                    .AppendPathSegment("admin")
                    .AppendPathSegment("realms")
                    .AppendPathSegment(tenant)
                    .AppendPathSegment("groups")
                    .AppendPathSegment(id)
                    .AppendPathSegment("members");

            var response = await httpClient.GetAsync(url);

            if (response.IsSuccessStatusCode)
            {
                var responseContent = await response.Content.ReadAsStringAsync();
                var users = JsonConvert.DeserializeObject<IEnumerable<User>>(responseContent);

                return Result<IEnumerable<User>>.Success(users!);
            }

            return Result<IEnumerable<User>>.Failure(GroupErrors.GetUsersInGroupsError);
        }

        private HttpClient CreateHttpClientWithHeaders(string accessToken)
        {
            var httpClient = _httpClientFactory.CreateClient("KeycloakClient");
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
            return httpClient;
        }

    }
}
