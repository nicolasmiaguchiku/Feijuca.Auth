using MediatR;

using TokenManager.Application.Requests.GroupRoles;
using TokenManager.Common.Models;

namespace TokenManager.Application.Commands.GroupRoles
{
    public record AddRoleToGroupCommand(string Tenant, RoleToGroupRequest AddRoleToGroupRequest) : IRequest<Result<bool>>;
}
