using Feijuca.Auth.Common.Models;
using Feijuca.Auth.Models;
using MediatR;

namespace Feijuca.Auth.Application.Commands.Config
{
    public record AddConfigCommand(string Tenant, AuthSettings Request) : IRequest<Result<bool>>;
}
