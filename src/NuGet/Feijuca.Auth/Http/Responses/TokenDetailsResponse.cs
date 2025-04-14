namespace Feijuca.Auth.Http.Responses
{
    public record TokenDetailsResponse(
        string? AccessToken,
        int ExpiresIn,
        int RefreshExpiresIn,
        string? RefreshToken,
        string? TokenType,
        string? Scope
        );
}
