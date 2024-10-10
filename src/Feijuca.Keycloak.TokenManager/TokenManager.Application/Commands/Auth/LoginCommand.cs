using Application.Requests.Auth;
using Application.Responses;
using Common.Models;
using MediatR;

namespace Application.Commands.Auth
{
    public record LoginCommand(string Tenant, LoginUserRequest LoginUser) : IRequest<Result<TokenDetailsResponse>>;
}
