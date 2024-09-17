using Flurl;

using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

using System.Net.Http.Headers;
using System.Text;

using TokenManager.Common.Errors;
using TokenManager.Common.Models;
using TokenManager.Domain.Entities;
using TokenManager.Domain.Interfaces;

namespace TokenManager.Infra.Data.Repositories
{
    public class UserRepository(IHttpClientFactory httpClientFactory, ITokenRepository tokenRepository, TokenCredentials tokenCredentials)
        : IUserRepository
    {
        private readonly IHttpClientFactory _httpClientFactory = httpClientFactory;
        private readonly ITokenRepository _tokenRepository = tokenRepository;

        private readonly TokenCredentials _tokenCredentials = tokenCredentials;
        private static readonly JsonSerializerSettings Settings = new()
        {
            ContractResolver = new CamelCasePropertyNamesContractResolver(),
            NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore
        };

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

        public async Task<Result<TokenDetails>> RefreshTokenAsync(string tenant, string refreshToken)
        {
            var httpClient = CreateHttpClientWithoutHeaders();

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

        public async Task<Result<IEnumerable<User>>> GetAllAsync(string tenant)
        {
            var tokenDetails = await _tokenRepository.GetAccessTokenAsync(tenant);
            var httpClient = CreateHttpClientWithHeaders(tokenDetails.Data.Access_Token);

            var urlGetUsers = httpClient.BaseAddress
                .AppendPathSegment("admin")
                .AppendPathSegment("realms")
                .AppendPathSegment(tenant)
                .AppendPathSegment("users");

            var response = await httpClient.GetAsync(urlGetUsers);
            var keycloakUserContent = await response.Content.ReadAsStringAsync();
            var users = JsonConvert.DeserializeObject<IEnumerable<User>>(keycloakUserContent)!;

            return Result<IEnumerable<User>>.Success(users);
        }

        public async Task<Result<bool>> DeleteAsync(string tenant, Guid id)
        {
            var tokenDetails = await _tokenRepository.GetAccessTokenAsync(tenant);
            var httpClient = CreateHttpClientWithHeaders(tokenDetails.Data.Access_Token);

            var url = httpClient.BaseAddress
                .AppendPathSegment("admin")
                .AppendPathSegment("realms")
                .AppendPathSegment(tenant)
                .AppendPathSegment("users")
                .AppendPathSegment(id);

            var response = await httpClient.DeleteAsync(url);
            if (response.IsSuccessStatusCode)
            {
                return Result<bool>.Success(true);
            }

            UserErrors.SetTechnicalMessage(response.ReasonPhrase!);
            return Result<bool>.Failure(UserErrors.DeletionUserError);
        }

        public async Task<Result<bool>> CreateAsync(string tenant, User user)
        {
            var tokenDetails = await _tokenRepository.GetAccessTokenAsync(tenant);
            var httpClient = CreateHttpClientWithHeaders(tokenDetails.Data.Access_Token);

            var url = httpClient.BaseAddress
                    .AppendPathSegment("admin")
                    .AppendPathSegment("realms")
                    .AppendPathSegment(tenant)
                    .AppendPathSegment("users");

            var json = JsonConvert.SerializeObject(user, Settings);
            var httpContent = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await httpClient.PostAsync(url, httpContent);
            if (response.IsSuccessStatusCode)
            {
                return Result<bool>.Success(true);
            }

            return Result<bool>.Failure(UserErrors.UserCreationError);
        }

        public async Task<Result<User>> GetAsync(string tenant, string userName)
        {
            var tokenDetails = await _tokenRepository.GetAccessTokenAsync(tenant);
            var httpClient = CreateHttpClientWithHeaders(tokenDetails.Data.Access_Token);

            var url = httpClient.BaseAddress
                    .AppendPathSegment("admin")
                    .AppendPathSegment("realms")
                    .AppendPathSegment(tenant)
                    .AppendPathSegment("users");

            url = url.SetQueryParam("username", userName);

            var response = await httpClient.GetAsync(url);
            var keycloakUserContent = await response.Content.ReadAsStringAsync();

            var user = JsonConvert.DeserializeObject<List<User>>(keycloakUserContent)!;
            return Result<User>.Success(user[0]);
        }

        public async Task<Result<bool>> ResetPasswordAsync(string tenant, string userId, string password)
        {
            var tokenDetails = await _tokenRepository.GetAccessTokenAsync(tenant);
            var httpClient = CreateHttpClientWithHeaders(tokenDetails.Data.Access_Token);

            var url = httpClient.BaseAddress
                    .AppendPathSegment("admin")
                    .AppendPathSegment("realms")
                    .AppendPathSegment(tenant)
                    .AppendPathSegment("users")
                    .AppendPathSegment(userId)
                    .AppendPathSegment("reset-password");

            var passwordData = new
            {
                type = "password",
                temporary = false,
                value = password
            };

            var json = JsonConvert.SerializeObject(passwordData, Settings);
            var httpContent = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await httpClient.PutAsync(url, httpContent);

            if (response.IsSuccessStatusCode)
            {
                return Result<bool>.Success(true);
            }

            var responseMessage = await response.Content.ReadAsStringAsync();
            UserErrors.SetTechnicalMessage(responseMessage);
            return Result<bool>.Failure(UserErrors.WrongPasswordDefinition);
        }

        public async Task<Result> SendEmailVerificationAsync(string tenant, string userId)
        {
            var tokenDetails = await _tokenRepository.GetAccessTokenAsync(tenant);
            var httpClient = CreateHttpClientWithHeaders(tokenDetails.Data.Access_Token);

            var url = httpClient.BaseAddress!
                .ToString()
                .AppendPathSegment("admin")
                .AppendPathSegment("realms")
                .AppendPathSegment(tenant)
                .AppendPathSegment("users")
                .AppendPathSegment(userId);

            var requestData = new
            {
                requiredActions = new string[] { "VERIFY_EMAIL" }
            };

            var json = JsonConvert.SerializeObject(requestData, Settings);
            var httpContent = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await httpClient.PutAsync(url, httpContent);

            if (response.IsSuccessStatusCode)
            {
                url = url.AppendPathSegment("send-verify-email");
                await httpClient.PutAsync(url, default!);
                return Result.Success();
            }

            var responseMessage = await response.Content.ReadAsStringAsync();
            UserErrors.SetTechnicalMessage(responseMessage);
            return Result.Failure(UserErrors.InvalidUserNameOrPasswordError);
        }

        private HttpClient CreateHttpClientWithHeaders(string accessToken)
        {
            var httpClient = _httpClientFactory.CreateClient("KeycloakClient");
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
            return httpClient;
        }

        private HttpClient CreateHttpClientWithoutHeaders()
        {
            var httpClient = _httpClientFactory.CreateClient("KeycloakClient");
            return httpClient;
        }
    }
}
