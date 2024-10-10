using Common.Errors;
using Common.Extensions;
using Common.Models;
using Domain.Entities;
using Domain.Filters;
using Domain.Interfaces;
using Flurl;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System.Net.Http.Headers;
using System.Text;

namespace Infra.Data.Repositories
{
    public class UserRepository(IHttpClientFactory httpClientFactory, IAuthRepository authRepository)
        : IUserRepository
    {
        private readonly IHttpClientFactory _httpClientFactory = httpClientFactory;
        private readonly IAuthRepository _authRepository = authRepository;

        private static readonly JsonSerializerSettings Settings = new()
        {
            ContractResolver = new CamelCasePropertyNamesContractResolver(),
            NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore
        };

        public async Task<Result<IEnumerable<User>>> GetUsersAsync(string tenant, UserFilters userFilters)
        {
            var tokenDetails = await _authRepository.GetAccessTokenAsync(tenant);
            var httpClient = CreateHttpClientWithHeaders(tokenDetails.Response.Access_Token);

            int first = (userFilters.PageFilter.PageNumber - 1) * userFilters.PageFilter.PageSize;

            var urlGetUsers = httpClient.BaseAddress
                .AppendPathSegment("admin")
                .AppendPathSegment("realms")
                .AppendPathSegment(tenant)
                .AppendPathSegment("users")
                .SetQueryParam("first", first)
                .SetQueryParam("max", userFilters.PageFilter.PageSize)
                .SetCollectionQueryParam("username", userFilters.Emails);

            var response = await httpClient.GetAsync(urlGetUsers);
            var keycloakUserContent = await response.Content.ReadAsStringAsync();
            var users = JsonConvert.DeserializeObject<IEnumerable<User>>(keycloakUserContent)!;

            return Result<IEnumerable<User>>.Success(users);
        }

        public async Task<int> GetTotalUsersAsync(string tenant)
        {
            var tokenDetails = await _authRepository.GetAccessTokenAsync(tenant);
            var httpClient = CreateHttpClientWithHeaders(tokenDetails.Response.Access_Token);

            var urlGetUsers = httpClient.BaseAddress
                .AppendPathSegment("admin")
                .AppendPathSegment("realms")
                .AppendPathSegment(tenant)
                .AppendPathSegment("users")
                .SetQueryParam("first", 0)
                .SetQueryParam("max", 99999);

            var response = await httpClient.GetAsync(urlGetUsers);
            var keycloakUserContent = await response.Content.ReadAsStringAsync();
            var users = JsonConvert.DeserializeObject<IEnumerable<User>>(keycloakUserContent)!;

            return users.Count();
        }

        public async Task<Result<bool>> DeleteAsync(string tenant, Guid id)
        {
            var tokenDetails = await _authRepository.GetAccessTokenAsync(tenant);
            var httpClient = CreateHttpClientWithHeaders(tokenDetails.Response.Access_Token);

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
            var tokenDetails = await _authRepository.GetAccessTokenAsync(tenant);
            var httpClient = CreateHttpClientWithHeaders(tokenDetails.Response.Access_Token);

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
            var tokenDetails = await _authRepository.GetAccessTokenAsync(tenant);
            var httpClient = CreateHttpClientWithHeaders(tokenDetails.Response.Access_Token);

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

        public async Task<Result<bool>> ResetPasswordAsync(string tenant, Guid id, string password)
        {
            var tokenDetails = await _authRepository.GetAccessTokenAsync(tenant);
            var httpClient = CreateHttpClientWithHeaders(tokenDetails.Response.Access_Token);

            var url = httpClient.BaseAddress
                    .AppendPathSegment("admin")
                    .AppendPathSegment("realms")
                    .AppendPathSegment(tenant)
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

        public async Task<Result> SendEmailVerificationAsync(string tenant, string userId)
        {
            var tokenDetails = await _authRepository.GetAccessTokenAsync(tenant);
            var httpClient = CreateHttpClientWithHeaders(tokenDetails.Response.Access_Token);

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
    }
}
