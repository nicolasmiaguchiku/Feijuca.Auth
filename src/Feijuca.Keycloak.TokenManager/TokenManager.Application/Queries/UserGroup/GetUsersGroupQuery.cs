using MediatR;
using TokenManager.Application.Responses;
using TokenManager.Common.Models;

namespace TokenManager.Application.Queries.UserGroup
{
    public record GetUsersGroupQuery(string Tenant, string GroupName) : IRequest<Result<UserGroupResponse>>;
}
