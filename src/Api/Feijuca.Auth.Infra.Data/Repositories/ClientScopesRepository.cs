using Feijuca.Auth.Common;
using Feijuca.Auth.Domain.Entities;
using Feijuca.Auth.Domain.Interfaces;
using Flurl;
using Newtonsoft.Json;
using System.Net.Http.Json;
using System.Text;

namespace Feijuca.Auth.Infra.Data.Repositories
{
    public class ClientScopesRepository(IHttpClientFactory httpClientFactory, IAuthRepository authRepository, ITenantService tenantService)
        : BaseRepository(httpClientFactory), IClientScopesRepository
    {
        public async Task<bool> AddAudienceMapperAsync(string clientScopeId, CancellationToken cancellationToken)
        {
            var tokenDetails = await authRepository.GetAccessTokenAsync(cancellationToken);
            using var httpClient = CreateHttpClientWithHeaders(tokenDetails.Response.Access_Token);

            var url = httpClient.BaseAddress
                .AppendPathSegment("admin")
                .AppendPathSegment("realms")
                .AppendPathSegment(tenantService.Tenant)
                .AppendPathSegment("client-scopes")
                .AppendPathSegment($"{clientScopeId}")
                .AppendPathSegment($"protocol-mappers")
                .AppendPathSegment($"models");

            var audienceMapper = new
            {
                name = Constants.FeijucaApiClientName,
                protocol = "openid-connect",
                protocolMapper = "oidc-audience-mapper",
                config = new Dictionary<string, string>
                {
                    { "included.client.audience", Constants.FeijucaApiClientName },
                    { "id.token.claim", "true" },
                    { "access.token.claim", "true" },
                    { "claim.name", "aud" },
                    { "userinfo.token.claim", "false" }, 
                    { "access.token.introspection", "true" }, 
                    { "lightweight.access.token.claim", "false" }
                }
            };

            var response = await httpClient.PostAsJsonAsync(url, audienceMapper, cancellationToken: cancellationToken);

            return response.IsSuccessStatusCode;
        }

        public async Task<bool> AddClientScopesAsync(ClientScopesEntity clientScopesEntity, CancellationToken cancellationToken)
        {
            var tokenDetails = await authRepository.GetAccessTokenAsync(cancellationToken);
            using var httpClient = CreateHttpClientWithHeaders(tokenDetails.Response.Access_Token);

            var url = httpClient.BaseAddress
                .AppendPathSegment("admin")
                .AppendPathSegment("realms")
                .AppendPathSegment(tenantService.Tenant)
                .AppendPathSegment("client-scopes");

            var clientScope = new
            {
                name = clientScopesEntity.Name,
                description = clientScopesEntity.Description,
                protocol = "openid-connect",
                attributes = new Dictionary<string, bool>
                {
                    { "display.on.consent.screen", true },
                    { "include.in.token.scope", clientScopesEntity.IncludeInTokenScope }
                },
                defaultScope = true
            };

            var jsonContent = JsonConvert.SerializeObject(clientScope);
            var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");
            using var response = await httpClient.PostAsync(url, content, cancellationToken);

            if (response.IsSuccessStatusCode)
            {
                return true;
            }

            return false;
        }

        public async Task<bool> AddClientScopeToClientAsync(string clientId,
            string clientScopeId,
            bool isOptional, 
            CancellationToken cancellationToken)
        {
            var tokenDetails = await authRepository.GetAccessTokenAsync(cancellationToken);
            using var httpClient = CreateHttpClientWithHeaders(tokenDetails.Response.Access_Token);

            var url = httpClient.BaseAddress
                .AppendPathSegment("admin")
                .AppendPathSegment("realms")
                .AppendPathSegment(tenantService.Tenant)
                .AppendPathSegment("clients")
                .AppendPathSegment(clientId)
                .AppendPathSegment(isOptional ? "optional-client-scopes" : "default-client-scopes")
                .AppendPathSegment(clientScopeId);

            using var response = await httpClient.PutAsync(url, default, cancellationToken);
            if (response.IsSuccessStatusCode)
            {
                return true;
            }

            return false;
        }

        public async Task<IEnumerable<ClientScopeEntity>> GetClientScopesAsync(CancellationToken cancellationToken)
        {
            var tokenDetails = await authRepository.GetAccessTokenAsync(cancellationToken);
            using var httpClient = CreateHttpClientWithHeaders(tokenDetails.Response.Access_Token);

            var url = httpClient.BaseAddress
                .AppendPathSegment("admin")
                .AppendPathSegment("realms")
                .AppendPathSegment(tenantService.Tenant)
                .AppendPathSegment("client-scopes");

            using var response = await httpClient.GetAsync(url, cancellationToken);
            var scopesList = new List<ClientScopeEntity>();

            if (response.IsSuccessStatusCode)
            {
                var responseBody = await response.Content.ReadAsStringAsync(cancellationToken);
                var clientScopes = JsonConvert.DeserializeObject<IEnumerable<ClientScopeEntity>>(responseBody);

                return clientScopes!;
            }

            return scopesList;

        }
    }
}
