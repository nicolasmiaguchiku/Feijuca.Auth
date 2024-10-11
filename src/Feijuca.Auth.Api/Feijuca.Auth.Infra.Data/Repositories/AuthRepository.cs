using Common.Errors;
using Common.Models;
using Domain.Entities;
using Domain.Interfaces;
using Flurl;
using Newtonsoft.Json;

namespace Infra.Data.Repositories
{
    public class AuthRepository(TokenCredentials tokenCredentials, IHttpClientFactory httpClientFactory) : IAuthRepository
    {
        private readonly TokenCredentials _tokenCredentials = tokenCredentials;
        private readonly IHttpClientFactory _httpClientFactory = httpClientFactory;

        public async Task<Result<TokenDetails>> GetAccessTokenAsync(string tenant)
        {
            var requestData = new FormUrlEncodedContent(
            [
                new KeyValuePair<string, string>("grant_type", "client_credentials"),
                new KeyValuePair<string, string>("client_id", _tokenCredentials.Client_Id),
                new KeyValuePair<string, string>("client_secret", _tokenCredentials.Client_Secret),
            ]);

            var httpClient = _httpClientFactory.CreateClient("KeycloakClient");
            var url = httpClient.BaseAddress
                .AppendPathSegment("realms")
                .AppendPathSegment(tenant)
                .AppendPathSegment("protocol")
                .AppendPathSegment("openid-connect")
                .AppendPathSegment("token");

            var response = await httpClient.PostAsync(url, requestData);

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<TokenDetails>(content)!;

                return Result<TokenDetails>.Success(result);
            }

            var responseMessage = await response.Content.ReadAsStringAsync();
            UserErrors.SetTechnicalMessage(responseMessage);
            return Result<TokenDetails>.Failure(UserErrors.TokenGenerationError);
        }
    }
}
