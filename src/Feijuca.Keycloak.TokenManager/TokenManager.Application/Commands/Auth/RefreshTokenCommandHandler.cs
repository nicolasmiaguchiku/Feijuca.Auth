using MediatR;
using TokenManager.Application.Mappers;
using TokenManager.Application.Responses;
using TokenManager.Common.Models;
using TokenManager.Domain.Interfaces;

namespace TokenManager.Application.Commands.Auth
{
    public class RefreshTokenCommandHandler(IAuthRepository authRepository) : IRequestHandler<RefreshTokenCommand, Result<TokenDetailsResponse>>
    {
        private readonly IAuthRepository _authRepository = authRepository;

        public async Task<Result<TokenDetailsResponse>> Handle(RefreshTokenCommand request, CancellationToken cancellationToken)
        {
            var tokeDetails = await _authRepository.RefreshTokenAsync(request.Tenant, request.RefreshToken);
            if (tokeDetails.IsSuccess)
            {
                return tokeDetails.ToTokenResponse();
            }

            return Result<TokenDetailsResponse>.Failure(tokeDetails.Error);
        }
    }
}
