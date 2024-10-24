using Feijuca.Auth.Domain.Interfaces;
using MediatR;

namespace Feijuca.Auth.Application.Commands.Group
{
    public class CreateGroupCommandHandler(IGroupRepository groupRepository) : IRequestHandler<CreateGroupCommand, Common.Models.Result>
    {
        private readonly IGroupRepository _groupRepository = groupRepository;

        public async Task<Common.Models.Result> Handle(CreateGroupCommand request, CancellationToken cancellationToken)
        {
            var result = await _groupRepository.CreateAsync(request.AddGroupRequest.Name, request.AddGroupRequest.Attributes, cancellationToken);
            if (result.IsSuccess)
            {
                return Common.Models.Result.Success();
            }

            return Common.Models.Result.Failure(result.Error);
        }
    }
}
