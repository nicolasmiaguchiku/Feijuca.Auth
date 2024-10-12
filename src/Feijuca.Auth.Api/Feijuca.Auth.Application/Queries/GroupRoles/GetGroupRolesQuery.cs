using Feijuca.Auth.Common.Models;

using Feijuca.Auth.Application.Responses;

using MediatR;

namespace Feijuca.Auth.Application.Queries.GroupRoles
{
    public record GetGroupRolesQuery(string Tenant, Guid GroupId) : IRequest<Result<IEnumerable<GroupRolesResponse>>>;
}
