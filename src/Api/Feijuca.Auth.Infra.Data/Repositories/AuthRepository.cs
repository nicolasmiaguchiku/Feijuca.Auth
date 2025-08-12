﻿using Feijuca.Auth.Common.Errors;
using Mattioli.Configurations.Models;
using Feijuca.Auth.Domain.Entities;
using Feijuca.Auth.Domain.Interfaces;
using Flurl;
using Newtonsoft.Json;

namespace Feijuca.Auth.Infra.Data.Repositories
{
    public class AuthRepository(IHttpClientFactory httpClientFactory, IConfigRepository configRepository) : IAuthRepository
    {
        private readonly IHttpClientFactory _httpClientFactory = httpClientFactory;

        public async Task<Result<TokenDetails>> GetAccessTokenAsync(CancellationToken cancellationToken)
        {
            var config = await configRepository.GetConfigAsync();
            var requestData = new FormUrlEncodedContent(
            [
                new KeyValuePair<string, string>("grant_type", "client_credentials"),
                new KeyValuePair<string, string>("client_id", config.Client.ClientId),
                new KeyValuePair<string, string>("client_secret", config.Secrets.ClientSecret),
            ]);

            using var httpClient = _httpClientFactory.CreateClient("KeycloakClient");
            var url = httpClient.BaseAddress
                .AppendPathSegment("realms")
                .AppendPathSegment("master")
                .AppendPathSegment("protocol")
                .AppendPathSegment("openid-connect")
                .AppendPathSegment("token");

            using var response =  await httpClient.PostAsync(url, requestData, cancellationToken);

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync(cancellationToken);
                var result = JsonConvert.DeserializeObject<TokenDetails>(content)!;

                return Result<TokenDetails>.Success(result);
            }

            var responseMessage = await response.Content.ReadAsStringAsync(cancellationToken);
            UserErrors.SetTechnicalMessage(responseMessage);
            return Result<TokenDetails>.Failure(UserErrors.TokenGenerationError);
        }
    }
}
