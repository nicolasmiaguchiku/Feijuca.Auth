namespace Feijuca.Auth.Domain.Entities
{
    public class Group
    {
        public required Guid Id { get; set; }
        public required string Name { get; set; }
        public required string Path { get; set; }
    }
}
