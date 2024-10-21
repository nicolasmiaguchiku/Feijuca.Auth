namespace Feijuca.Auth.Models
{
    public interface ClientScopes
    {
        public IEnumerable<string> Scopes { get; }
    }
}
