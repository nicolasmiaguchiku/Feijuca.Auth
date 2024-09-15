namespace TokenManager.Application.Requests.RoleGroup
{
    public record AddRoleToGroupRequest(string ClientId, string GroupId, string RoleId);
}
