namespace TokenManager.Application.Requests.RoleGroup
{
    public record RemoveRoleFromGroupRequest(string ClientId, string GroupId, string RoleId);
}
