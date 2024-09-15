namespace TokenManager.Application.Responses
{
    public record GroupRolesResponse(string Id, string Client, IEnumerable<RoleResponse> Mappings);
}
