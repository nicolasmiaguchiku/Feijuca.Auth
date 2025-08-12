using Mattioli.Configurations.Models;
using MediatR;
using Feijuca.Auth.Application.Responses;

namespace Feijuca.Auth.Application.Queries.Groups
{
    public record GetAllGroupsQuery(bool NotDisplayInternalGroups) : IRequest<Result<IEnumerable<GroupResponse>>>;
}
