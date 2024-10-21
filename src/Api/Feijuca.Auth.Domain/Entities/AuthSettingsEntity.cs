using Feijuca.Auth.Common.Models;

using MongoDB.Bson.Serialization.Attributes;

namespace Feijuca.Auth.Domain.Entities
{
    public class KeycloakSettingsEntity : KeycloakSettings
    {
        [BsonId]
        public Guid Id { get; set; }
    }
}
