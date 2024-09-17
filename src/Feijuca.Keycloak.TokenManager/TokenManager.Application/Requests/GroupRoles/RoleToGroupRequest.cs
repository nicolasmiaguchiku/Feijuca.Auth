namespace TokenManager.Application.Requests.GroupRoles
{
    public record RoleToGroupRequest(Guid ClientId, Guid GroupId, Guid RoleId);
}
