namespace TokenManager.Domain.Entities
{
    public class Role
    {
        public required string Id { get; set; }
        public required string Name { get; set; }
        public required string Description { get; set; }
        public bool Composite { get; set; }
        public bool ClientRole { get; set; }
        public required string ContainerId { get; set; }
    }
}
