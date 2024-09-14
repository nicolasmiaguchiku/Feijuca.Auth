namespace TokenManager.Application.Services.Requests.User
{
    public record AddGroupRequest(string Name, Dictionary<string, string[]> Attributes);
}
