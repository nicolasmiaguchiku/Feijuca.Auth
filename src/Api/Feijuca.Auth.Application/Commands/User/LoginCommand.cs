using Feijuca.Auth.Application.Requests.Auth;
using Feijuca.Auth.Http.Responses;
using Mattioli.Configurations.Models;
using MediatR;

namespace Feijuca.Auth.Application.Commands.User
{
    public record LoginCommand(string Tenant, LoginUserRequest LoginUser) : IRequest<Result<TokenDetailsResponse>>;
}
