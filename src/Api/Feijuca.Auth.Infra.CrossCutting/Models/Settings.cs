using Mattioli.Configurations.Models;

namespace Feijuca.Auth.Infra.CrossCutting.Models
{
    public interface ISettings
    {
        public MltSettings MltSettings { get; }
        public MongoSettings MongoSettings { get; }
        public SeqSettings SeqSettings { get; }
    }
    public class Settings : ISettings
    {
        public required MltSettings MltSettings { get; set; }
        public required MongoSettings MongoSettings { get; set; }
        public required SeqSettings SeqSettings { get; set; }
    }
}