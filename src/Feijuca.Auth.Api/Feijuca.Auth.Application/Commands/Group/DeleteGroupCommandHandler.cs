using Common.Errors;
using Common.Models;
using Domain.Interfaces;
using MediatR;

namespace Application.Commands.Group
{
    public class DeleteGroupCommandHandler(IGroupRepository groupRepository) : IRequestHandler<DeleteGroupCommand, Result<bool>>
    {
        private readonly IGroupRepository _groupRepository = groupRepository;

        public async Task<Result<bool>> Handle(DeleteGroupCommand request, CancellationToken cancellationToken)
        {
            var result = await _groupRepository.DeleteAsync(request.Tenant, request.Id);

            if (result.IsSuccess)
            {
                return Result<bool>.Success(true);
            }

            return Result<bool>.Failure(GroupErrors.DeletionGroupError);
        }
    }
}
