using Feijuca.Auth.Common.Models;
using MediatR;

namespace Feijuca.Auth.Application.Commands.ClientScopeProtocol
{
    public record AddClientScopeAudienceProtocolMapperCommand(string ClientScopeId) : IRequest<Result<bool>>;
}
