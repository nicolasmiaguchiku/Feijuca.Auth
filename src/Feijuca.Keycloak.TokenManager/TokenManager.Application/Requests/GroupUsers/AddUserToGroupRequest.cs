namespace TokenManager.Application.Requests.GroupUsers
{
    public record AddUserToGroupRequest(Guid UserId, Guid GroupId);
}
