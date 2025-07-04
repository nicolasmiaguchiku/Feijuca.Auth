using Feijuca.Auth.Common.Errors;
using Feijuca.Auth.Common.Models;
using Feijuca.Auth.Domain.Interfaces;
using MediatR;

namespace Feijuca.Auth.Application.Commands.ClientScopeMapper
{
    public class AddClientScopeMapperCommandHandler(IClientScopesRepository clientScopesRepository) : IRequestHandler<AddClientScopeMapperCommand, Result<bool>>
    {
        public async Task<Result<bool>> Handle(AddClientScopeMapperCommand request, CancellationToken cancellationToken)
        {
            var result = await clientScopesRepository.AddUserPropertyMapperAsync(request.ClientScopeId, request.UserPropertyName, request.ClaimName, cancellationToken);
            if (result)
            {
                return Result<bool>.Success(true);
            }

            return Result<bool>.Failure(ClientScopesErrors.CreateAudienceMapperProtocolError);
        }
    }
}
