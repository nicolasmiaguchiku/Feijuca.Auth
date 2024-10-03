using MediatR;
using TokenManager.Application.Requests.GroupUsers;
using TokenManager.Application.Responses;
using TokenManager.Common.Models;

namespace TokenManager.Application.Queries.GroupUser
{
    public record GetUsersGroupQuery(string Tenant, GetUsersGroupRequest GetUsersGroupRequest) : IRequest<Result<PagedResult<UserGroupResponse>>>;
}
