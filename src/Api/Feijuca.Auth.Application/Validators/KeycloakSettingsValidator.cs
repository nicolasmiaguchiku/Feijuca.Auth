using Feijuca.Auth.Common.Models;
using FluentValidation;

namespace Feijuca.Auth.Application.Validators
{
    public class KeycloakSettingsValidator : AbstractValidator<KeycloakSettings>
    {
        public KeycloakSettingsValidator()
        {
            RuleFor(x => x.Client.ClientId)
                .NotNull()
                .NotEmpty()
                .WithMessage($"The {nameof(KeycloakSettings.Client.ClientId)} field is required.");

            RuleFor(x => x.Secrets.ClientSecret)
                .NotNull()
                .NotEmpty()
                .WithMessage($"The {nameof(KeycloakSettings.Secrets.ClientSecret)} field is required.");

            RuleFor(x => x.ServerSettings.Url)
                .NotNull()
                .NotEmpty()
                .WithMessage($"The {nameof(KeycloakSettings.ServerSettings.Url)} field is required.");

            RuleFor(x => x.Realm.Name)
                .NotNull()
                .NotEmpty()
                .WithMessage($"The {nameof(KeycloakSettings.Realm.Name)} field is required.");

            RuleFor(x => x.Realm.Audience)
                .NotNull()
                .NotEmpty()
                .WithMessage($"The {nameof(KeycloakSettings.Realm.Audience)} field is required.");

            RuleFor(x => x.Realm.Issuer)
                .NotNull()
                .NotEmpty()
                .WithMessage($"The {nameof(KeycloakSettings.Realm.Issuer)} field is required.");
        }
    }
}
