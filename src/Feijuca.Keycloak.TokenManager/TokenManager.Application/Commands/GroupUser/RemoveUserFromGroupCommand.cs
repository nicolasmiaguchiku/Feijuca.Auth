using Common.Models;
using MediatR;

namespace Application.Commands.GroupUser
{
    public record RemoveUserFromGroupCommand(string Tenant, Guid UserId, Guid GroupId) : IRequest<Result<bool>>;
}
