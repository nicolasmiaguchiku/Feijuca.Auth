using Feijuca.Auth.Application.Requests.Auth;
using Feijuca.Auth.Application.Responses;
using Feijuca.Auth.Common.Models;
using MediatR;

namespace Feijuca.Auth.Application.Commands.Users
{
    public record LoginCommand(LoginUserRequest LoginUser) : IRequest<Result<TokenDetailsResponse>>;
}
