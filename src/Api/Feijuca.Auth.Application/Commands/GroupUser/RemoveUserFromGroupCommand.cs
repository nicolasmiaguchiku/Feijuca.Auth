using LiteBus.Commands.Abstractions;
using Feijuca.Auth.Models;

namespace Feijuca.Auth.Application.Commands.GroupUser
{
    public record RemoveUserFromGroupCommand(Guid UserId, Guid GroupId) : ICommand<Result<bool>>;
}
