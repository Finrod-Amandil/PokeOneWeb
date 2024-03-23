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
    public class SeasonTimeOfDaySheetMapperTest
    {
        private readonly Mock<ISpreadsheetImportReporter> _reporterMock;
        private readonly SeasonTimeOfDaySheetMapper _mapper;

        private readonly RowHash _rowHash = new() { IdHash = "Id Hash", Hash = "Hash" };
        private List<string> _columnNames = new() { "Season", "Time", "StartHour", "EndHour" };
        private List<object> _values;

        private List<SheetDataRow> Data => new() { new SheetDataRow(_columnNames, _rowHash, _values) };

        public SeasonTimeOfDaySheetMapperTest()
        {
            _reporterMock = new Mock<ISpreadsheetImportReporter>();
            _mapper = new SeasonTimeOfDaySheetMapper(_reporterMock.Object);
        }

        [Fact]
        public void Map_WithMinimalValidValues_ShouldMap()
        {
            // Arrange
            const string season = "Season Name";
            const string time = "Time Name";
            const int startHour = 1;
            const int endHour = 2;

            _values = new List<object>
            {
                season,
                time,
                startHour,
                endHour
            };

            var expected = new SeasonTimeOfDay
            {
                IdHash = _rowHash.IdHash,
                Hash = _rowHash.Hash,
                SeasonName = season,
                TimeOfDayName = time,
                StartHour = startHour,
                EndHour = endHour
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
            const string season1 = "Season Name 1";
            const string time1 = "Time Name 1";
            const int startHour1 = 1;
            const int endHour1 = 2;

            const string season2 = "Season Name 2";
            const string time2 = "Time Name 2";
            const int startHour2 = 5;
            const int endHour2 = 6;

            List<object> values1 = new()
            {
                season1,
                time1,
                startHour1,
                endHour1
            };

            List<object> values2 = new()
            {
                season2,
                time2,
                startHour2,
                endHour2
            };

            var data = new List<SheetDataRow>
            {
                new(_columnNames, _rowHash, values1),
                new(_columnNames, _rowHash, values2)
            };

            var expected = new List<SeasonTimeOfDay>
            {
                new()
                {
                    IdHash = _rowHash.IdHash,
                    Hash = _rowHash.Hash,
                    SeasonName = season1,
                    TimeOfDayName = time1,
                    StartHour = startHour1,
                    EndHour = endHour1
                },
                new()
                {
                    IdHash = _rowHash.IdHash,
                    Hash = _rowHash.Hash,
                    SeasonName = season2,
                    TimeOfDayName = time2,
                    StartHour = startHour2,
                    EndHour = endHour2
                }
            };

            // Act
            var actual = _mapper.Map(data).ToList();

            // Assert
            actual.Count.Should().Be(2);
            actual.Should().BeEquivalentTo(expected);
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
                    nameof(SeasonTimeOfDay),
                    _rowHash.IdHash,
                    It.IsAny<Exception>()),
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
        [InlineData("", "b", 1, 1)]
        [InlineData("a", "", 1, 1)]
        [InlineData("a", "b", "notInt", 1)]
        [InlineData("a", "b", 1, "notInt")]
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
                    nameof(SeasonTimeOfDay),
                    _rowHash.IdHash,
                    It.IsAny<ParseException>()),
                Times.AtLeastOnce);
        }

        [Fact]
        public void Map_WithDifferentColumnOrder_ShouldParse()
        {
            // Arrange
            _columnNames = new List<string>
            {
                "StartHour",
                "Season",
                "EndHour",
                "Time"
            };

            const string season = "Season Name";
            const string time = "Time Name";
            const int startHour = 1;
            const int endHour = 2;

            _values = new List<object>
            {
                startHour,
                season,
                endHour,
                time
            };

            var expected = new SeasonTimeOfDay
            {
                IdHash = _rowHash.IdHash,
                Hash = _rowHash.Hash,
                SeasonName = season,
                TimeOfDayName = time,
                StartHour = startHour,
                EndHour = endHour
            };

            // Act
            var actual = _mapper.Map(Data).ToList();

            // Assert
            actual.Count.Should().Be(1);
            actual[0].Should().BeEquivalentTo(expected);
        }
    }
}