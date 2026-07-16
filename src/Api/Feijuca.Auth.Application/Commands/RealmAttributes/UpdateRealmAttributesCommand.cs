using Feijuca.Auth.Application.Requests.RealmAttributes;
using LiteBus.Commands.Abstractions;
using Mattioli.Configurations.Models;

namespace Feijuca.Auth.Application.Commands.RealmAttributes;

public record UpdateRealmAttributesCommand(UpdateRealmAttributesRequest UpdateRealmAttributesRequest) : ICommand<Result>;