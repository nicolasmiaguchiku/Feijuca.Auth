namespace TokenManager.Domain.Entities
{
    public class Client
    {
        public string Id { get; set; } = null!;
        public string ClientId { get; set; } = null!;
        public bool Enabled { get; set; }
    }
}
