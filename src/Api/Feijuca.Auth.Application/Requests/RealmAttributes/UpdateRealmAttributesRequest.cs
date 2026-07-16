namespace Feijuca.Auth.Application.Requests.RealmAttributes;

public record UpdateRealmAttributesRequest(Dictionary<string, string> Attributes);