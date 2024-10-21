namespace Feijuca.Auth.Models
{
    public interface IPolicy
    {
        public string Name { get; }
        public IEnumerable<string> Roles { get; }
    }
}
