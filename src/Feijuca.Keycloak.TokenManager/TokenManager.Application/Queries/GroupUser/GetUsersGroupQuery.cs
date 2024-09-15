using MediatR;
using TokenManager.Application.Responses;
using TokenManager.Common.Models;

namespace TokenManager.Application.Queries.GroupUser
{
    public record GetUsersGroupQuery(string Tenant, string GroupId) : IRequest<Result<UserGroupResponse>>;
}
