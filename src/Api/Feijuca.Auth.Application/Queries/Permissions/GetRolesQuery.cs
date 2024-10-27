using Feijuca.Auth.Common.Models;

using Feijuca.Auth.Application.Responses;

using MediatR;

namespace Feijuca.Auth.Application.Queries.Permissions
{
    public record GetRolesQuery() : IRequest<Result<IEnumerable<ClientRoleResponse>>>;
}
