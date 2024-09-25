using MediatR;
using TokenManager.Common.Models;

namespace TokenManager.Application.Commands.Auth
{
    public record SignoutCommand(string Tenant, string RefreshToken) : IRequest<Result<bool>>;
}
