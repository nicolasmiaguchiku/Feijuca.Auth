using Feijuca.Auth.Application.Responses;
using Feijuca.Auth.Common.Models;

using MediatR;

namespace Feijuca.Auth.Application.Commands.Auth
{
    public record RefreshTokenCommand(string RefreshToken) : IRequest<Result<TokenDetailsResponse>>;
}
