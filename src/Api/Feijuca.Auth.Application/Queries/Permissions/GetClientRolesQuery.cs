using Mattioli.Configurations.Models;

using MediatR;
using Feijuca.Auth.Application.Responses;

namespace Feijuca.Auth.Application.Queries.Permissions
{
    public record GetClientRolesQuery() : IRequest<Result<IEnumerable<ClientRoleResponse>>>;
}
