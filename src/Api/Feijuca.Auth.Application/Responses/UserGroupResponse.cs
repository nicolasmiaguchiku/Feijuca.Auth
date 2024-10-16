namespace Feijuca.Auth.Application.Responses
{
    public record UserGroupResponse(GroupResponse Group, IEnumerable<UserResponse> Users);
}
