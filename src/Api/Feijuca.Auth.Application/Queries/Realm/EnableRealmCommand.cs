using Feijuca.Auth.Application.Requests.Realm;
using LiteBus.Commands.Abstractions;
using Feijuca.Auth.Models;

namespace Feijuca.Auth.Application.Queries.Realm
{
    public record EnableRealmCommand(EnableRealmRequest EnableRealmRequest) : ICommand<Result<bool>>;
}
