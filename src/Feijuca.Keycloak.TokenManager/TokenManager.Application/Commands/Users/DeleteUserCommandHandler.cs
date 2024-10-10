using Common.Errors;
using Common.Models;
using Domain.Interfaces;
using MediatR;

namespace Application.Commands.Users
{
    public class DeleteUserCommandHandler(IUserRepository UserRepository) : IRequestHandler<DeleteUserCommand, Result<bool>>
    {
        private readonly IUserRepository _userRepository = UserRepository;

        public async Task<Result<bool>> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
        {
            var result = await _userRepository.DeleteAsync(request.Tenant, request.Id);

            if (result.IsSuccess)
            {
                return Result<bool>.Success(true);
            }

            return Result<bool>.Failure(UserErrors.DeletionUserError);
        }
    }
}
