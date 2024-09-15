using MediatR;

using TokenManager.Common.Models;

namespace TokenManager.Application.Commands.GroupUser
{
    public record AddUserToGroupCommand(string Tenant, Guid UserId, Guid GroupId) : IRequest<Result<bool>>;
}
