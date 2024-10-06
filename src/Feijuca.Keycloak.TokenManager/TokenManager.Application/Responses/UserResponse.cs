using Newtonsoft.Json;
using TokenManager.Domain.Entities;

namespace TokenManager.Application.Responses
{
    public class UserResponse
    {
        [JsonIgnore]
        public Guid Id { get; set; }
        public bool Enabled { get; set; }
        public bool EmailVerified { get; set; }
        public string Username { get; set; }
        public string? Email { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public bool Totp { get; set; }
        public List<string> DisableableCredentialTypes { get; set; } = [];
        public List<string> RequiredActions { get; set; } = [];
        public int NotBefore { get; set; }
        public long CreatedTimestamp { get; set; }
        public Access? Access { get; set; }
        public Dictionary<string, string[]>? Attributes { get; set; }

        public UserResponse(Guid id, string userName, string email, string firstName, string lastName, Dictionary<string, string[]> attributes)
        {
            Id = id;
            Username = userName;
            Email = email;
            FirstName = firstName;
            LastName = lastName;
            Attributes = attributes;
        }

        public UserResponse(Guid userId, string userName, string email, string firstName, string lastName)
        {
            Id = userId;
            Username = userName;
            Email = email;
            FirstName = firstName;
            LastName = lastName;
        }
    }
}
