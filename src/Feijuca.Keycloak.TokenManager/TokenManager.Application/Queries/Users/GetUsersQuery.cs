using MediatR;
using TokenManager.Application.Requests.User;
using TokenManager.Application.Responses;
using TokenManager.Common.Models;

namespace TokenManager.Application.Queries.Users
{
    public record GetUsersQuery(string Tenant, GetUsersRequest GetUsersRequest) : IRequest<Result<PagedResult<UserResponse>>>;
}
