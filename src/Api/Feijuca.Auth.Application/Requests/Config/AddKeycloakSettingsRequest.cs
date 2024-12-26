using Feijuca.Auth.Models;

namespace Feijuca.Auth.Application.Requests.Config
{
    public record AddKeycloakSettingsRequest(Models.Client MasterClient, Secrets MasterClientSecret, ServerSettings ServerSettings, Models.Realm Realm);
}
