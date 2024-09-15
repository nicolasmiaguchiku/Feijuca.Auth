using MediatR;

using TokenManager.Application.Requests.RoleGroup;
using TokenManager.Common.Models;

namespace TokenManager.Application.Commands.GroupRoles
{
    public record RemoveRoleFromGroupCommand(string Tenant, RoleToGroupRequest AddRoleToGroupRequest) : IRequest<Result<bool>>;
}
