﻿using Mattioli.Configurations.Models;
using MediatR;

namespace Feijuca.Auth.Application.Commands.User
{
    public record SignoutCommand(string RefreshToken) : IRequest<Result<bool>>;
}
