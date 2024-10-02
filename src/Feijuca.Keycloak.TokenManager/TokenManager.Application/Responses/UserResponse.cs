using Newtonsoft.Json;

using TokenManager.Domain.Entities;

namespace TokenManager.Application.Responses
{
    public class UserResponse(Guid id, string userName, string email, string firstName, string lastName, Dictionary<string, string[]> attributes)
    {
        [JsonIgnore]
        public Guid Id { get; set; } = id;
        public bool Enabled { get; set; }
        public bool EmailVerified { get; set; }
        public string Username { get; set; } = userName;
        public string? Email { get; set; } = email;
        public string? FirstName { get; set; } = firstName;
        public string? LastName { get; set; } = lastName;
        public bool Totp { get; set; }
        public List<string> DisableableCredentialTypes { get; set; } = [];
        public List<string> RequiredActions { get; set; } = [];
        public int NotBefore { get; set; }
        public long CreatedTimestamp { get; set; }
        public Access? Access { get; set; }
        public Dictionary<string, string[]>? Attributes { get; set; } = attributes;
    }
}
