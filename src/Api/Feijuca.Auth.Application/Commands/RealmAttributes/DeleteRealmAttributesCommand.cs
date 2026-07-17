using LiteBus.Commands.Abstractions;
using Feijuca.Auth.Models;

namespace Feijuca.Auth.Application.Commands.RealmAttributes;

public record DeleteRealmAttributesCommand(IEnumerable<string> DeleteRealmAttributeRequest) : ICommand<Result>;
