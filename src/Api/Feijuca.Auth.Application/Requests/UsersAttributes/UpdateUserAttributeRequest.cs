namespace Feijuca.Auth.Application.Requests.UsersAttributes
{
    public record UpdateUserAttributeRequest(Dictionary<string, string[]> Attributes);
}
