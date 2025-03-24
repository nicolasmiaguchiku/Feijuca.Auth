namespace Feijuca.Auth.Application.Requests.UsersAttributes
{
    public record AddUserAttributesRequest(Dictionary<string, string[]> Attributes);
}
