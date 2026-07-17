using LiteBus.Commands.Abstractions;
using Feijuca.Auth.Models;
using Feijuca.Auth.Application.Requests.User;

namespace Feijuca.Auth.Application.Commands.User
{
    public record UpdateUserCommand(Guid Id, AddUserRequest UserRequest) : ICommand<Result<bool>>;
}
