using MediatR;
using TokenManager.Common.Models;

namespace TokenManager.Application.Commands.UserGroup
{
    public record AddUserToGroupCommand(string Tenant, Guid UserId, Guid GroupId) : IRequest<Result<bool>>;
}
