using MediatR;
using TokenManager.Application.Responses;
using TokenManager.Common.Models;

namespace TokenManager.Application.Queries.Permissions
{
    public record GetRolesQuery(string Tenant) : IRequest<Result<IEnumerable<ClientRoleResponse>>>;
}
