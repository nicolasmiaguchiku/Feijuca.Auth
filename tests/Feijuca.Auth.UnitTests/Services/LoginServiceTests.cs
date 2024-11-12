using AutoFixture;
using Feijuca.Auth.Common.Errors;
using Feijuca.Auth.Common.Models;
using Feijuca.Auth.Domain.Entities;
using Feijuca.Auth.Domain.Interfaces;
using Feijuca.Auth.Domain.Services;
using FluentAssertions;
using Moq;

namespace Feijuca.Auth.Api.UnitTests.Services
{
    public class LoginServiceTests
    {
        private readonly IFixture _fixture = new Fixture();
        private readonly Mock<IUserRepository> _userRepositoryMock = new();
        private readonly LoginService _loginServiceMock;

        public LoginServiceTests()
        {
            _loginServiceMock = new LoginService(_userRepositoryMock.Object);
        }

        [Fact]
        public async Task Given_InvalidUser_WhenLoginCalled_Then_ReturnsFailure()
        {
            // Arrange
            var userName = _fixture.Create<string>();
            var password = _fixture.Create<string>();
            var revokeActiveSessions = _fixture.Create<bool>();
            var cancellationToken = _fixture.Create<CancellationToken>();
            var nameResult = Result<User>.Failure(UserErrors.InvalidUserNameOrPasswordError);

            _userRepositoryMock
                .Setup(repo => repo.GetAsync(It.IsAny<string>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(nameResult);

            // Act
            var result = await _loginServiceMock.LoginAsync(revokeActiveSessions, userName, password, cancellationToken);

            // Assert
            result
                .Error
                .Should()
                .Be(UserErrors.InvalidUserNameOrPasswordError);

            _userRepositoryMock.Verify(repo => repo.GetAsync(It.IsAny<string>(), It.IsAny<CancellationToken>()), Times.Once());
            _userRepositoryMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task Given_ValidUserAndPasswordAndRevokeActiveSessionsTrue_WhenLoginAsyncCalled_Then_ReturnsSuccessWithTokenDetails()
        {
            // Arrange
            var username = _fixture.Create<string>();
            var password = _fixture.Create<string>();
            var revokeActiveSessions = _fixture.Create<bool>();
            var cancellationToken = _fixture.Create<CancellationToken>();
            var user = new User { Id = Guid.NewGuid(), Username = username };
            var tokenDetails = _fixture.Create<TokenDetails>();

            _userRepositoryMock
                .Setup(repo => repo.GetAsync(It.IsAny<string>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(Result<User>.Success(user));

            _userRepositoryMock
                .Setup(repo => repo.RevokeSessionsAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(Result<bool>.Success(true));

            _userRepositoryMock
                .Setup(repo => repo.LoginAsync(username, password, cancellationToken))
                .ReturnsAsync(Result<TokenDetails>.Success(tokenDetails));

            // Act
            var result = await _loginServiceMock.LoginAsync(revokeActiveSessions, username, password, cancellationToken);

            // Assert
            result
                .IsSuccess
                .Should()
                .BeTrue();

            result
                .Response
                .Should()
                .BeEquivalentTo(tokenDetails);

            _userRepositoryMock.Verify(repo => repo.GetAsync(It.IsAny<string>(), It.IsAny<CancellationToken>()), Times.Once());
            _userRepositoryMock.Verify(repo => repo.RevokeSessionsAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()), Times.Once);
            _userRepositoryMock.Verify(repo => repo.LoginAsync(username, password, cancellationToken), Times.Once);
            _userRepositoryMock.VerifyNoOtherCalls();
        }
    }
}
