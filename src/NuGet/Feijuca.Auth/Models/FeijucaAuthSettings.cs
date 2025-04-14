namespace Feijuca.Auth.Models
{
    public class FeijucaAuthSettings
    {
        public required Client Client { get; set; }
        public required Secrets? Secrets { get; set; }
        public required ServerSettings ServerSettings { get; set; }
        public required IReadOnlyCollection<Realm> Realms { get; init; }
        public required ClientScopes? ClientScopes { get; set; }
    }
}
