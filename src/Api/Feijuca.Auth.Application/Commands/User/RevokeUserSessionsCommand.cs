using Feijuca.Auth.Common.Models;
using MediatR;

namespace Feijuca.Auth.Application.Commands.User
{
    public record RevokeUserSessionsCommand(Guid UserId) : IRequest<Result>;
}
