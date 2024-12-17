using Feijuca.Auth.Common.Errors;
using Feijuca.Auth.Common.Models;
using Feijuca.Auth.Domain.Entities;
using Feijuca.Auth.Domain.Interfaces;
using Flurl;
using Newtonsoft.Json;

namespace Feijuca.Auth.Infra.Data.Repositories
{
    public class ClientRepository(IHttpClientFactory httpClientFactory, IAuthRepository authRepository, ITenantService tenantService) : BaseRepository(httpClientFactory), IClientRepository
    {
        private readonly IAuthRepository _authRepository = authRepository;
        private readonly ITenantService _tenantService = tenantService;

        public async Task<Result<IEnumerable<Client>>> GetClientsAsync(CancellationToken cancellationToken)
        {
            var tokenDetails = await _authRepository.GetAccessTokenAsync(cancellationToken);
            var httpClient = CreateHttpClientWithHeaders(tokenDetails.Response.Access_Token);

            var url = httpClient.BaseAddress
                    .AppendPathSegment("admin")
                    .AppendPathSegment("realms")
                    .AppendPathSegment(_tenantService.Tenant)
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
    }
}
