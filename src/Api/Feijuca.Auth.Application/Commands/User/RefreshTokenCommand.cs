using Feijuca.Auth.Http.Responses;
using Mattioli.Configurations.Models;
using MediatR;

namespace Feijuca.Auth.Application.Commands.User
{
    public record RefreshTokenCommand(string Tenant, string RefreshToken) : IRequest<Result<TokenDetailsResponse>>;
}
