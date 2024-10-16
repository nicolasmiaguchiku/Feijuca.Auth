using Feijuca.Auth.Common.Models;

using Feijuca.Auth.Application.Responses;

using MediatR;

namespace Feijuca.Auth.Application.Queries.Groups
{
    public record GetAllGroupsQuery(string Tenant) : IRequest<Result<IEnumerable<GroupResponse>>>;
}
