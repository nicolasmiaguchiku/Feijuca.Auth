using LiteBus.Commands.Abstractions;
using Feijuca.Auth.Models;

namespace Feijuca.Auth.Application.Commands.User
{
    public record SignoutCommand(string RefreshToken) : ICommand<Result<bool>>;
}
