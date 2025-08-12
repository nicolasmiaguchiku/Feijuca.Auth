﻿using Feijuca.Auth.Common.Errors;
using Mattioli.Configurations.Models;
using Feijuca.Auth.Domain.Entities;
using Feijuca.Auth.Domain.Filters;
using Feijuca.Auth.Domain.Interfaces;
using Feijuca.Auth.Services;
using Flurl;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System.Text;

namespace Feijuca.Auth.Infra.Data.Repositories
{
    public class UserRepository(IClientRepository clientRepository,
        IHttpClientFactory httpClientFactory,
        IAuthRepository authRepository,
        ITenantService tenantService)
        : BaseRepository(httpClientFactory), IUserRepository
    {
        private readonly IHttpClientFactory _httpClientFactory = httpClientFactory;
        private readonly IAuthRepository _authRepository = authRepository;
        private readonly ITenantService _tenantService = tenantService;

        private static readonly JsonSerializerSettings Settings = new()
        {
            ContractResolver = new CamelCasePropertyNamesContractResolver(),
            NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore
        };

        public async Task<Result<IEnumerable<User>>> GetAllAsync(UserFilters userFilters, int totalUsers, CancellationToken cancellationToken)
        {
            var tokenDetails = await _authRepository.GetAccessTokenAsync(cancellationToken);
            using var httpClient = CreateHttpClientWithHeaders(tokenDetails.Data.Access_Token);

            int first = (userFilters.PageFilter.PageNumber - 1) * userFilters.PageFilter.PageSize;

            var urlGetUsers = httpClient.BaseAddress
                .AppendPathSegment("admin")
                .AppendPathSegment("realms")
                .AppendPathSegment(_tenantService.Tenant.Name)
                .AppendPathSegment("users")
                .SetQueryParam("first", first)
                .SetQueryParam("max", totalUsers);

            using var response = await httpClient.GetAsync(urlGetUsers, cancellationToken);
            var keycloakUserContent = await response.Content.ReadAsStringAsync(cancellationToken);
            var users = JsonConvert.DeserializeObject<IEnumerable<User>>(keycloakUserContent)!;

            return Result<IEnumerable<User>>.Success(users
                .OrderBy(a => a.FirstName)
                .ThenBy(a => a.LastName));
        }

        public async Task<int> GetTotalAsync(CancellationToken cancellationToken)
        {
            var tokenDetails = await _authRepository.GetAccessTokenAsync(cancellationToken);
            using var httpClient = CreateHttpClientWithHeaders(tokenDetails.Data.Access_Token);

            var urlGetUsers = httpClient.BaseAddress
                .AppendPathSegment("admin")
                .AppendPathSegment("realms")
                .AppendPathSegment(_tenantService.Tenant.Name)
                .AppendPathSegment("users")
                .SetQueryParam("first", 0)
                .SetQueryParam("max", 99999);

            using var response = await httpClient.GetAsync(urlGetUsers, cancellationToken);
            var keycloakUserContent = await response.Content.ReadAsStringAsync(cancellationToken);
            var users = JsonConvert.DeserializeObject<IEnumerable<User>>(keycloakUserContent)!;

            return users.Count();
        }

        public async Task<Result<bool>> DeleteAsync(Guid id, CancellationToken cancellationToken)
        {
            var tokenDetails = await _authRepository.GetAccessTokenAsync(cancellationToken);
            using var httpClient = CreateHttpClientWithHeaders(tokenDetails.Data.Access_Token);

            var url = httpClient.BaseAddress
                .AppendPathSegment("admin")
                .AppendPathSegment("realms")
                .AppendPathSegment(_tenantService.Tenant.Name)
                .AppendPathSegment("users")
                .AppendPathSegment(id);

            using var response = await httpClient.DeleteAsync(url, cancellationToken);
            if (response.IsSuccessStatusCode)
            {
                return Result<bool>.Success(true);
            }

            UserErrors.SetTechnicalMessage(response.ReasonPhrase!);
            return Result<bool>.Failure(UserErrors.DeletionUserError);
        }

        public async Task<Result<bool>> CreateAsync(string tenant, User user, CancellationToken cancellationToken)
        {
            var tokenDetails = await _authRepository.GetAccessTokenAsync(cancellationToken);
            using var httpClient = CreateHttpClientWithHeaders(tokenDetails.Data.Access_Token);

            var url = httpClient.BaseAddress
                    .AppendPathSegment("admin")
                    .AppendPathSegment("realms")
                    .AppendPathSegment(tenant)
                    .AppendPathSegment("users");
            
            var json = JsonConvert.SerializeObject(user, Settings);
            var httpContent = new StringContent(json, Encoding.UTF8, "application/json");
            using var response = await httpClient.PostAsync(url, httpContent, cancellationToken);
            if (response.IsSuccessStatusCode)
            {
                return Result<bool>.Success(true);
            }
            var error = await response.Content.ReadAsStringAsync(cancellationToken);

            UserErrors.SetTechnicalMessage(error);
            return Result<bool>.Failure(UserErrors.UserCreationError);
        }

        public async Task<Result<User>> GetAsync(string tenant, string username, CancellationToken cancellationToken)
        {
            if (string.IsNullOrEmpty(username))
            {
                return Result<User>.Failure(UserErrors.InvalidUserNameOrPasswordError);
            }

            var tokenDetails = await _authRepository.GetAccessTokenAsync(cancellationToken);
            using var httpClient = CreateHttpClientWithHeaders(tokenDetails.Data.Access_Token);

            var url = httpClient.BaseAddress
                    .AppendPathSegment("admin")
                    .AppendPathSegment("realms")
                    .AppendPathSegment(tenant)
                    .AppendPathSegment("users");

            url = url.SetQueryParam("username", username);

            using var response = await httpClient.GetAsync(url, cancellationToken);
            var keycloakUserContent = await response.Content.ReadAsStringAsync(cancellationToken);

            var user = JsonConvert.DeserializeObject<List<User>>(keycloakUserContent)!;

            if (user.Count == 0)
            {
                return Result<User>.Failure(UserErrors.InvalidUserNameOrPasswordError);
            }

            return Result<User>.Success(user[0]);
        }

        public async Task<Result<bool>> ResetPasswordAsync(Guid id, string tenant, string password, CancellationToken cancellationToken)
        {
            var tokenDetails = await _authRepository.GetAccessTokenAsync(cancellationToken);
            using var httpClient = CreateHttpClientWithHeaders(tokenDetails.Data.Access_Token);

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
            using var response = await httpClient.PutAsync(url, httpContent, cancellationToken);

            if (response.IsSuccessStatusCode)
            {
                return Result<bool>.Success(true);
            }

            var responseMessage = await response.Content.ReadAsStringAsync(cancellationToken);
            UserErrors.SetTechnicalMessage(responseMessage);
            return Result<bool>.Failure(UserErrors.WrongPasswordDefinition);
        }

        public async Task<Result> SendEmailVerificationAsync(string userId, CancellationToken cancellationToken)
        {
            var tokenDetails = await _authRepository.GetAccessTokenAsync(cancellationToken);
            using var httpClient = CreateHttpClientWithHeaders(tokenDetails.Data.Access_Token);

            var url = httpClient.BaseAddress!
                .ToString()
                .AppendPathSegment("admin")
                .AppendPathSegment("realms")
                .AppendPathSegment(_tenantService.Tenant.Name)
                .AppendPathSegment("users")
                .AppendPathSegment(userId);

            var requestData = new
            {
                requiredActions = new string[] { "VERIFY_EMAIL" }
            };

            var json = JsonConvert.SerializeObject(requestData, Settings);
            var httpContent = new StringContent(json, Encoding.UTF8, "application/json");
            using var response = await httpClient.PutAsync(url, httpContent, cancellationToken);

            if (response.IsSuccessStatusCode)
            {
                url = url.AppendPathSegment("send-verify-email");
                await httpClient.PutAsync(url, default!, cancellationToken);
                return Result.Success();
            }

            var responseMessage = await response.Content.ReadAsStringAsync(cancellationToken);
            UserErrors.SetTechnicalMessage(responseMessage);
            return Result.Failure(UserErrors.InvalidUserNameOrPasswordError);
        }

        public async Task<Result<bool>> RevokeSessionsAsync(Guid id, CancellationToken cancellationToken)
        {
            var tokenDetails = await _authRepository.GetAccessTokenAsync(cancellationToken);
            using var httpClient = CreateHttpClientWithHeaders(tokenDetails.Data.Access_Token);

            var url = httpClient.BaseAddress!
                .ToString()
                .AppendPathSegment("admin")
                .AppendPathSegment("realms")
                .AppendPathSegment(_tenantService.Tenant.Name)
                .AppendPathSegment("users")
                .AppendPathSegment(id)
                .AppendPathSegment("logout");

            var content = new StringContent("{}", Encoding.UTF8, "application/json");
            using var resut = await httpClient.PostAsync(url, content, cancellationToken);

            if (resut.IsSuccessStatusCode)
            {
                return Result<bool>.Success(true);
            }

            return Result<bool>.Failure(UserErrors.RevokeSessionsError);
        }

        public async Task<Result<TokenDetails>> LoginAsync(string username, string password, CancellationToken cancellationToken)
        {
            using var httpClient = _httpClientFactory.CreateClient("KeycloakClient");

            var urlGetToken = httpClient.BaseAddress.AppendPathSegment("realms")
                .AppendPathSegment(_tenantService.Tenant.Name)
                .AppendPathSegment("protocol")
                .AppendPathSegment("openid-connect")
                .AppendPathSegment("token");

            var client = await clientRepository.GetClientAsync("feijuca-auth-api", cancellationToken);

            var requestData = new FormUrlEncodedContent(
            [
                new KeyValuePair<string, string>("grant_type", "password"),
                new KeyValuePair<string, string>("client_id", client.Data.ClientId),
                new KeyValuePair<string, string>("client_secret", client.Data.Secret),
                new KeyValuePair<string, string>("username", username),
                new KeyValuePair<string, string>("password", password)
            ]);

            var response = await httpClient.PostAsync(urlGetToken, requestData, cancellationToken);

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync(cancellationToken);
                var result = JsonConvert.DeserializeObject<TokenDetails>(content);
                return Result<TokenDetails>.Success(result!);
            }

            var responseMessage = await response.Content.ReadAsStringAsync(cancellationToken);
            UserErrors.SetTechnicalMessage(responseMessage);
            return Result<TokenDetails>.Failure(UserErrors.InvalidUserNameOrPasswordError);
        }

