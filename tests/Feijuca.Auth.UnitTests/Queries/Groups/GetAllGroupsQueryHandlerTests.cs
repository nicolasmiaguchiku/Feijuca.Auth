﻿using AutoFixture;
using Feijuca.Auth.Application.Queries.Groups;
using Feijuca.Auth.Common.Errors;
using Mattioli.Configurations.Models;
using Feijuca.Auth.Domain.Entities;
using Feijuca.Auth.Domain.Interfaces;
using FluentAssertions;
using Moq;

namespace Feijuca.Auth.Api.UnitTests.Queries.Groups
{
    public class GetAllGroupsQueryHandlerTests
    {
        private readonly IFixture _fixture = new Fixture();
        private readonly Mock<IGroupRepository> _groupRepositoryMock = new();
        private readonly GetAllGroupsQueryHandler _handler;

        public GetAllGroupsQueryHandlerTests()
        {
            _handler = new GetAllGroupsQueryHandler(_groupRepositoryMock.Object);
        }

        [Fact]
        public async Task Given_ValidRequest_WhenGroupsExist_Then_ReturnSuccessResult()
        {
            // Arrange
            var request = _fixture.Create<GetAllGroupsQuery>();
            var cancellationToken = _fixture.Create<CancellationToken>();
            var groups = _fixture.CreateMany<Group>();
            var groupsResult =  Result<IEnumerable<Group>>.Success(groups);

            _groupRepositoryMock
                .Setup(repo => repo.GetAllAsync(It.IsAny<CancellationToken>()))
                .ReturnsAsync(groupsResult);

            // Act
            var result = await _handler.Handle(request, cancellationToken);

            // Assert
            result
                .IsSuccess
                .Should()
                .BeTrue();

            _groupRepositoryMock.Verify(repo => repo.GetAllAsync(It.IsAny<CancellationToken>()), Times.Once());
            _groupRepositoryMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task Given_InvalidRequest_WhenGroupsExistNoExist_Then_ReturnFailure()
        {
            // Arrange
            var request = _fixture.Create<GetAllGroupsQuery>();
            var cancellationToken = _fixture.Create<CancellationToken>();
            var groupsResult = Result<IEnumerable<Group>>.Failure(GroupErrors.CreationGroupError);

            _groupRepositoryMock
                .Setup(repo => repo.GetAllAsync(It.IsAny<CancellationToken>()))
                .ReturnsAsync(groupsResult);

            // Act
            var result = await _handler.Handle(request, cancellationToken);

            // Assert
            result
                .Error
                .Should()
                .Be(GroupErrors.CreationGroupError);

            _groupRepositoryMock.Verify(repo => repo.GetAllAsync(It.IsAny<CancellationToken>()), Times.Once());
            _groupRepositoryMock.VerifyNoOtherCalls();
        }
    }
}
