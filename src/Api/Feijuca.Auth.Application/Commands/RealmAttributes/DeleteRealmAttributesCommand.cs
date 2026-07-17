using LiteBus.Commands.Abstractions;
using Mattioli.Configurations.Models;

namespace Feijuca.Auth.Application.Commands.RealmAttributes;

public record DeleteRealmAttributesCommand(IEnumerable<string> DeleteRealmAttributeRequest) : ICommand<Result>;
