using Mattioli.Configurations.Models;
using MediatR;

namespace Feijuca.Auth.Application.Commands.ClientScopeMapper
{
    public record AddClientScopeMapperCommand(string ClientScopeId, string UserPropertyName, string ClaimName) : IRequest<Result<bool>>;
}
