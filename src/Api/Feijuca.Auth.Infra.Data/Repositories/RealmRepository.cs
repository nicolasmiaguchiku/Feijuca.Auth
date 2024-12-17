using Feijuca.Auth.Domain.Interfaces;
using Flurl;
using Newtonsoft.Json;

namespace Feijuca.Auth.Infra.Data.Repositories
{
    public class RealmRepository(IHttpClientFactory httpClientFactory, IAuthRepository authRepository) : BaseRepository(httpClientFactory), IRealmRepository
    {
        private readonly IAuthRepository _authRepository = authRepository;

        public async Task<string> GetRealmConfig(string name, CancellationToken cancellationToken)
        {
            var tokenDetails = await _authRepository.GetAccessTokenAsync(cancellationToken);
            var httpClient = CreateHttpClientWithHeaders(tokenDetails.Response.Access_Token);

            var url = httpClient.BaseAddress
                    .AppendPathSegment("admin")
                    .AppendPathSegment("realms")
                    .AppendPathSegment(name)
                    .AppendPathSegment("clients");

            var response = await httpClient.GetAsync(url, cancellationToken);

            if (response.IsSuccessStatusCode)
            {
                var responseContent = await response.Content.ReadAsStringAsync(cancellationToken);
                var result = JsonConvert.DeserializeObject<string>(responseContent)!;
                return result;
            }

            return "";
        }
    }
}
