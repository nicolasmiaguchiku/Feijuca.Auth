namespace TokenManager.Application.Requests.GroupUsers
{
    public record RemoveUserFromGroupRequest(Guid UserId, Guid GroupId);
}
