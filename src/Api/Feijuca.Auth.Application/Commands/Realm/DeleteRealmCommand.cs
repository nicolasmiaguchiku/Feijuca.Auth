using Mattioli.Configurations.Models;
using MediatR;

namespace Feijuca.Auth.Application.Commands.Realm
{
    public record DeleteRealmCommand(string RealmName) : IRequest<Result<bool>>;
}
