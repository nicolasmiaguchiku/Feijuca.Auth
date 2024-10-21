
using Feijuca.Auth.Models;

namespace Feijuca.Auth.Common.Models
{
    public class KeycloakSettings : IClient, ISecrets, IServerSettings, IRealm
    {
        public string Id => throw new NotImplementedException();

        public string ClientSecret => throw new NotImplementedException();

        public string Url => throw new NotImplementedException();
    }
}
