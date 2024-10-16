namespace Feijuca.Auth.Domain.Entities
{
    public class TokenCredentials
    {
        public string ServerUrl { get; set; } = null!;
        public string Client_Secret { get; set; } = null!;
        public string Client_Id { get; set; } = null!;
    }
}
