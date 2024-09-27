using Flurl;
using Newtonsoft.Json;
using TokenManager.Common.Errors;
using TokenManager.Common.Models;
using TokenManager.Domain.Entities;
using TokenManager.Domain.Interfaces;

namespace TokenManager.Infra.Data.Repositories
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


        public async Task<Result<TokenDetails>> LoginAsync(string tenant, User user)
        {
            var httpClient = _httpClientFactory.CreateClient("KeycloakClient");
            var urlGetToken = httpClient.BaseAddress.AppendPathSegment("realms")
                .AppendPathSegment(tenant)
                .AppendPathSegment("protocol")
                .AppendPathSegment("openid-connect")
                .AppendPathSegment("token");

            var requestData = new FormUrlEncodedContent(
            [
                new KeyValuePair<string, string>("grant_type", "password"),
                new KeyValuePair<string, string>("client_id", _tokenCredentials.Client_Id),
                new KeyValuePair<string, string>("client_secret", _tokenCredentials.Client_Secret),
                new KeyValuePair<string, string>("username", user.Username!),
                new KeyValuePair<string, string>("password", user.Password!)
            ]);

            var response = await httpClient.PostAsync(urlGetToken, requestData);

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<TokenDetails>(content);
                return Result<TokenDetails>.Success(result!);
            }

            var responseMessage = await response.Content.ReadAsStringAsync();
            UserErrors.SetTechnicalMessage(responseMessage);
            return Result<TokenDetails>.Failure(UserErrors.InvalidUserNameOrPasswordError);
        }

        public async Task<Result<bool>> SignoutAsync(string tenant, string refreshToken)
        {
            var httpClient = _httpClientFactory.CreateClient("KeycloakClient");

            var urlGetToken = httpClient.BaseAddress.AppendPathSegment("realms")
                .AppendPathSegment(tenant)
                .AppendPathSegment("protocol")
                .AppendPathSegment("openid-connect")
                .AppendPathSegment("logout");

            var requestData = new FormUrlEncodedContent(
            [
                new KeyValuePair<string, string>("client_id", _tokenCredentials.Client_Id),
                new KeyValuePair<string, string>("refresh_token", refreshToken),
                new KeyValuePair<string, string>("client_secret", _tokenCredentials.Client_Secret)
            ]);

            var response = await httpClient.PostAsync(urlGetToken, requestData);

            if (response.IsSuccessStatusCode)
            {
                return Result<bool>.Success(true);
            }

            return Result<bool>.Failure(UserErrors.SignOutError);
        }

        public async Task<Result<TokenDetails>> RefreshTokenAsync(string tenant, string refreshToken)
        {
            var httpClient = _httpClientFactory.CreateClient("KeycloakClient");

            var urlGetToken = httpClient.BaseAddress.AppendPathSegment("realms")
                .AppendPathSegment(tenant)
                .AppendPathSegment("protocol")
                .AppendPathSegment("openid-connect")
                .AppendPathSegment("token");

            var requestData = new FormUrlEncodedContent(
            [
                new KeyValuePair<string, string>("grant_type", "refresh_token"),
                new KeyValuePair<string, string>("client_id", _tokenCredentials.Client_Id),
                new KeyValuePair<string, string>("client_secret", _tokenCredentials.Client_Secret),
                new KeyValuePair<string, string>("refresh_token", refreshToken),
            ]);

            var response = await httpClient.PostAsync(urlGetToken, requestData);

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<TokenDetails>(content);
                return Result<TokenDetails>.Success(result!);
            }

            var responseMessage = await response.Content.ReadAsStringAsync();
            UserErrors.SetTechnicalMessage(responseMessage);
            return Result<TokenDetails>.Failure(UserErrors.InvalidRefreshToken);
        }
    }
}
