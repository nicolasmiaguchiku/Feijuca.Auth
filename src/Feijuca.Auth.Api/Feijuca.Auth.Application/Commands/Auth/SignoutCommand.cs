using Feijuca.Auth.Common.Models;

using MediatR;

namespace Feijuca.Auth.Application.Commands.Auth
{
    public record SignoutCommand(string Tenant, string RefreshToken) : IRequest<Result<bool>>;
}
