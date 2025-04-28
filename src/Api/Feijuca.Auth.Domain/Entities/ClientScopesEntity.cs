using MongoDB.Bson.Serialization.Attributes;

namespace Feijuca.Auth.Domain.Entities
{
    [BsonIgnoreExtraElements]
    public record ClientScopesEntity(string Name, string Description, bool IncludeInTokenScope);
}
