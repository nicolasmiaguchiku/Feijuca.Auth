
using Flurl;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using TokenManager.Common.Errors;
using TokenManager.Common.Models;
using TokenManager.Domain.Entities;
using TokenManager.Domain.Interfaces;

namespace TokenManager.Infra.Data.Repositories
{
    public class ClientRepository(IHttpClientFactory httpClientFactory, ITokenRepository tokenRepository) : IClientRepository
    {
        private readonly IHttpClientFactory _httpClientFactory = httpClientFactory;
        private readonly ITokenRepository _tokenRepository = tokenRepository;

        public async Task<Result<IEnumerable<Client>>> GetClientsAsync(string tenant)
        {
            var tokenDetails = await _tokenRepository.GetAccessTokenAsync(tenant);
            var httpClient = CreateHttpClientWithHeaders(tokenDetails.Data.Access_Token);

            var url = httpClient.BaseAddress
                    .AppendPathSegment("admin")
                    .AppendPathSegment("realms")
                    .AppendPathSegment(tenant)
                    .AppendPathSegment("clients");

            var response = await httpClient.GetAsync(url);

            if (response.IsSuccessStatusCode)
            {
                var responseContent = await response.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<IEnumerable<Client>>(responseContent);

                return Result<IEnumerable<Client>>.Success(result!);
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
