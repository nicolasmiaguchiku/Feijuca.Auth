using MediatR;

using TokenManager.Common.Models;
using TokenManager.Domain.Interfaces;

namespace TokenManager.Application.Commands.GroupUser
{
    public class AddUserToGroupCommandHandler(IGroupUsersRepository userGroupRepository) : IRequestHandler<AddUserToGroupCommand, Result<bool>>
    {
        private readonly IGroupUsersRepository _userGroupRepository = userGroupRepository;

        public async Task<Result<bool>> Handle(AddUserToGroupCommand request, CancellationToken cancellationToken)
        {
            var result = await _userGroupRepository.AddUserToGroupAsync(request.Tenant, request.UserId, request.GroupId);

            if (result.IsSuccess)
            {
                return Result<bool>.Success(result.Data);
            }

            return Result<bool>.Failure(result.Error);
        }
    }
}
