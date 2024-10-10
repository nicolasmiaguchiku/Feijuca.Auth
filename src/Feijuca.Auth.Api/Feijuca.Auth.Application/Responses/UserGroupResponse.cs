namespace Application.Responses
{
    public record UserGroupResponse(GroupResponse Group, IEnumerable<UserResponse> Users);
}