        public async Task<Result<bool>> SignoutAsync(string refreshToken, CancellationToken cancellationToken)
        {
            using var httpClient = _httpClientFactory.CreateClient("KeycloakClient");

            var urlGetToken = httpClient.BaseAddress.AppendPathSegment("realms")
                .AppendPathSegment(_tenantService.Tenant.Name)
                .AppendPathSegment("protocol")
                .AppendPathSegment("openid-connect")
                .AppendPathSegment("logout");

            var client = await clientRepository.GetClientAsync("feijuca-auth-api", cancellationToken);

            var requestData = new FormUrlEncodedContent(
            [
                new KeyValuePair<string, string>("client_id", client.Data.ClientId),
                new KeyValuePair<string, string>("refresh_token", refreshToken),
                new KeyValuePair<string, string>("client_secret", client.Data.Secret)
            ]);

            var response = await httpClient.PostAsync(urlGetToken, requestData, cancellationToken);

            if (response.IsSuccessStatusCode)
            {
                return Result<bool>.Success(true);
            }

            return Result<bool>.Failure(UserErrors.SignOutError);
        }

        public async Task<Result<TokenDetails>> RefreshTokenAsync(string tenant, string refreshToken, CancellationToken cancellationToken)
        {
            using var httpClient = _httpClientFactory.CreateClient("KeycloakClient");

            var urlGetToken = httpClient.BaseAddress.AppendPathSegment("realms")
                .AppendPathSegment(tenant)
                .AppendPathSegment("protocol")
                .AppendPathSegment("openid-connect")
                .AppendPathSegment("token");

            var client = await clientRepository.GetClientAsync("feijuca-auth-api", cancellationToken);

            var requestData = new FormUrlEncodedContent(
            [
                new KeyValuePair<string, string>("grant_type", "refresh_token"),
                new KeyValuePair<string, string>("client_id", client.Data.ClientId),
                new KeyValuePair<string, string>("client_secret", client.Data.Secret),
                new KeyValuePair<string, string>("refresh_token", refreshToken),
            ]);

            var response = await httpClient.PostAsync(urlGetToken, requestData, cancellationToken);

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync(cancellationToken);
                var result = JsonConvert.DeserializeObject<TokenDetails>(content);
                return Result<TokenDetails>.Success(result!);
            }

