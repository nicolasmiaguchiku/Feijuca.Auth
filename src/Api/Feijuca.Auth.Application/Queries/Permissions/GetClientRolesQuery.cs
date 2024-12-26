using Feijuca.Auth.Common.Models;

using Feijuca.Auth.Application.Responses;

using MediatR;

namespace Feijuca.Auth.Application.Queries.Permissions
{
    public record GetClientRolesQuery() : IRequest<Result<IEnumerable<ClientRoleResponse>>>;
}
