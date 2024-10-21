using Feijuca.Auth.Models;

namespace Feijuca.Auth.Api.Tests.Models
{
    public class Settings
    {
        public required IServerSettings AuthSettings { get; set; }
    }
}
