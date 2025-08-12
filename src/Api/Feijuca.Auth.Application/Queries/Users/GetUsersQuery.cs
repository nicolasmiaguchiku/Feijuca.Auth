using Feijuca.Auth.Application.Requests.User;
using Feijuca.Auth.Application.Responses;
using Feijuca.Auth.Http.Responses;
using Mattioli.Configurations.Models;
using MediatR;

namespace Feijuca.Auth.Application.Queries.Users
{
    public record GetUsersQuery(GetUsersRequest GetUsersRequest) : IRequest<Result<PagedResult<UserResponse>>>;
}
