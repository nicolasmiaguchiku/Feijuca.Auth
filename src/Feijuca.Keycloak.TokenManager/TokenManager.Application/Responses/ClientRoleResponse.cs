namespace TokenManager.Application.Responses
{
    public record ClientRoleResponse(string ClientName, IEnumerable<RoleResponse> Roles);
    public record RoleResponse(string Id, string Name, string Description);
}