            var responseMessage = await response.Content.ReadAsStringAsync(cancellationToken);
            UserErrors.SetTechnicalMessage(responseMessage);
            return Result<TokenDetails>.Failure(UserErrors.InvalidRefreshToken);
        }

        public async Task<Result<bool>> UpdateUserAsync(Guid id, User user, CancellationToken cancellationToken)
        {
            var tokenDetails = await _authRepository.GetAccessTokenAsync(cancellationToken);
            using var httpClient = CreateHttpClientWithHeaders(tokenDetails.Data.Access_Token);

            var url = httpClient.BaseAddress
                    .AppendPathSegment("admin")
                    .AppendPathSegment("realms")
                    .AppendPathSegment(_tenantService.Tenant.Name)
                    .AppendPathSegment("users")
                    .AppendPathSegment(id);

            var json = JsonConvert.SerializeObject(user, Settings);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            using var response = await httpClient.PutAsync(url, content, cancellationToken);

            if (response.IsSuccessStatusCode)
            {
                return Result<bool>.Success(true);
            }

            var responseMessage = await response.Content.ReadAsStringAsync(cancellationToken);
            UserErrors.SetTechnicalMessage(responseMessage);
            return Result<bool>.Failure(UserErrors.InvalidRefreshToken);
        }
    }
}
