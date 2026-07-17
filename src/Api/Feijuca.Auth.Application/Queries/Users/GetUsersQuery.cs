using Feijuca.Auth.Application.Responses;
using Feijuca.Auth.Http.Responses;
using LiteBus.Queries.Abstractions;
using Feijuca.Auth.Models;
using Feijuca.Auth.Application.Requests.User;

namespace Feijuca.Auth.Application.Queries.Users
{
    public record GetUsersQuery(GetUsersRequest GetUsersRequest) : IQuery<Result<Responses.PagedResult<UserResponse>>>;
}
