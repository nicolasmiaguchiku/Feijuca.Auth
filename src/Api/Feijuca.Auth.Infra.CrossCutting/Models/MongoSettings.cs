namespace Feijuca.Auth.Infra.CrossCutting.Models
{
    public class MongoSettings
    {
        public required string DatabaseName { get; set; }
        public required string ConnectionString { get; set; }
    }
}
