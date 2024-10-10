using Application.Requests.GroupRoles;
using Common.Models;
using MediatR;

namespace Application.Commands.GroupRoles
{
    public record RemoveRoleFromGroupCommand(string Tenant, Guid GroupId, RoleToGroupRequest RemoveRoleFromGroupRequest) : IRequest<Result<bool>>;
}
