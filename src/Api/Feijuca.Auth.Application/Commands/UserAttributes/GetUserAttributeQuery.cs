using Feijuca.Auth.Application.Responses;
using Feijuca.Auth.Common.Models;
using MediatR;

namespace Feijuca.Auth.Application.Commands.UserAttributes
{
    public record GetUserAttributeQuery(string Username) : IRequest<Result<Dictionary<string, string[]>>>;
}
