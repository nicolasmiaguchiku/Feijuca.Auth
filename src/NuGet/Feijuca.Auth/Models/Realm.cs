namespace Feijuca.Auth.Models
{
    public class Realm
    {
        public required string Name { get; init; }
        public required string Audience { get; init; }
        public required string Issuer { get; init; }
    }
}
