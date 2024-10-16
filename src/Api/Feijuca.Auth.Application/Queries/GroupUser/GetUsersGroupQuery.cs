using Feijuca.Auth.Common.Models;

using Feijuca.Auth.Application.Requests.GroupUsers;
using Feijuca.Auth.Application.Responses;

using MediatR;

namespace Feijuca.Auth.Application.Queries.GroupUser
{
    public record GetUsersGroupQuery(string Tenant, GetUsersGroupRequest GetUsersGroupRequest) : IRequest<Result<PagedResult<UserGroupResponse>>>;
}
