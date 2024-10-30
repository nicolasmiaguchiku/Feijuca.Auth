using AutoFixture;
using Feijuca.Auth.Application.Commands.Users;
using Feijuca.Auth.Common.Errors;
using Feijuca.Auth.Common.Models;
using Feijuca.Auth.Domain.Entities;
using Feijuca.Auth.Domain.Interfaces;
using FluentAssertions;
using Moq;

namespace Feijuca.Auth.Api.UnitTests.Command.Auth
{
    public class LoginCommandHandlerTests
    {
        private readonly IFixture _fixture = new Fixture();
        private readonly Mock<ILoginService> _loginServiceMock = new();
        private readonly LoginCommandHandler _handler;

        public LoginCommandHandlerTests()
        {
            _handler = new LoginCommandHandler(_loginServiceMock.Object);
        }

        [Fact]
        public async Task Given_ValidLogin_When_LoginAsync_Then_ShouldReturnSuccess()
        {
            // Arrange
            var loginCommand = _fixture.Create<LoginCommand>();
            var cancellationToken = _fixture.Create<CancellationToken>();
            var tokenDetails = _fixture.Create<TokenDetails>();
            var loginResult = Result<TokenDetails>.Success(tokenDetails);

            _loginServiceMock
                .Setup(repo => repo.LoginAsync(It.IsAny<bool>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(loginResult);

            // Act
            var result = await _handler.Handle(loginCommand, cancellationToken);

            // Assert
            result
                .IsSuccess
                .Should()
                .BeTrue();

            _loginServiceMock.Verify(repo => repo.LoginAsync(It.IsAny<bool>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<CancellationToken>()), Times.Once());
            _loginServiceMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task Given_InvalidLogin_When_LoginAsync_Then_ShouldReturnFailure()
        {
            // Arrange
            var loginCommand = _fixture.Create<LoginCommand>();
            var cancellationToken = _fixture.Create<CancellationToken>();
            var loginResult = Result<TokenDetails>.Failure(UserErrors.InvalidUserNameOrPasswordError);

            _loginServiceMock
                .Setup(repo => repo.LoginAsync(It.IsAny<bool>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(loginResult);

            // Act
            var result = await _handler.Handle(loginCommand, cancellationToken);

            // Assert
            result
                .Error
                .Should()
                .Be(UserErrors.InvalidUserNameOrPasswordError);

            _loginServiceMock.Verify(repo => repo.LoginAsync(It.IsAny<bool>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<CancellationToken>()), Times.Once());
            _loginServiceMock.VerifyNoOtherCalls();
        }
    }
}
