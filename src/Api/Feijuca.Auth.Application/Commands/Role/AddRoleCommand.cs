using Feijuca.Auth.Common.Models;

using Feijuca.Auth.Application.Requests.Role;

using MediatR;

namespace Feijuca.Auth.Application.Commands.Role
{
    public record AddRoleCommand(AddRoleRequest AddRoleRequest) : IRequest<Result<bool>>;
}
