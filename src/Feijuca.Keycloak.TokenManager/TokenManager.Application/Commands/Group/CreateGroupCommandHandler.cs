using MediatR;

using TokenManager.Application.Commands.Groups;
using TokenManager.Common.Models;
using TokenManager.Domain.Interfaces;

namespace TokenManager.Application.Commands.Group
{
    public class CreateGroupCommandHandler(IGroupRepository groupRepository) : IRequestHandler<CreateGroupCommand, Result>
    {
        private readonly IGroupRepository _groupRepository = groupRepository;

        public async Task<Result> Handle(CreateGroupCommand request, CancellationToken cancellationToken)
        {
            var result = await _groupRepository.CreateAsync(request.Tenant, request.AddGroupRequest.Name, request.AddGroupRequest.Attributes);
            if (result.IsSuccess)
            {
                return Result.Success();
            }

            return Result.Failure(result.Error);
        }
    }
}
