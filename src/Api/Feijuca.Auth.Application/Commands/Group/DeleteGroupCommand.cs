using LiteBus.Commands.Abstractions;
using Feijuca.Auth.Models;

namespace Feijuca.Auth.Application.Commands.Group
{
    public record DeleteGroupCommand(string Id) : ICommand<Result<bool>>;
}
