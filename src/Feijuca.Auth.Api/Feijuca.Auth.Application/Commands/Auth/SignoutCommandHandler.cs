using Common.Errors;
using Common.Models;
using Domain.Interfaces;
using MediatR;

namespace Application.Commands.Auth
{
    public class SignoutCommandHandler(IUserRepository userRepository) : IRequestHandler<SignoutCommand, Result<bool>>
    {
        private readonly IUserRepository _userRepository = userRepository;
        public async Task<Result<bool>> Handle(SignoutCommand request, CancellationToken cancellationToken)
        {
            var result = await _userRepository.SignoutAsync(request.RefreshToken);
            if (result.IsSuccess)
            {
                return Result<bool>.Success(true);
            }

            return Result<bool>.Failure(UserErrors.InvalidRefreshToken);
        }
    }
}
