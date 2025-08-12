using Feijuca.Auth.Application.Requests.Client;
using Mattioli.Configurations.Models;
using MediatR;

namespace Feijuca.Auth.Application.Commands.ClientScopes
{
    public record AddClientScopeToClientCommand(AddClientScopeToClientRequest AddClientScopeToClientRequest) : IRequest<Result<bool>>;
}
