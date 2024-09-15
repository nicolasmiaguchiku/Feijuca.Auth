
using Flurl;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Net.Http.Headers;
using System.Text;
using TokenManager.Common.Errors;
using TokenManager.Common.Models;
using TokenManager.Domain.Entities;
using TokenManager.Domain.Interfaces;

namespace TokenManager.Infra.Data.Repositories
{
    public class GroupRolesRepository(IHttpClientFactory httpClientFactory, ITokenRepository tokenRepository) : IGroupRolesRepository
    {
        private readonly IHttpClientFactory _httpClientFactory = httpClientFactory;
        private readonly ITokenRepository _tokenRepository = tokenRepository;

        public async Task<Result<bool>> AddRoleToGroupAsync(string tenant, string groupId, string clientId, string roleId, string roleName)
        {
            var tokenDetails = await _tokenRepository.GetAccessTokenAsync(tenant);
            var httpClient = CreateHttpClientWithHeaders(tokenDetails.Data.Access_Token);

            var url = httpClient.BaseAddress
                    .AppendPathSegment("admin")
                    .AppendPathSegment("realms")
                    .AppendPathSegment(tenant)
                    .AppendPathSegment("groups")
                    .AppendPathSegment(groupId)
                    .AppendPathSegment("role-mappings")
                    .AppendPathSegment("clients")
                    .AppendPathSegment(clientId);

            var roleData = new[]
            {
                new { id = roleId, name = roleName }
            };

            var content = new StringContent(JsonConvert.SerializeObject(roleData), Encoding.UTF8, "application/json");

            var response = await httpClient.PostAsync(url, content);

            if (response.IsSuccessStatusCode)
            {
                return Result<bool>.Success(true);
            }

            return Result<bool>.Failure(GroupRolesErrors.ErrorAddRoleToGroup);
        }

        public async Task<Result<IEnumerable<ClientMapping>>> GetGroupRolesAsync(string tenant, string groupId)
        {
            var tokenDetails = await _tokenRepository.GetAccessTokenAsync(tenant);
            var httpClient = CreateHttpClientWithHeaders(tokenDetails.Data.Access_Token);

            var url = httpClient.BaseAddress
                    .AppendPathSegment("admin")
                    .AppendPathSegment("realms")
                    .AppendPathSegment(tenant)
                    .AppendPathSegment("groups")
                    .AppendPathSegment(groupId)
                    .AppendPathSegment("role-mappings");

            var response = await httpClient.GetAsync(url);

            if (response.IsSuccessStatusCode)
            {
                var groupRolesContent = await response.Content.ReadAsStringAsync();
                var jsonObject = JObject.Parse(groupRolesContent);
                var clientMappings = jsonObject["clientMappings"]!
                    .Children<JProperty>()
                    .Select(x => x.Value.ToObject<ClientMapping>())
                    .ToList();

                return Result<IEnumerable<ClientMapping>>.Success(clientMappings!);
            }

            return Result<IEnumerable<ClientMapping>>.Failure(GroupRolesErrors.ErrorGetGroupRoles);
        }

        private HttpClient CreateHttpClientWithHeaders(string accessToken)
        {
            var httpClient = _httpClientFactory.CreateClient("KeycloakClient");
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
            return httpClient;
        }
    }
}
