using Feijuca.Auth.Common.Errors;
using Feijuca.Auth.Domain.Interfaces;
using LiteBus.Commands.Abstractions;
using Feijuca.Auth.Models;

namespace Feijuca.Auth.Application.Commands.Group
{
    public class UpdateGroupNameCommandHandler(IGroupRepository _groupRepository) : ICommandHandler<UpdateGroupNameCommand, Result<bool>>
    {
        public async Task<Result<bool>> HandleAsync(UpdateGroupNameCommand request, CancellationToken cancellationToken = default)
        {
            var groups = await _groupRepository.GetAllAsync(cancellationToken);

            if (groups.IsFailure)
            {
                return Result<bool>.Failure(GroupErrors.GetGroupsError);
            }

            var group = groups.Data.FirstOrDefault(g => g.Id == request.GroupId.ToString());

            if (group is null)
            {
                return Result<bool>.Failure(GroupErrors.NotFoundError);
            }

            group.Name = request.Request.Name;

            var result = await _groupRepository.UpdateAsync(group, cancellationToken);

            if (result.IsSuccess)
            {
                return Result<bool>.Success(true);
            }

            return Result<bool>.Failure(result.Error);
        }
    }
}
