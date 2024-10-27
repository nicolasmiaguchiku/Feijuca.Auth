using Feijuca.Auth.Common.Models;
using MediatR;

namespace Feijuca.Auth.Application.Commands.Users
{
    public record SignoutCommand(string RefreshToken) : IRequest<Result<bool>>;
}
