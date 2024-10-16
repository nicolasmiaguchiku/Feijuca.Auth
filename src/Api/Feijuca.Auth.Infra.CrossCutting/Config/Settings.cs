using Feijuca.Auth.Models;

namespace Feijuca.Auth.Infra.CrossCutting.Config
{
    public interface ISettings
    {
        public AuthSettings AuthSettings { get; }
    }

    public class Settings : ISettings
    {
        public AuthSettings AuthSettings { get; set; } = null!;
    }
}
