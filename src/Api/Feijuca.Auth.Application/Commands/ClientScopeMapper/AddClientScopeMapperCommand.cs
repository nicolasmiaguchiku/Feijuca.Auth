using LiteBus.Commands.Abstractions;
using Feijuca.Auth.Models;

namespace Feijuca.Auth.Application.Commands.ClientScopeMapper
{
    public record AddClientScopeMapperCommand(string ClientScopeId, string UserPropertyName, string ClaimName) : ICommand<Result<bool>>;
}
