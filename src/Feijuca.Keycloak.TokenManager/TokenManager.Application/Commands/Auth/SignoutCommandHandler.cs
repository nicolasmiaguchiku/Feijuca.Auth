using MediatR;
using TokenManager.Common.Errors;
using TokenManager.Common.Models;
using TokenManager.Domain.Interfaces;

namespace TokenManager.Application.Commands.Auth
{
    public class SignoutCommandHandler(IAuthRepository authRepository) : IRequestHandler<SignoutCommand, Result<bool>>
    {
        private readonly IAuthRepository _authRepository = authRepository;
        public async Task<Result<bool>> Handle(SignoutCommand request, CancellationToken cancellationToken)
        {
            var result = await _authRepository.SignoutAsync(request.Tenant, request.RefreshToken);
            if (result.IsSuccess)
            {
                return Result<bool>.Success(true);
            }

            return Result<bool>.Failure(UserErrors.InvalidRefreshToken);
        }
    }
}
