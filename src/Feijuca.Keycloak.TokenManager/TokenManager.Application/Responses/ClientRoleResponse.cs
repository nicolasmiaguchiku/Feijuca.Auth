namespace TokenManager.Application.Responses
{
    public record ClientRoleResponse(string ClientName, string ClientId, IEnumerable<RoleResponse> Roles);
    public record RoleResponse(string Id, string Name, string Description, bool Composite, bool ClientRole, string ContainerId);
}
