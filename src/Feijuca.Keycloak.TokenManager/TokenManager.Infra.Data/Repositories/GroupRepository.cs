using Flurl;
using Newtonsoft.Json;
using System.Text;
using TokenManager.Domain.Entities;
using TokenManager.Domain.Errors;
using TokenManager.Domain.Interfaces;

namespace TokenManager.Infra.Data.Repositories
{
    public class GroupRepository(IHttpClientFactory httpClientFactory, ITokenRepository tokenRepository) : IGroupRepository
    {
        private readonly IHttpClientFactory _httpClientFactory = httpClientFactory;
        private readonly ITokenRepository _tokenRepository = tokenRepository;

        public async Task<Result> CreateGroupAsync(string tenant, string name, Dictionary<string, string[]> attributes)
        {
            var httpClient = _httpClientFactory.CreateClient("KeycloakClient");

            var url = httpClient.BaseAddress
                    .AppendPathSegment("admin")
                    .AppendPathSegment("realms")
                    .AppendPathSegment(tenant)
                    .AppendPathSegment("groups");

            var group = new
            {
                name,
                attributes
            };

            var tokenDetails = await _tokenRepository.GetAccessTokenAsync(tenant);

            httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {tokenDetails.Value.Access_Token}");

            var jsonContent = JsonConvert.SerializeObject(group);
            var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

            var response = await httpClient.PostAsync(url, content);
            if (response.IsSuccessStatusCode)
            {
                return Result.Success();
            }

            GroupErrors.SetTechnicalMessage(response.ReasonPhrase!);
            return Result.Failure(GroupErrors.CreationGroupError);
        }
    }
}
