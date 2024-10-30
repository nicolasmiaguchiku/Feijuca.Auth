using AutoFixture;
using Feijuca.Auth.Application.Requests.Auth;
using Feijuca.Auth.Application.Validators;
using FluentAssertions;

namespace Feijuca.Auth.Api.UnitTests.Validators
{
    public class LoginUserRequestValidatorTests
    {
        private readonly IFixture _fixture = new Fixture();
        private LoginUserRequestValidator _loginUserRequestValidator = new();

        [Fact]
        public void Given_NullAndEmptyUsername_WhenValidatingRequest_Then_ReturnsError()
        {
            // Arrange
            var userNameInvalid = _fixture.Build<LoginUserRequest>()
                .With(r => r.Username, (string) null!)
                .With(r => r.Username, string.Empty)
                .Create();

            // Act
            var result = _loginUserRequestValidator.Validate(userNameInvalid);

            // Assert
            result
                .IsValid
                .Should()
                .BeFalse();

            result.Errors.Should().Contain(e => e.ErrorMessage == $"The {nameof(LoginUserRequest.Username)} is mandatory.");
        }

        [Fact]
        public void Given_NullPassword_WhenValidatingRequest_Then_ReturnsError()
        {
            // Arrange
            var passwordInvalid = _fixture.Build<LoginUserRequest>()
                .With(r => r.Password, (string)null!)
                .With(r => r.Password, string.Empty)
                .Create();

            // Act
            var result = _loginUserRequestValidator.Validate(passwordInvalid);

            // Assert
            result
                .IsValid
                .Should()
                .BeFalse();

            result.Errors.Should().Contain(e => e.ErrorMessage == $"The {nameof(LoginUserRequest.Password)} is mandatory.");
        }

        [Fact]
        public void Given_ValidUsernameAndPassword_WhenValidatingRequest_Then_ReturnsSuccess()
        {
            // Arrange
            var loginUserRequest = _fixture.Create<LoginUserRequest>();

            //Act
            var result = _loginUserRequestValidator.Validate(loginUserRequest);

            // Assert
            result
                .IsValid
                .Should()
                .BeTrue();
        }
    }
}
