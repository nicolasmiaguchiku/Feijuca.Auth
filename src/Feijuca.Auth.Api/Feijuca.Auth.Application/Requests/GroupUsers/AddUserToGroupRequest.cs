namespace Feijuca.Auth.Application.Requests.GroupUsers
{
    public record AddUserToGroupRequest(Guid UserId, Guid GroupId);
}
