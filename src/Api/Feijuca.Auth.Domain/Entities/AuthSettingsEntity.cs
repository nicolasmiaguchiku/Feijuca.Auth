using Feijuca.Auth.Common.Models;

using MongoDB.Bson.Serialization.Attributes;

namespace Feijuca.Auth.Domain.Entities
{
    [BsonIgnoreExtraElements]
    public class KeycloakSettingsEntity : KeycloakSettings
    {
        [BsonId]
        public Guid Id { get; set; }
    }
}
