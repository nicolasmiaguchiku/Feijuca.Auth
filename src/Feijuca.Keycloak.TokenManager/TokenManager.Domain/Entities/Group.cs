namespace TokenManager.Domain.Entities
{
    public class Group
    {
        public required string Id { get; set; }
        public required string Name { get; set; }
        public required string Path { get; set; }
        public List<Group> SubGroups { get; set; } = [];
    }
}
