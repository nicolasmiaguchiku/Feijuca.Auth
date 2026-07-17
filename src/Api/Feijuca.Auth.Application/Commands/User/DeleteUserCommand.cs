using LiteBus.Commands.Abstractions;
using Feijuca.Auth.Models;

namespace Feijuca.Auth.Application.Commands.User
{
    public record DeleteUserCommand(Guid Id) : ICommand<Result<bool>>;
}
