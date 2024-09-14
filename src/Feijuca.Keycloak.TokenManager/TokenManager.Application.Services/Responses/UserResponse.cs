using Newtonsoft.Json;

using TokenManager.Domain.Entities;

namespace TokenManager.Application.Responses
{
    public class UserResponse
    {
        [JsonIgnore]
        public string Password { get; set; } = null!;
        public string? Id { get; set; }
        public bool Enabled { get; set; }
        public bool EmailVerified { get; set; }
        public string Username { get; set; } = null!;
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

        public UserResponse()
        {

        }

        public UserResponse(string userName, string password)
        {
            Username = userName;
            Password = password;
        }

        public UserResponse(string id, string userName, string password, string email, string firstName, string lastName, Dictionary<string, string[]> attributes)
        {
            Id = id;
            Email = email;
            Password = password;
            FirstName = firstName;
            Username = userName;
            LastName = lastName;
            Attributes = attributes;
        }
    }
}
