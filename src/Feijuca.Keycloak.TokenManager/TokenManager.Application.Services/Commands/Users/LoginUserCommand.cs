using MediatR;

using TokenManager.Application.Services.Requests.User;
using TokenManager.Application.Services.Responses;
using TokenManager.Common.Models;

namespace TokenManager.Application.Commands.Users
{
    public record LoginUserCommand(string Tenant, LoginUserRequest LoginUser) : IRequest<Result<TokenDetailsResponse>>;
}
