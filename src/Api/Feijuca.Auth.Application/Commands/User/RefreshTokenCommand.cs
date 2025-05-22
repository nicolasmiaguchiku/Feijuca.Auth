using Feijuca.Auth.Common.Models;
using Feijuca.Auth.Http.Responses;
using MediatR;

namespace Feijuca.Auth.Application.Commands.User
{
    public record RefreshTokenCommand(string Tenant, string RefreshToken) : IRequest<Result<TokenDetailsResponse>>;
}
