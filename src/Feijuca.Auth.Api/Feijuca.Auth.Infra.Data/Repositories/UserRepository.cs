using Feijuca.Auth.Common.Errors;
using Feijuca.Auth.Common.Extensions;
using Feijuca.Auth.Common.Models;
using Feijuca.Auth.Domain.Entities;
using Feijuca.Auth.Domain.Filters;
using Feijuca.Auth.Domain.Interfaces;

using Flurl;

using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

using System.Net.Http.Headers;
using System.Text;

namespace Feijuca.Auth.Infra.Data.Repositories
{
    public class UserRepository(TokenCredentials tokenCredentials,
        IHttpClientFactory httpClientFactory,
        IAuthRepository authRepository,
        ITenantService tenantService)
        : IUserRepository
    {
        private readonly TokenCredentials _tokenCredentials = tokenCredentials;
        private readonly IHttpClientFactory _httpClientFactory = httpClientFactory;
        private readonly IAuthRepository _authRepository = authRepository;
        private readonly ITenantService _tenantService = tenantService;

        private static readonly JsonSerializerSettings Settings = new()
        {
            ContractResolver = new CamelCasePropertyNamesContractResolver(),
            NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore
        };

        public async Task<Result<IEnumerable<User>>> GetAllAsync(UserFilters userFilters)
        {
            var tokenDetails = await _authRepository.GetAccessTokenAsync(_tenantService.Tenant);
            var httpClient = CreateHttpClientWithHeaders(tokenDetails.Response.Access_Token);

            int first = (userFilters.PageFilter.PageNumber - 1) * userFilters.PageFilter.PageSize;

            var urlGetUsers = httpClient.BaseAddress
                .AppendPathSegment("admin")
                .AppendPathSegment("realms")
                .AppendPathSegment(_tenantService.Tenant)
                .AppendPathSegment("users")
                .SetQueryParam("first", first)
                .SetQueryParam("max", userFilters.PageFilter.PageSize)
                .SetCollectionQueryParam("username", userFilters.Emails);

            var response = await httpClient.GetAsync(urlGetUsers);
            var keycloakUserContent = await response.Content.ReadAsStringAsync();
            var users = JsonConvert.DeserializeObject<IEnumerable<User>>(keycloakUserContent)!;

            return Result<IEnumerable<User>>.Success(users);
        }

        public async Task<int> GetTotalAsync()
        {
            var tokenDetails = await _authRepository.GetAccessTokenAsync(_tenantService.Tenant);
            var httpClient = CreateHttpClientWithHeaders(tokenDetails.Response.Access_Token);

            var urlGetUsers = httpClient.BaseAddress
                .AppendPathSegment("admin")
                .AppendPathSegment("realms")
                .AppendPathSegment(_tenantService.Tenant)
                .AppendPathSegment("users")
                .SetQueryParam("first", 0)
                .SetQueryParam("max", 99999);

            var response = await httpClient.GetAsync(urlGetUsers);
            var keycloakUserContent = await response.Content.ReadAsStringAsync();
            var users = JsonConvert.DeserializeObject<IEnumerable<User>>(keycloakUserContent)!;

            return users.Count();
        }

        public async Task<Result<bool>> DeleteAsync(Guid id)
        {
            var tokenDetails = await _authRepository.GetAccessTokenAsync(_tenantService.Tenant);
            var httpClient = CreateHttpClientWithHeaders(tokenDetails.Response.Access_Token);

            var url = httpClient.BaseAddress
                .AppendPathSegment("admin")
                .AppendPathSegment("realms")
                .AppendPathSegment(_tenantService.Tenant)
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

        public async Task<Result<bool>> CreateAsync(User user)
        {
            var tokenDetails = await _authRepository.GetAccessTokenAsync(_tenantService.Tenant);
            var httpClient = CreateHttpClientWithHeaders(tokenDetails.Response.Access_Token);

            var url = httpClient.BaseAddress
                    .AppendPathSegment("admin")
                    .AppendPathSegment("realms")
                    .AppendPathSegment(_tenantService.Tenant)
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

        public async Task<Result<User>> GetAsync(string username)
        {
            var tokenDetails = await _authRepository.GetAccessTokenAsync(_tenantService.Tenant);
            var httpClient = CreateHttpClientWithHeaders(tokenDetails.Response.Access_Token);

            var url = httpClient.BaseAddress
                    .AppendPathSegment("admin")
                    .AppendPathSegment("realms")
                    .AppendPathSegment(_tenantService.Tenant)
                    .AppendPathSegment("users");

            url = url.SetQueryParam("username", username);

            var response = await httpClient.GetAsync(url);
            var keycloakUserContent = await response.Content.ReadAsStringAsync();

            var user = JsonConvert.DeserializeObject<List<User>>(keycloakUserContent)!;
            return Result<User>.Success(user[0]);
        }

        public async Task<Result<bool>> ResetPasswordAsync(Guid id, string password)
        {
            var tokenDetails = await _authRepository.GetAccessTokenAsync(_tenantService.Tenant);
            var httpClient = CreateHttpClientWithHeaders(tokenDetails.Response.Access_Token);

            var url = httpClient.BaseAddress
                    .AppendPathSegment("admin")
                    .AppendPathSegment("realms")
                    .AppendPathSegment(_tenantService.Tenant)
                    .AppendPathSegment("users")
                    .AppendPathSegment(id)
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

        public async Task<Result> SendEmailVerificationAsync(string userId)
        {
            var tokenDetails = await _authRepository.GetAccessTokenAsync(_tenantService.Tenant);
            var httpClient = CreateHttpClientWithHeaders(tokenDetails.Response.Access_Token);

            var url = httpClient.BaseAddress!
                .ToString()
                .AppendPathSegment("admin")
                .AppendPathSegment("realms")
                .AppendPathSegment(_tenantService.Tenant)
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

        public async Task<bool> RevokeSessionsAsync(Guid id)
        {
            var tokenDetails = await _authRepository.GetAccessTokenAsync(_tenantService.Tenant);
            var httpClient = CreateHttpClientWithHeaders(tokenDetails.Response.Access_Token);

            var url = httpClient.BaseAddress!
                .ToString()
                .AppendPathSegment("admin")
                .AppendPathSegment("realms")
                .AppendPathSegment(_tenantService.Tenant)
                .AppendPathSegment("users")
                .AppendPathSegment(id)
                .AppendPathSegment("logout");

            var content = new StringContent("{}", Encoding.UTF8, "application/json");
            var resut = await httpClient.PostAsync(url, content);

            if (resut.IsSuccessStatusCode)
            {
                return true;
            }

            return false;
        }

        public async Task<Result<TokenDetails>> LoginAsync(string username, string password)
        {
            var httpClient = _httpClientFactory.CreateClient("KeycloakClient");
            var urlGetToken = httpClient.BaseAddress.AppendPathSegment("realms")
                .AppendPathSegment(_tenantService.Tenant)
                .AppendPathSegment("protocol")
                .AppendPathSegment("openid-connect")
                .AppendPathSegment("token");

            var requestData = new FormUrlEncodedContent(
            [
                new KeyValuePair<string, string>("grant_type", "password"),
                new KeyValuePair<string, string>("client_id", _tokenCredentials.Client_Id),
                new KeyValuePair<string, string>("client_secret", _tokenCredentials.Client_Secret),
                new KeyValuePair<string, string>("username", username),
                new KeyValuePair<string, string>("password", password)
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

        public async Task<Result<bool>> SignoutAsync(string refreshToken)
        {
            var httpClient = _httpClientFactory.CreateClient("KeycloakClient");

            var urlGetToken = httpClient.BaseAddress.AppendPathSegment("realms")
                .AppendPathSegment(_tenantService.Tenant)
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

        public async Task<Result<TokenDetails>> RefreshTokenAsync(string refreshToken)
        {
            var httpClient = _httpClientFactory.CreateClient("KeycloakClient");

            var urlGetToken = httpClient.BaseAddress.AppendPathSegment("realms")
                .AppendPathSegment(_tenantService.Tenant)
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

        private HttpClient CreateHttpClientWithHeaders(string accessToken)
        {
            var httpClient = _httpClientFactory.CreateClient("KeycloakClient");
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
            return httpClient;
        }
    }
}
