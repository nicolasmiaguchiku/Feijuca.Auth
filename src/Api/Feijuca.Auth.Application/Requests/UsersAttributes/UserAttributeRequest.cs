namespace Feijuca.Auth.Application.Requests.UsersAttributes
{
    public record UserAttributeRequest(Dictionary<string, string[]> Attributes);
}
