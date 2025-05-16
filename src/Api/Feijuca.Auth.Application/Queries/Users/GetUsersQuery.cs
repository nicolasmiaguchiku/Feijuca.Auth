using Feijuca.Auth.Common.Models;
using Feijuca.Auth.Application.Requests.User;
using Feijuca.Auth.Application.Responses;
using MediatR;
using Feijuca.Auth.Http.Responses;

namespace Feijuca.Auth.Application.Queries.Users
{
    public record GetUsersQuery(GetUsersRequest GetUsersRequest) : IRequest<Result<PagedResult<UserResponse>>>;
}
