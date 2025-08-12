using Mattioli.Configurations.Models;
using MediatR;
using Feijuca.Auth.Application.Responses;

namespace Feijuca.Auth.Application.Queries.GroupRoles
{
    public record GetGroupRolesQuery(string GroupId) : IRequest<Result<IEnumerable<GroupRolesResponse>>>;
}
