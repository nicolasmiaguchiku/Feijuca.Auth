using Feijuca.Auth.Common.Errors;
using Mattioli.Configurations.Models;
using Feijuca.Auth.Domain.Entities;
using Feijuca.Auth.Domain.Interfaces;
using Feijuca.Auth.Services;
using Flurl;
using Newtonsoft.Json;
using System.Text;

namespace Feijuca.Auth.Infra.Data.Repositories
{
    public class ClientRoleRepository(IHttpClientFactory httpClientFactory, IAuthRepository _authRepository, ITenantService _tenantService) : BaseRepository(httpClientFactory), IClientRoleRepository
    {
        public async Task<Result<IEnumerable<Role>>> GetRolesForClientAsync(string clientId, CancellationToken cancellationToken)
        {
            var tokenDetails = await _authRepository.GetAccessTokenAsync(cancellationToken);
            using var httpClient = CreateHttpClientWithHeaders(tokenDetails.Data.Access_Token);

            var url = httpClient.BaseAddress
                    .AppendPathSegment("admin")
                    .AppendPathSegment("realms")
                    .AppendPathSegment(_tenantService.Tenant.Name)
                    .AppendPathSegment("clients")
                    .AppendPathSegment(clientId)
                    .AppendPathSegment("roles");

            using var response = await httpClient.GetAsync(url, cancellationToken);

            if (response.IsSuccessStatusCode)
            {
                var responseContent = await response.Content.ReadAsStringAsync(cancellationToken);
                var result = JsonConvert.DeserializeObject<IEnumerable<Role>>(responseContent)!;
                var defaultRoles = new List<string> { "uma_protection" };
                var rolesWithoutDefaultRoles = result.Where(role => !defaultRoles.Contains(role.Name)).ToList();
                return Result<IEnumerable<Role>>.Success(rolesWithoutDefaultRoles!);
            }

            return Result<IEnumerable<Role>>.Failure(RoleErrors.GetRoleErrors);
        }

        public async Task<Result<bool>> AddClientRoleAsync(string clientId, string name, string description, CancellationToken cancellationToken)
        {
            var tokenDetails = await _authRepository.GetAccessTokenAsync(cancellationToken);
            using var httpClient = CreateHttpClientWithHeaders(tokenDetails.Data.Access_Token);

            var url = httpClient.BaseAddress
                    .AppendPathSegment("admin")
                    .AppendPathSegment("realms")
                    .AppendPathSegment(_tenantService.Tenant.Name)
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
            using var response = await httpClient.PostAsync(url, content, cancellationToken);

            if (response.IsSuccessStatusCode)
            {
                return Result<bool>.Success(true);
            }

            return Result<bool>.Failure(RoleErrors.AddRoleErrors);
        }
    }
}
