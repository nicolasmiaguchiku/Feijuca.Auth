
using Feijuca.Auth.Models;

namespace Feijuca.Auth.Common.Models
{
    public class KeycloakSettings
    {
        public required IClient Client { get; set; }
        public required ISecrets Secrets { get; set; }
        public required IServerSettings ServerSettings { get; set; }
        public required IRealm Realm { get; set; }
    }
}
