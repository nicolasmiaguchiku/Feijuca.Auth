using Feijuca.Auth.Common.Errors;
using Feijuca.Auth.Common.Models;
using Feijuca.Auth.Domain.Interfaces;
using MediatR;

namespace Feijuca.Auth.Application.Commands.ClientScopeProtocol
{
    public class AddClientScopeAudienceProtocolMapperCommandHandler(IClientScopesRepository clientScopesRepository) : IRequestHandler<AddClientScopeAudienceProtocolMapperCommand, Result<bool>>
    {
        public async Task<Result<bool>> Handle(AddClientScopeAudienceProtocolMapperCommand request, CancellationToken cancellationToken)
        {
            var result = await clientScopesRepository.AddAudienceMapperAsync(request.ClientScopeId, cancellationToken);
            if (result)
            {
                return Result<bool>.Success(true);
            }

            return Result<bool>.Failure(ClientScopesErrors.CreateAudienceMapperProtocolError);
        }
    }
}
