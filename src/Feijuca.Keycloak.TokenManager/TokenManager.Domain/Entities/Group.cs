namespace TokenManager.Domain.Entities
{
    public class Group
    {
        public required Guid Id { get; set; }
        public required string Name { get; set; }
        public required string Path { get; set; }
        public List<Group> SubGroups { get; set; } = [];
    }
}
