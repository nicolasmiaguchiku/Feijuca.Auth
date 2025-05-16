using Newtonsoft.Json;

namespace Feijuca.Auth.Http.Responses
{
    public record TokenDetailsResponse(
        [property: JsonProperty("access_token")] string? AccessToken,
        [property: JsonProperty("expires_in")] int ExpiresIn,
        [property: JsonProperty("refresh_expires_in")] int RefreshExpiresIn,
        [property: JsonProperty("refresh_token")] string? RefreshToken,
        [property: JsonProperty("token_type")] string? TokenType,
        [property: JsonProperty("scope")] string? Scope
        );
}
