using MediatR;
using TokenManager.Application.Requests.RoleGroup;
using TokenManager.Common.Models;

namespace TokenManager.Application.Commands.RoleGroup
{
    public record AddRoleToGroupCommand(string Tenant, AddRoleToGroupRequest AddRoleToGroupRequest) : IRequest<Result<bool>>;
}
