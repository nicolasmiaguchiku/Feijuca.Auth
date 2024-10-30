using Feijuca.Auth.Common.Models;
using Feijuca.Auth.Domain.Interfaces;
using MediatR;

namespace Feijuca.Auth.Application.Commands.Group
{
    public class CreateGroupCommandHandler(IGroupRepository groupRepository) : IRequestHandler<CreateGroupCommand, Result>
    {
        private readonly IGroupRepository _groupRepository = groupRepository;

        public async Task<Result> Handle(CreateGroupCommand request, CancellationToken cancellationToken)
        {
            var result = await _groupRepository.CreateAsync(request.AddGroupRequest.Name, request.AddGroupRequest.Attributes, cancellationToken);
            if (result.IsSuccess)
            {
                return Result.Success();
            }

            return Result.Failure(result.Error);
        }
    }
}
