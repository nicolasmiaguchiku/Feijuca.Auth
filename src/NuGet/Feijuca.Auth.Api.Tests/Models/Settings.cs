using Feijuca.Auth.Models;

namespace Feijuca.Auth.Api.Tests.Models
{
    public class Settings
    {
        public required Client Client { get; init; }

        public required Secrets Secrets { get; init; }

        public required ServerSettings ServerSettings { get; init; }

        public required Realm Realm { get; init; }

        public required ClientScopes ClientScopes { get; init; }
    }
}
