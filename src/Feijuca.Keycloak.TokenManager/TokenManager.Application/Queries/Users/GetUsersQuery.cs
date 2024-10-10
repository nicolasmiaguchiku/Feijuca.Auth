using Application.Requests.User;
using Application.Responses;
using Common.Models;
using MediatR;

namespace Application.Queries.Users
{
    public record GetUsersQuery(string Tenant, GetUsersRequest GetUsersRequest) : IRequest<Result<PagedResult<UserResponse>>>;
}
