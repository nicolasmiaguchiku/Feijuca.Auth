using Feijuca.Auth.Models;

namespace Feijuca.Auth.Application.Requests.Config
{
    public record AddKeycloakSettingsRequest(RealmAdminUser RealmAdminUser, Models.Client MasterClient, Secrets MasterClientSecret, ServerSettings ServerSettings, Models.Realm Realm);
}
