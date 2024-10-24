using Feijuca.Auth.Common.Models;

using Feijuca.Auth.Application.Requests.GroupRoles;

using MediatR;

namespace Feijuca.Auth.Application.Commands.GroupRoles
{
    public record RemoveRoleFromGroupCommand(Guid GroupId, RoleToGroupRequest RemoveRoleFromGroupRequest) : IRequest<Result<bool>>;
}
