using System;
using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using Moq;
using PokeOneWeb.Data;
using PokeOneWeb.Data.Entities;
using PokeOneWeb.DataSync.GoogleSpreadsheet.DataTypes;
using PokeOneWeb.DataSync.Import.Interfaces;
using PokeOneWeb.DataSync.Import.SheetMappers;
using PokeOneWeb.Shared.Exceptions;
using Xunit;

namespace PokeOneWeb.DataSync.Tests.GoogleSpreadsheet.Import.Impl.SheetMappers
{
    public class LearnableMoveLearnMethodAvailabilitySheetMapperTest
    {
        private readonly Mock<ISpreadsheetImportReporter> _reporterMock;
        private readonly LearnableMoveLearnMethodAvailabilitySheetMapper _mapper;

        private readonly RowHash _rowHash = new() { IdHash = "Id Hash", Hash = "Hash", ImportSheetId = 1 };
        private List<string> _columnNames = new() { "Name", "IsAvailable", "Description" };
        private List<object> _values;

        private List<SheetDataRow> Data => new() { new SheetDataRow(_columnNames, _rowHash, _values) };

        public LearnableMoveLearnMethodAvailabilitySheetMapperTest()
        {
            _reporterMock = new Mock<ISpreadsheetImportReporter>();
            _mapper = new LearnableMoveLearnMethodAvailabilitySheetMapper(_reporterMock.Object);
        }

        [Fact]
        public void Map_WithMinimalValidValues_ShouldMap()
        {
            // Arrange
            const string name = "Availability Name";
            const bool isAvailable = true;
            const string description = "Availability Description";

            _values = new List<object>
            {
                name,
                isAvailable,
                description
            };

            var expected = new LearnableMoveLearnMethodAvailability
            {
                IdHash = _rowHash.IdHash,
                Hash = _rowHash.Hash,
                ImportSheetId = _rowHash.ImportSheetId,
                Name = name,
                IsAvailable = isAvailable,
                Description = description
            };

            // Act
            var actual = _mapper.Map(Data).ToList();

            // Assert
            actual.Count.Should().Be(1);
            actual[0].Should().BeEquivalentTo(expected);
        }

        [Fact]
        public void Map_WithMultipleRows_ShouldMap()
        {
            // Arrange
            const string name1 = "Availability Name 1";
            const bool isAvailable1 = true;
            const string description1 = "Availability Description 1";

            const string name2 = "Availability Name 2";
            const bool isAvailable2 = false;
            const string description2 = "Availability Description 2";

            List<object> values1 = new()
            {
                name1,
                isAvailable1,
                description1
            };

            List<object> values2 = new()
            {
                name2,
                isAvailable2,
                description2
            };

            var data = new List<SheetDataRow>
            {
                new(_columnNames, _rowHash, values1),
                new(_columnNames, _rowHash, values2)
            };

            var expected1 = new LearnableMoveLearnMethodAvailability
            {
                IdHash = _rowHash.IdHash,
                Hash = _rowHash.Hash,
                ImportSheetId = _rowHash.ImportSheetId,
                Name = name1,
                IsAvailable = isAvailable1,
                Description = description1
            };

            var expected2 = new LearnableMoveLearnMethodAvailability
            {
                IdHash = _rowHash.IdHash,
                Hash = _rowHash.Hash,
                ImportSheetId = _rowHash.ImportSheetId,
                Name = name2,
                IsAvailable = isAvailable2,
                Description = description2
            };

            // Act
            var actual = _mapper.Map(data).ToList();

            // Assert
            actual.Count.Should().Be(2);
            actual.Single(x => x.Name.Equals(name1)).Should().BeEquivalentTo(expected1);
            actual.Single(x => x.Name.Equals(name2)).Should().BeEquivalentTo(expected2);
        }

        [Fact]
        public void Map_WithMissingRequiredValues_ShouldNotParseAndReportErrors()
        {
            // Arrange
            _values = new List<object>();

            // Act
            var actual = _mapper.Map(Data).ToList();

            // Assert
            actual.Count.Should().Be(0);
            _reporterMock.Verify(
                x => x.ReportError(
                    nameof(LearnableMoveLearnMethodAvailability),
                    _rowHash.IdHash,
                    It.IsAny<Exception>(),
                    It.IsAny<string>()),
                Times.AtLeastOnce);
        }

        [Fact]
        public void Map_WithNull_ShouldThrow()
        {
            // Arrange
            List<SheetDataRow> data = null;

            // Act
            var actual = () => _mapper.Map(data).ToList();

            // Assert
            actual.Should().Throw<ArgumentNullException>();
        }

        [Fact]
        public void Map_WithEmptyList_ShouldReturnEmptyList()
        {
            // Arrange
            var data = new List<SheetDataRow>();

            // Act
            var actual = _mapper.Map(data).ToList();

            // Assert
            actual.Count.Should().Be(0);
        }

        [Theory]
        [InlineData("", true, "b")]
        [InlineData("a", "notBool", "b")]
        [InlineData("a", true, "")]
        public void Map_WithUnparsableValue_ShouldReportError(params object[] valuesAsArray)
        {
            // Arrange
            _values = valuesAsArray.ToList();

            // Act
            var actual = _mapper.Map(Data).ToList();

            // Assert
            actual.Count.Should().Be(0);
            _reporterMock.Verify(
                x => x.ReportError(
                    nameof(LearnableMoveLearnMethodAvailability),
                    _rowHash.IdHash,
                    It.IsAny<ParseException>(),
                    It.IsAny<string>()),
                Times.AtLeastOnce);
        }

        [Fact]
        public void Map_WithDifferentColumnOrder_ShouldParse()
        {
            // Arrange
            _columnNames = new List<string>
            {
                "IsAvailable",
                "Description",
                "Name"
            };

            const string name = "Availability Name";
            const bool isAvailable = true;
            const string description = "Availability Description";

            _values = new List<object>
            {
                isAvailable,
                description,
                name
            };

            var expected = new LearnableMoveLearnMethodAvailability()
            {
                IdHash = _rowHash.IdHash,
                Hash = _rowHash.Hash,
                ImportSheetId = _rowHash.ImportSheetId,
                Name = name,
                IsAvailable = isAvailable,
                Description = description
            };

            // Act
            var actual = _mapper.Map(Data).ToList();

            // Assert
            actual.Count.Should().Be(1);
            actual[0].Should().BeEquivalentTo(expected);
        }
    }
}
