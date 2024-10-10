namespace Application.Responses
{
    public record ClientRoleResponse(string ClientId, Guid Id, IEnumerable<RoleResponse> Roles);
    public record RoleResponse(Guid Id, string Name, string Description, bool Composite, bool ClientRole, string ContainerId);
}
