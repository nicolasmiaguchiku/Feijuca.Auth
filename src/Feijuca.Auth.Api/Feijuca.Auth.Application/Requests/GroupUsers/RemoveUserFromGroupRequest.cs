namespace Application.Requests.GroupUsers
{
    public record RemoveUserFromGroupRequest(Guid UserId, Guid GroupId);
}
