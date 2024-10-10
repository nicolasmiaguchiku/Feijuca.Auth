namespace Application.Requests.User
{
    public record AddGroupRequest(string Name, Dictionary<string, string[]> Attributes);
}
