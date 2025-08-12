using Mattioli.Configurations.Models;

using Feijuca.Auth.Application.Requests.GroupUsers;

using MediatR;
using Feijuca.Auth.Application.Responses;

namespace Feijuca.Auth.Application.Queries.GroupUser
{
    public record GetUsersGroupQuery(GetUsersGroupRequest GetUsersGroupRequest) : IRequest<Result<PagedResult<UserGroupResponse>>>;
}
