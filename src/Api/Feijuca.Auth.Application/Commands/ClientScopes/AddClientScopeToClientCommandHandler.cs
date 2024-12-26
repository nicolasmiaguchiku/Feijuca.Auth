using Feijuca.Auth.Common.Errors;
using Feijuca.Auth.Common.Models;
using Feijuca.Auth.Domain.Interfaces;
using MediatR;

namespace Feijuca.Auth.Application.Commands.ClientScopes
{
    public class AddClientScopeToClientCommandHandler(IClientScopesRepository clientScopesRepository) : IRequestHandler<AddClientScopeToClientCommand, Result<bool>>
    {
        public async Task<Result<bool>> Handle(AddClientScopeToClientCommand request, CancellationToken cancellationToken)
        {
            var result = await clientScopesRepository.AddClientScopeToClientAsync(
                request.AddClientScopeToClientRequest.ClientId, 
                request.AddClientScopeToClientRequest.ClientScopeId,
                request.AddClientScopeToClientRequest.IsOpticionalScope, 
                cancellationToken);

            if(result)
                return Result<bool>.Success(true);

            return Result<bool>.Failure(ClientErrors.AddClientRoleError);
        }
    }
}
