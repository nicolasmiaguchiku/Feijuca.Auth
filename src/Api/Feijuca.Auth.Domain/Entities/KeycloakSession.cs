namespace Feijuca.Auth.Domain.Entities
{
    public  class KeycloakSession
    {
        public string? Id { get; set; }
        public string? Username { get; set; }
        public string? UserId { get; set; }
        public string? IpAddress { get; set; }
        public string? Start { get; set; }
        public string? LastAccess { get; set; }
        public Dictionary<string, string>? Clients { get; set; }
    }
}
