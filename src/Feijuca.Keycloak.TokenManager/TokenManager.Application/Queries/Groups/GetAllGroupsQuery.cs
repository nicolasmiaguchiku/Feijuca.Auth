using Application.Responses;
using Common.Models;
using MediatR;

namespace Application.Queries.Groups
{
    public record GetAllGroupsQuery(string Tenant) : IRequest<Result<IEnumerable<GroupResponse>>>;
}
