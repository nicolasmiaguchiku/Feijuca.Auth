using Common.Models;
using Domain.Interfaces;
using MediatR;

namespace Application.Commands.GroupUser
{
    public class AddUserToGroupCommandHandler(IGroupUsersRepository userGroupRepository) : IRequestHandler<AddUserToGroupCommand, Result<bool>>
    {
        private readonly IGroupUsersRepository _userGroupRepository = userGroupRepository;

        public async Task<Result<bool>> Handle(AddUserToGroupCommand request, CancellationToken cancellationToken)
        {
            var result = await _userGroupRepository.AddUserToGroupAsync(request.Tenant, request.UserId, request.GroupId);

            if (result.IsSuccess)
            {
                return Result<bool>.Success(result.Response);
            }

            return Result<bool>.Failure(result.Error);
        }
    }
}
