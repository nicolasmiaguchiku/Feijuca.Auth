using MediatR;

using TokenManager.Application.Mappers;
using TokenManager.Application.Services.Responses;
using TokenManager.Common.Models;
using TokenManager.Domain.Interfaces;

namespace TokenManager.Application.Commands.Users
{
    public class RefreshTokenCommandHandler(IUserRepository userRepository) : IRequestHandler<RefreshTokenCommand, Result<TokenDetailsResponse>>
    {
        private readonly IUserRepository _userRepository = userRepository;

        public async Task<Result<TokenDetailsResponse>> Handle(RefreshTokenCommand request, CancellationToken cancellationToken)
        {
            var tokeDetails = await _userRepository.RefreshTokenAsync(request.Tenant, request.RefreshToken);
            if (tokeDetails.IsSuccess)
            {
                return tokeDetails.ToTokenResponse();
            }

            return Result<TokenDetailsResponse>.Failure(tokeDetails.Error);
        }
    }
}
