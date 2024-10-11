using Application.Mappers;
using Application.Responses;
using Common.Models;
using Domain.Interfaces;
using MediatR;

namespace Application.Commands.Auth
{
    public class RefreshTokenCommandHandler(IUserRepository userRepository) : IRequestHandler<RefreshTokenCommand, Result<TokenDetailsResponse>>
    {
        private readonly IUserRepository _userRepository = userRepository;

        public async Task<Result<TokenDetailsResponse>> Handle(RefreshTokenCommand request, CancellationToken cancellationToken)
        {
            var tokeDetails = await _userRepository.RefreshTokenAsync(request.RefreshToken);
            if (tokeDetails.IsSuccess)
            {
                return tokeDetails.ToTokenResponse();
            }

            return Result<TokenDetailsResponse>.Failure(tokeDetails.Error);
        }
    }
}
