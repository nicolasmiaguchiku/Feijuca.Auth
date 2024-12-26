namespace Feijuca.Auth.Application.Requests.GroupRoles
{
    public record AddClientRoleToGroupRequest(string ClientId, Guid RoleId);
}
