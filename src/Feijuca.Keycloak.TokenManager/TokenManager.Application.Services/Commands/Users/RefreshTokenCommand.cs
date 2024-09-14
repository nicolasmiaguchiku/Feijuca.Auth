using MediatR;

using TokenManager.Application.Services.Responses;
using TokenManager.Common.Models;

namespace TokenManager.Application.Commands.Users
{
    public record RefreshTokenCommand(string Tenant, string RefreshToken) : IRequest<Result<TokenDetailsResponse>>;
}
