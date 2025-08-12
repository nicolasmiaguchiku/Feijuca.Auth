﻿using Mattioli.Configurations.Models;

using MediatR;

namespace Feijuca.Auth.Application.Commands.User
{
    public record DeleteUserCommand(Guid Id) : IRequest<Result<bool>>;
}
