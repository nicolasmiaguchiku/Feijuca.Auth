using Feijuca.Auth.Application.Requests.Client;
using Feijuca.Auth.Common.Models;
using MediatR;

namespace Feijuca.Auth.Application.Commands.ClientScopes
{
    public record AddClientScopeToClientCommand(AddClientScopeToClientRequest AddClientScopeToClientRequest) : IRequest<Result<bool>>;
}
