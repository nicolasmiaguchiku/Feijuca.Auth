using MediatR;
using TokenManager.Application.Responses;
using TokenManager.Common.Models;

namespace TokenManager.Application.Commands.Auth
{
    public record RefreshTokenCommand(string Tenant, string RefreshToken) : IRequest<Result<TokenDetailsResponse>>;
}
