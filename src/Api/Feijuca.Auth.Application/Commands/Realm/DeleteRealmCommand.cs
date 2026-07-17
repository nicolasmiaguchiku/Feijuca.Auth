using LiteBus.Commands.Abstractions;
using Feijuca.Auth.Models;

namespace Feijuca.Auth.Application.Commands.Realm
{
    public record DeleteRealmCommand(string RealmName) : ICommand<Result<bool>>;
}
