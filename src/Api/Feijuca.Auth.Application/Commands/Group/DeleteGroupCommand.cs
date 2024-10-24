using Feijuca.Auth.Common.Models;

using MediatR;

namespace Feijuca.Auth.Application.Commands.Group
{
    public record DeleteGroupCommand(Guid Id) : IRequest<Result<bool>>;
}
