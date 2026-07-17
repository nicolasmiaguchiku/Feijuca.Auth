using Feijuca.Auth.Application.Requests.RealmAttributes;
using LiteBus.Commands.Abstractions;
using Feijuca.Auth.Models;

namespace Feijuca.Auth.Application.Commands.RealmAttributes;

public record AddRealmAttributesCommand(AddRealmAttributesRequest AddRealmAttributesRequest) : ICommand<Result>;
