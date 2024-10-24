using Feijuca.Auth.Common.Models;
using MediatR;

namespace Feijuca.Auth.Application.Commands.Users
{
    public record RevokeUserSessionsCommand(Guid UserId) : IRequest<Result>;
}
