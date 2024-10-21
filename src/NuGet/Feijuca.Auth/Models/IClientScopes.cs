namespace Feijuca.Auth.Models
{
    public interface IClientScopes
    {
        public IEnumerable<string> Scopes { get; }
    }
}
