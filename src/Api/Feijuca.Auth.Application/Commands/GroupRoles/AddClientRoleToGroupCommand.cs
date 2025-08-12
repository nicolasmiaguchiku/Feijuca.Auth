using Mattioli.Configurations.Models;
using Feijuca.Auth.Application.Requests.GroupRoles;
using MediatR;

namespace Feijuca.Auth.Application.Commands.GroupRoles
{
    public record AddClientRoleToGroupCommand(string GroupId, AddClientRoleToGroupRequest AddRoleToGroupRequest) : IRequest<Result<bool>>;
}
