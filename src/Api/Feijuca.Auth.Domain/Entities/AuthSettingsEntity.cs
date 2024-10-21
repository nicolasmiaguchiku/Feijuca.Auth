using Feijuca.Auth.Models;
using MongoDB.Bson.Serialization.Attributes;

namespace Feijuca.Auth.Domain.Entities
{
    public class AuthSettingsEntity : AuthSettings
    {
        [BsonId]
        public Guid Id { get; set; }
    }
}
