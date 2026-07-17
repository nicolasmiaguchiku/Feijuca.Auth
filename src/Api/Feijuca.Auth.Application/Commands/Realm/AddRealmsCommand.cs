using Feijuca.Auth.Application.Requests.Realm;
using LiteBus.Commands.Abstractions;
using Feijuca.Auth.Models;

namespace Feijuca.Auth.Application.Commands.Realm
{
    public record AddRealmsCommand(IEnumerable<AddRealmRequest> AddRealmsRequest) : ICommand<Result<bool>>;
}
