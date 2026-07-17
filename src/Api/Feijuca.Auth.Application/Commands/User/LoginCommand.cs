using Feijuca.Auth.Application.Requests.Auth;
using Feijuca.Auth.Http.Responses;
using LiteBus.Commands.Abstractions;
using Feijuca.Auth.Models;

namespace Feijuca.Auth.Application.Commands.User
{
    public record LoginCommand(string Tenant, LoginUserRequest LoginUser) : ICommand<Result<TokenDetailsResponse>>;
}
