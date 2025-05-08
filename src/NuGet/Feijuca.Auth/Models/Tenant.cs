namespace Feijuca.Auth.Models
{
    public class Tenant(IEnumerable<string> names)
    {
        public IEnumerable<string> Names { get; set; } = names;
    }
}
