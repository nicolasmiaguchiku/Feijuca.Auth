using Feijuca.Auth.Application.Requests.ClientScopes;
using Mattioli.Configurations.Models;
using MediatR;

namespace Feijuca.Auth.Application.Commands.ClientScopes
{
    public record AddClientScopesCommand(IEnumerable<AddClientScopesRequest> AddClientScopesRequest) : IRequest<Result<bool>>;
}
