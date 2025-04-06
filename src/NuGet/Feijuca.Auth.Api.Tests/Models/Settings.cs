using Coderaw.Settings.Models;

using Feijuca.Auth.Models;

namespace Feijuca.Auth.Api.Tests.Models
{
    public class Settings
    {
        public required FeijucaAuthSettings KeycloakSettings { get; set; }
    }
}
