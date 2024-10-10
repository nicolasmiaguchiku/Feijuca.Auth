using Common.Models;
using MediatR;

namespace Application.Commands.Group
{
    public record DeleteGroupCommand(string Tenant, Guid Id) : IRequest<Result<bool>>;
}
