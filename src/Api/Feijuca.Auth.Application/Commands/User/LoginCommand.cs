using Feijuca.Auth.Application.Requests.Auth;
using Feijuca.Auth.Common.Models;
using Feijuca.Auth.Http.Responses;
using MediatR;

namespace Feijuca.Auth.Application.Commands.User
{
    public record LoginCommand(LoginUserRequest LoginUser) : IRequest<Result<TokenDetailsResponse>>;
}
