
using Common.Errors;
using Common.Models;
using Domain.Entities;
using Domain.Interfaces;
using Flurl;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text;

namespace Infra.Data.Repositories
{
    public class RoleRepository(IHttpClientFactory httpClientFactory, IAuthRepository authRepository) : IRoleRepository
    {
        private readonly IHttpClientFactory _httpClientFactory = httpClientFactory;
        private readonly IAuthRepository _authRepository = authRepository;

        public async Task<Result<IEnumerable<Role>>> GetRolesForClientAsync(string tenant, Guid clientId)
        {
            var tokenDetails = await _authRepository.GetAccessTokenAsync(tenant);
            var httpClient = CreateHttpClientWithHeaders(tokenDetails.Response.Access_Token);

            var url = httpClient.BaseAddress
                    .AppendPathSegment("admin")
                    .AppendPathSegment("realms")
                    .AppendPathSegment(tenant)
                    .AppendPathSegment("clients")
                    .AppendPathSegment(clientId)
                    .AppendPathSegment("roles");

            var response = await httpClient.GetAsync(url);

            if (response.IsSuccessStatusCode)
            {
                var responseContent = await response.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<IEnumerable<Role>>(responseContent)!;
                var defaultRoles = new List<string> { "uma_protection" };
                var rolesWithoutDefaultRoles = result.Where(role => !defaultRoles.Contains(role.Name)).ToList();
                return Result<IEnumerable<Role>>.Success(rolesWithoutDefaultRoles!);
            }

            return Result<IEnumerable<Role>>.Failure(RoleErrors.GetRoleErrors);
        }


        public async Task<Result<bool>> AddRoleAsync(string tenant, Guid clientId, string name, string description)
        {
            var tokenDetails = await _authRepository.GetAccessTokenAsync(tenant);
            var httpClient = CreateHttpClientWithHeaders(tokenDetails.Response.Access_Token);

            var url = httpClient.BaseAddress
                    .AppendPathSegment("admin")
                    .AppendPathSegment("realms")
                    .AppendPathSegment(tenant)
                    .AppendPathSegment("clients")
                    .AppendPathSegment(clientId)
                    .AppendPathSegment("roles");

            var roleData = new
            {
                name,
                description,
                composite = false
            };

            var content = new StringContent(JsonConvert.SerializeObject(roleData), Encoding.UTF8, "application/json");
            var response = await httpClient.PostAsync(url, content);

            if (response.IsSuccessStatusCode)
            {
                return Result<bool>.Success(true);
            }

            return Result<bool>.Failure(RoleErrors.AddRoleErrors);
        }

        private HttpClient CreateHttpClientWithHeaders(string accessToken)
        {
            var httpClient = _httpClientFactory.CreateClient("KeycloakClient");
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
            return httpClient;
        }
    }
}
