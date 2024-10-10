using Application.Responses;
using Common.Models;
using MediatR;

namespace Application.Queries.Permissions
{
    public record GetRolesQuery(string Tenant) : IRequest<Result<IEnumerable<ClientRoleResponse>>>;
}
