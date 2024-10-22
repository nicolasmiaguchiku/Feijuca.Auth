using Feijuca.Auth.Common.Errors;
using Feijuca.Auth.Common.Models;
using Feijuca.Auth.Domain.Entities;
using Feijuca.Auth.Domain.Interfaces;

using Flurl;

using Newtonsoft.Json;

using System.Net.Http.Headers;

namespace Feijuca.Auth.Infra.Data.Repositories
{
    public class ClientRepository(IHttpClientFactory httpClientFactory, IAuthRepository authRepository) : IClientRepository
    {
        private readonly IHttpClientFactory _httpClientFactory = httpClientFactory;
        private readonly IAuthRepository _authRepository = authRepository;

        public async Task<Result<IEnumerable<Client>>> GetClientsAsync(string tenant, CancellationToken cancellationToken)
        {
            var tokenDetails = await _authRepository.GetAccessTokenAsync(tenant, cancellationToken);
            var httpClient = CreateHttpClientWithHeaders(tokenDetails.Response.Access_Token);

            var url = httpClient.BaseAddress
                    .AppendPathSegment("admin")
                    .AppendPathSegment("realms")
                    .AppendPathSegment(tenant)
                    .AppendPathSegment("clients");

            var response = await httpClient.GetAsync(url, cancellationToken);

            if (response.IsSuccessStatusCode)
            {
                var responseContent = await response.Content.ReadAsStringAsync(cancellationToken);
                var result = JsonConvert.DeserializeObject<IEnumerable<Client>>(responseContent)!;
                var defaultClients = new List<string> { "account", "admin-cli", "broker", "realm-management", "security-admin-console", "account-console" };
                var clientsWithoutDefaultClients = result.Where(client => !defaultClients.Contains(client.ClientId.ToString())).ToList();
                return Result<IEnumerable<Client>>.Success(clientsWithoutDefaultClients!);
            }

            return Result<IEnumerable<Client>>.Failure(ClientErrors.GetClientsErrors);
        }

        private HttpClient CreateHttpClientWithHeaders(string accessToken)
        {
            var httpClient = _httpClientFactory.CreateClient("KeycloakClient");
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
            return httpClient;
        }
    }
}
