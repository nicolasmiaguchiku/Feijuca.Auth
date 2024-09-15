using MediatR;

using TokenManager.Common.Models;
using TokenManager.Domain.Interfaces;

namespace TokenManager.Application.Commands.Group
{
    public class CreateGroupCommandHandler(IGroupRepository groupRepository) : IRequestHandler<CreateGroupCommand, Common.Models.Result>
    {
        private readonly IGroupRepository _groupRepository = groupRepository;

        public async Task<Common.Models.Result> Handle(CreateGroupCommand request, CancellationToken cancellationToken)
        {
            var result = await _groupRepository.CreateAsync(request.Tenant, request.AddGroupRequest.Name, request.AddGroupRequest.Attributes);
            if (result.IsSuccess)
            {
                return Common.Models.Result.Success();
            }

            return Common.Models.Result.Failure(result.Error);
        }
    }
}
