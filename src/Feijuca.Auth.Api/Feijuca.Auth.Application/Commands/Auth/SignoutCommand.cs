using Common.Models;
using MediatR;

namespace Application.Commands.Auth
{
    public record SignoutCommand(string Tenant, string RefreshToken) : IRequest<Result<bool>>;
}
