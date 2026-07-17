using LiteBus.Commands.Abstractions;
using Feijuca.Auth.Models;

namespace Feijuca.Auth.Application.Commands.ClientScopeProtocol
{
    public record AddClientScopeAudienceProtocolMapperCommand(string ClientScopeId) : ICommand<Result<bool>>;
}
