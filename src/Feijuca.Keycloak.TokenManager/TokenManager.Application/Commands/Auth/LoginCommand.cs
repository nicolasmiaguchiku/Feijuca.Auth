using MediatR;
using TokenManager.Application.Requests.Auth;
using TokenManager.Application.Responses;
using TokenManager.Common.Models;

namespace TokenManager.Application.Commands.Auth
{
    public record LoginCommand(string Tenant, LoginUserRequest LoginUser) : IRequest<Result<TokenDetailsResponse>>;
}
