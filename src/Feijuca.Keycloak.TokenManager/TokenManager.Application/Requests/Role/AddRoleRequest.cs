namespace Application.Requests.Role
{
    public record AddRoleRequest(Guid ClientId, string Name, string Description);
}
