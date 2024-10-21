using Feijuca.Auth.Domain.Entities;
using Feijuca.Auth.Models;

namespace Feijuca.Auth.Application.Mappers
{
    public static class ConfigMapper
    {
        public static AuthSettingsEntity ToEntity(this AuthSettings authSettings)
        {
            return new AuthSettingsEntity
            {
                AuthServerUrl = authSettings.AuthServerUrl,
                ClientId = authSettings.ClientId,
                ClientSecret = authSettings.ClientId,
                Realms = authSettings.Realms,
                PolicyName = authSettings.PolicyName,
                Roles = authSettings.Roles,
                Scopes = authSettings.Scopes,
                Id = Guid.NewGuid()
            };
        }

        public static AuthSettings ToResponse(this AuthSettingsEntity authSettings)
        {
            return new AuthSettings
            {
                AuthServerUrl = authSettings.AuthServerUrl,
                ClientId = authSettings.ClientId,
                ClientSecret = authSettings.ClientId,
                Realms = authSettings.Realms,
                PolicyName = authSettings.PolicyName,
                Roles = authSettings.Roles,
                Scopes = authSettings.Scopes
            };
        }
    }
}
