using Feijuca.Auth.Models;
using LiteBus.Commands.Abstractions;
using Feijuca.Auth.Application.Requests.User;

namespace Feijuca.Auth.Application.Commands.Group
{
    public record AddGroupCommand(AddGroupRequest AddGroupRequest) : ICommand<Result<string>>;
}
