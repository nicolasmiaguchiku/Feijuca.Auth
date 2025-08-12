using Feijuca.Auth.Application.Requests.Realm;
using Mattioli.Configurations.Models;
using MediatR;

namespace Feijuca.Auth.Application.Commands.Realm
{
    public record AddRealmsCommand(IEnumerable<AddRealmRequest> AddRealmsRequest) : IRequest<Result<bool>>;
}
