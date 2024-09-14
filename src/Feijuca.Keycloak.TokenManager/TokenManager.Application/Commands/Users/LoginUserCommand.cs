using MediatR;

using TokenManager.Application.Requests.User;
using TokenManager.Application.Responses;
using TokenManager.Common.Models;

namespace TokenManager.Application.Commands.Users
{
    public record LoginUserCommand(string Tenant, LoginUserRequest LoginUser) : IRequest<Result<TokenDetailsResponse>>;
}
