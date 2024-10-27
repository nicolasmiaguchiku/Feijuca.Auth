using Feijuca.Auth.Application.Responses;
using Feijuca.Auth.Common.Models;
using MediatR;

namespace Feijuca.Auth.Application.Commands.Users
{
    public record RefreshTokenCommand(string RefreshToken) : IRequest<Result<TokenDetailsResponse>>;
}
