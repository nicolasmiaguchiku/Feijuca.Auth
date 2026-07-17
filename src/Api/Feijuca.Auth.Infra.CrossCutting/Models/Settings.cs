using Feijuca.Auth.Models;

namespace Feijuca.Auth.Infra.CrossCutting.Models
{
    public interface ISettings
    {
        public MongoSettings MongoSettings { get; }
    }
    public class Settings : ISettings
    {
        public required MongoSettings MongoSettings { get; set; }
    }
}