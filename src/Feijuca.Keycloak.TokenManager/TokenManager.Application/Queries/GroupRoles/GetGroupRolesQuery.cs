using Application.Responses;
using Common.Models;
using MediatR;

namespace Application.Queries.GroupRoles
{
    public record GetGroupRolesQuery(string Tenant, Guid GroupId) : IRequest<Result<IEnumerable<GroupRolesResponse>>>;
}
