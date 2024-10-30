using AutoFixture;
using Feijuca.Auth.Application.Validators;
using Feijuca.Auth.Common.Models;
using Feijuca.Auth.Models;
using FluentAssertions;

namespace Feijuca.Auth.Api.UnitTests.Validators
{
    public class KeycloakSettingsValidatorTests
    {
        private readonly Fixture _fixture = new();
        private readonly KeycloakSettingsValidator _keycloakSettingsValidator = new();

        [Fact]
        public void Given_InvalidKeycloakSettings_WhenValidatingSettings_Then_Returnsfailures()
        {
            // Arrange
            var invalidKeycloakSettings = _fixture.Build<KeycloakSettings>()
                               .With(x => x.Client, (Client) null!)
                               .With(x => x.Secrets, (Secrets) null!)
                               .With(x => x.ServerSettings, (ServerSettings) null!)
                               .With(x => x.Realm, (Realm) null!)
                               .Create();

            // Act
            var result = _keycloakSettingsValidator.Validate(invalidKeycloakSettings);

            // Assert
            result
               .IsValid
               .Should()
               .BeFalse();

            result.Errors.Should().Contain(e => e.ErrorMessage == $"The {nameof(KeycloakSettings.Client)} field is required.");
            result.Errors.Should().Contain(e => e.ErrorMessage == $"The {nameof(KeycloakSettings.Secrets)} field is required.");
            result.Errors.Should().Contain(e => e.ErrorMessage == $"The {nameof(KeycloakSettings.ServerSettings)} field is required.");
            result.Errors.Should().Contain(e => e.ErrorMessage == $"The {nameof(KeycloakSettings.Realm)} field is required.");
        }

        [Fact]
        public void Given_ValidKeycloakSettings_WhenValidatingSettings_Then_ReturnsSuccess()
        {
            // Arrange
            var validKeycloakSettings = _fixture.Create<KeycloakSettings>();

            // Act
            var result = _keycloakSettingsValidator.Validate(validKeycloakSettings);

            // Assert
            result
                .IsValid
                .Should()
                .BeTrue();
        }
    }
}
