using Newtonsoft.Json;

namespace Application.Responses
{
    public class TokenDetailsResponse
    {
        [JsonProperty("access_token")]
        public string? AccessToken { get; set; }

        [JsonProperty("expires_in")]
        public int ExpiresIn { get; set; }

        [JsonProperty("refresh_expires_in")]
        public int RefreshExpiresIn { get; set; }

        [JsonProperty("refresh_token")]
        public string? RefreshToken { get; set; }

        [JsonProperty("token_type")]
        public string? TokenType { get; set; }

        public string? Scope { get; set; }
    }
}
