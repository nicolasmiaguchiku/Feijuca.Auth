using Feijuca.Auth.Application.Requests.Auth;
using Feijuca.Auth.Application.Responses;
using Feijuca.Auth.Common.Models;

using MediatR;

namespace Feijuca.Auth.Application.Commands.Auth
{
    public record LoginCommand(string Tenant, LoginUserRequest LoginUser) : IRequest<Result<TokenDetailsResponse>>;
}
