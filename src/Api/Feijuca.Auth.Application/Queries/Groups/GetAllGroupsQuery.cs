using Feijuca.Auth.Common.Models;
using Feijuca.Auth.Application.Responses;
using MediatR;

namespace Feijuca.Auth.Application.Queries.Groups
{
    public record GetAllGroupsQuery(bool NotDisplayInternalGroups) : IRequest<Result<IEnumerable<GroupResponse>>>;
}
