using Feijuca.Auth.Common.Models;
using Feijuca.Auth.Application.Requests.Role;
using MediatR;

namespace Feijuca.Auth.Application.Commands.ClientRole
{
    public record AddClientRoleCommand(IEnumerable<AddClientRoleRequest> AddClientRolesRequest) : IRequest<Result<bool>>;
}
