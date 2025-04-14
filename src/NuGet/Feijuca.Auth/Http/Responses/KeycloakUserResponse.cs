namespace Feijuca.Auth.Http.Responses
{
    public record KeycloakUser
    {
        public Guid Id { get; init; }
        public bool Enabled { get; init; }
        public bool EmailVerified { get; init; }
        public required string Username { get; init; }
        public required string? Email { get; init; }
        public required string? FirstName { get; init; }
        public required string? LastName { get; init; }
        public required string Tenant { get; init; }
        public bool Totp { get; init; }
        public IEnumerable<string> DisableableCredentialTypes { get; init; } = [];
        public IEnumerable<string> RequiredActions { get; init; } = [];
        public int NotBefore { get; init; }
        public long CreatedTimestamp { get; init; }
        public Dictionary<string, string[]>? Attributes { get; init; }
    }

}
