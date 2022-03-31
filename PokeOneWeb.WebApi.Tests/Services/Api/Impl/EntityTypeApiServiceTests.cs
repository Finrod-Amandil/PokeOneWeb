using AutoFixture;
using FluentAssertions;
using Moq;
using PokeOneWeb.Data;
using PokeOneWeb.Data.ReadModels;
using PokeOneWeb.Data.ReadModels.Enums;
using PokeOneWeb.WebApi.Services.Api.Impl;
using Xunit;

namespace PokeOneWeb.WebApi.Tests.Services.Api.Impl
{
    public class EntityTypeApiServiceTests
    {
        private readonly Fixture _fixture;
        private readonly EntityTypeReadModel _entityTypeReadModel;
        private readonly Mock<ReadModelDbContext> _readModelDbContextMock;
        private readonly EntityTypeApiService _entityTypeApiService;

        public EntityTypeApiServiceTests()
        {
            _fixture = new Fixture();

            _entityTypeReadModel = _fixture.Create<EntityTypeReadModel>();

            _readModelDbContextMock = new Mock<ReadModelDbContext>();

            // _readModelDbContextMock.Setup(m => m.EntityTypeReadModels).Returns(new List<>
            // {
            //    _entityTypeReadModel
            // });

            _entityTypeApiService = new EntityTypeApiService(_readModelDbContextMock.Object);
        }

        [Fact(Skip = "Not yet testable because the service access the db directly.")]
        public void GetEntityTypeForPath_ValidPath_CorrectReturn()
        {
            // When
            var result = _entityTypeApiService.GetEntityTypeForPath(_entityTypeReadModel.ResourceName);

            // Then
            result.EntityType.Should().Be(_entityTypeReadModel.EntityType, "because the entity type should be the same.");
        }

        [Theory(Skip = "Not yet testable because the service access the db directly.")]
        [InlineData(null)]
        [InlineData("")]
        [InlineData(" ")]
        public void GetEntityTypeForPath_EmptyPath_UnkonwEntityType(string path)
        {
            // When
            var result = _entityTypeApiService.GetEntityTypeForPath(path);

            // Then
            result.EntityType.Should().Be(EntityType.Unknown, "because the entity type should be unknown.");
        }

        [Fact(Skip = "Not yet testable because the service access the db directly.")]
        public void GetEntityTypeForPath_PathNotFound_UnkonwEntityType()
        {
            // When
            var result = _entityTypeApiService.GetEntityTypeForPath("path");

            // Then
            result.EntityType.Should().Be(EntityType.Unknown, "because the entity type should be unknown.");
        }
    }
}