﻿using Feijuca.Auth.Application.Requests.UsersAttributes;
using Mattioli.Configurations.Models;
using MediatR;

namespace Feijuca.Auth.Application.Commands.UserAttributes
{
    public record AddUserAttributeCommand(string UserName, AddUserAttributesRequest AddUserAttributesRequest) : IRequest<Result<bool>>;
}
