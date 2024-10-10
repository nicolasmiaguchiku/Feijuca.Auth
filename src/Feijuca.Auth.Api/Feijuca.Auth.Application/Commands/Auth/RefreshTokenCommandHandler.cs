using Application.Mappers;
using Application.Responses;
using Common.Models;
using Domain.Interfaces;
using MediatR;

namespace Application.Commands.Auth
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
