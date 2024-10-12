using Feijuca.Auth.Common.Models;

using Feijuca.Auth.Application.Responses;

using MediatR;

namespace Feijuca.Auth.Application.Queries.Permissions
{
    public record GetRolesQuery(string Tenant) : IRequest<Result<IEnumerable<ClientRoleResponse>>>;
}
