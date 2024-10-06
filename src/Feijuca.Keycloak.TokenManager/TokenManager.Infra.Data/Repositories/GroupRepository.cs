using Flurl;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text;
using TokenManager.Common.Errors;
using TokenManager.Common.Extensions;
using TokenManager.Common.Models;
using TokenManager.Domain.Entities;
using TokenManager.Domain.Filters;
using TokenManager.Domain.Interfaces;

namespace TokenManager.Infra.Data.Repositories
{
    public class GroupRepository(IHttpClientFactory httpClientFactory, IAuthRepository authRepository) : IGroupRepository
    {
        private readonly IHttpClientFactory _httpClientFactory = httpClientFactory;
        private readonly IAuthRepository _authRepository = authRepository;

        public async Task<Result<IEnumerable<Group>>> GetAllAsync(string tenant)
        {
            var tokenDetailsResult = await _authRepository.GetAccessTokenAsync(tenant);

            if (tokenDetailsResult.IsSuccess)
            {
                var httpClient = CreateHttpClientWithHeaders(tokenDetailsResult.Response.Access_Token);

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

            return Result<IEnumerable<Group>>.Failure(tokenDetailsResult.Error);
        }

        public async Task<Result> CreateAsync(string tenant, string name, Dictionary<string, string[]> attributes)
        {
            var tokenDetails = await _authRepository.GetAccessTokenAsync(tenant);
            var httpClient = CreateHttpClientWithHeaders(tokenDetails.Response.Access_Token);

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
            var tokenDetails = await _authRepository.GetAccessTokenAsync(tenant);
            var httpClient = CreateHttpClientWithHeaders(tokenDetails.Response.Access_Token);

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

        public async Task<Result<IEnumerable<User>>> GetUsersInGroupAsync(string tenant, Guid id, UserFilters userFilters)
        {
            var tokenDetails = await _authRepository.GetAccessTokenAsync(tenant);
            var httpClient = CreateHttpClientWithHeaders(tokenDetails.Response.Access_Token);
            int first = (userFilters.PageFilter.PageNumber - 1) * userFilters.PageFilter.PageSize;

            var url = httpClient.BaseAddress
                    .AppendPathSegment("admin")
                    .AppendPathSegment("realms")
                    .AppendPathSegment(tenant)
                    .AppendPathSegment("groups")
                    .AppendPathSegment(id)
                    .AppendPathSegment("members")
                    .SetQueryParam("first", first)
                    .SetQueryParam("max", userFilters.PageFilter.PageSize)
                    .SetCollectionQueryParam("username", userFilters.Emails);

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
