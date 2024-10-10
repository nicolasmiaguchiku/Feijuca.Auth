using Application.Responses;
using Common.Models;
using MediatR;

namespace Application.Commands.Auth
{
    public record RefreshTokenCommand(string Tenant, string RefreshToken) : IRequest<Result<TokenDetailsResponse>>;
}
