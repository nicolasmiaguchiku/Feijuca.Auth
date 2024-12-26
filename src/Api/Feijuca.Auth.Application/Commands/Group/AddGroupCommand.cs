using Feijuca.Auth.Common.Models;
using Feijuca.Auth.Application.Requests.User;
using MediatR;

namespace Feijuca.Auth.Application.Commands.Group
{
    public record AddGroupCommand(AddGroupRequest AddGroupRequest) : IRequest<Result<bool>>;
}
