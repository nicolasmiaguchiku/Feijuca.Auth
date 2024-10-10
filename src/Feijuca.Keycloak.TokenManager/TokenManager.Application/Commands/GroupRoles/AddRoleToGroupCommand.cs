using Application.Requests.GroupRoles;
using Common.Models;
using MediatR;

namespace Application.Commands.GroupRoles
{
    public record AddRoleToGroupCommand(string Tenant, Guid GroupId, RoleToGroupRequest AddRoleToGroupRequest) : IRequest<Result<bool>>;
}
