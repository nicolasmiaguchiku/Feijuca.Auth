using Feijuca.Auth.Application.Requests.ClientScopes;
using LiteBus.Commands.Abstractions;
using Feijuca.Auth.Models;

namespace Feijuca.Auth.Application.Commands.ClientScopes
{
    public record AddClientScopesCommand(IEnumerable<AddClientScopesRequest> AddClientScopesRequest) : ICommand<Result<bool>>;
}
