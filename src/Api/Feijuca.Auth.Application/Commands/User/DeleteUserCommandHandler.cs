using Feijuca.Auth.Common.Errors;
using Mattioli.Configurations.Models;
using Feijuca.Auth.Domain.Interfaces;

using MediatR;

namespace Feijuca.Auth.Application.Commands.User
{
    public class DeleteUserCommandHandler(IUserRepository UserRepository) : IRequestHandler<DeleteUserCommand, Result<bool>>
    {
        private readonly IUserRepository _userRepository = UserRepository;

        public async Task<Result<bool>> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
        {
            var result = await _userRepository.DeleteAsync(request.Id, cancellationToken);

            if (result.IsSuccess)
            {
                return Result<bool>.Success(true);
            }

            return Result<bool>.Failure(UserErrors.DeletionUserError);
        }
    }
}
