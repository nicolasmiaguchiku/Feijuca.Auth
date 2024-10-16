namespace Feijuca.Auth.Domain.Entities
{
    public class ClientMapping
    {
        public required string Id { get; set; }
        public required string Client { get; set; }
        public List<Role> Mappings { get; set; } = [];
    }
}
