using MediatR;
using TokenManager.Application.Responses;
using TokenManager.Common.Models;
using TokenManager.Domain.Entities;

namespace TokenManager.Application.Queries.Groups
{
    public record GetAllGroupsQuery(string Tenant) : IRequest<Result<IEnumerable<GroupResponse>>>;
}
