namespace Domain.Entities
{
    public class Client
    {
        public Guid Id { get; set; }
        public required string ClientId { get; set; }
        public bool Enabled { get; set; }
    }
}
