using Feijuca.Auth.Common.Models;
using FluentValidation;

namespace Feijuca.Auth.Application.Validators
{
    public class KeycloakSettingsValidator : AbstractValidator<KeycloakSettings>
    {
        public KeycloakSettingsValidator()
        {
            RuleFor(x => x.Client)
                .NotNull()
                .WithMessage($"The {nameof(KeycloakSettings.Client)} field is required.");

            RuleFor(x => x.Secrets)
                .NotNull()
                .WithMessage($"The {nameof(KeycloakSettings.Secrets)} field is required.");

            RuleFor(x => x.ServerSettings)
                .NotNull()
                .WithMessage($"The {nameof(KeycloakSettings.ServerSettings)} field is required.");

            RuleFor(x => x.Realm)
                .NotNull()
                .WithMessage($"The {nameof(KeycloakSettings.Realm)} field is required.");
        }
    }
}
