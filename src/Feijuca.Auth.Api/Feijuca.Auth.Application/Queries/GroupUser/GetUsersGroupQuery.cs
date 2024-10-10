using Application.Requests.GroupUsers;
using Application.Responses;
using Common.Models;
using MediatR;

namespace Application.Queries.GroupUser
{
    public record GetUsersGroupQuery(string Tenant, GetUsersGroupRequest GetUsersGroupRequest) : IRequest<Result<PagedResult<UserGroupResponse>>>;
}
