using Feijuca.Auth.Common.Models;
using Feijuca.Auth.Application.Requests.GroupRoles;
using MediatR;

namespace Feijuca.Auth.Application.Commands.GroupRoles
{
    public record RemoveRoleFromGroupCommand(string GroupId, AddClientRoleToGroupRequest RemoveRoleFromGroupRequest) : IRequest<Result<bool>>;
}
