using Feijuca.Auth.Application.Requests.Client;

using MediatR;

namespace Feijuca.Auth.Application.Commands.ClientScopes
{
    public record AddClientScopeToClientCommand(AddClientScopeToClientRequest AddClientScopeToClientRequest) : IRequest<bool>;
}
