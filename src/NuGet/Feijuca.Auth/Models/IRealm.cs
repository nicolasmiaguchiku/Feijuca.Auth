namespace Feijuca.Auth.Models
{
    public interface IRealm
    {
        public string Name { get;}
        public string Audience { get; }
        public string Issuer { get; }
    }
}
