using LiteBus.Commands.Abstractions;
using Feijuca.Auth.Models;

namespace Feijuca.Auth.Application.Commands.User
{
    public record RevokeUserSessionsCommand(Guid UserId) : ICommand<Result>;
}
