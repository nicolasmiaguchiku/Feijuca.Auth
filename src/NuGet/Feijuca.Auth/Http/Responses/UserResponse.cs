using Newtonsoft.Json;

namespace Feijuca.Auth.Http.Responses
{
    public record UserResponse(
        [property: JsonIgnore] Guid Id,
        bool Enabled,
        bool EmailVerified,
        string Username,
        string? Email,
        string? FirstName,
        string? LastName,
        string Tenant,
        bool Totp,
        List<string> DisableableCredentialTypes,
        List<string> RequiredActions,
        int NotBefore,
        long CreatedTimestamp,
        Access? Access,
        Dictionary<string, string[]>? Attributes
        );
}
