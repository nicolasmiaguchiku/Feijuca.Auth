using MediatR;

using TokenManager.Application.Responses;
using TokenManager.Common.Models;

namespace TokenManager.Application.Queries.Users
{
    public record GetAllUsersQuery(string Tenant) : IRequest<Result<IEnumerable<UserResponse>>>;
}
