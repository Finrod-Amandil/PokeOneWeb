using System;
using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using Moq;
using PokeOneWeb.Data;
using PokeOneWeb.Data.Entities;
using PokeOneWeb.DataSync.GoogleSpreadsheet.DataTypes;
using PokeOneWeb.DataSync.GoogleSpreadsheet.Import;
using PokeOneWeb.DataSync.GoogleSpreadsheet.Import.Impl.SheetMappers;
using PokeOneWeb.Shared.Exceptions;
using Xunit;

namespace PokeOneWeb.DataSync.Tests.GoogleSpreadsheet.Import.Impl.SheetMappers
{
    public class SeasonSheetMapperTest
    {
        private readonly Mock<ISpreadsheetImportReporter> _reporterMock;
        private readonly SeasonSheetMapper _mapper;

        private readonly RowHash _rowHash = new() { IdHash = "Id Hash", Hash = "Hash", ImportSheetId = 1 };

        private List<string> _columnNames = new()
        {
            "SortIndex",
            "Name",
            "Abbreviation",
            "Color"
        };

        private List<object> _values;

        private List<SheetDataRow> Data => new() { new SheetDataRow(_columnNames, _rowHash, _values) };

        public SeasonSheetMapperTest()
        {
            _reporterMock = new Mock<ISpreadsheetImportReporter>();
            _mapper = new SeasonSheetMapper(_reporterMock.Object);
        }

        [Fact]
        public void Map_WithMinimalValidValues_ShouldMap()
        {
            // Arrange
            const int sortIndex = 123;
            const string name = "Season Name";
            const string abbreviation = "A";

            _values = new List<object>
            {
                sortIndex,
                name,
                abbreviation
            };

            var expected = new Season
            {
                IdHash = _rowHash.IdHash,
                Hash = _rowHash.Hash,
                ImportSheetId = _rowHash.ImportSheetId,
                SortIndex = sortIndex,
                Name = name,
                Abbreviation = abbreviation,
                Color = string.Empty
            };

            // Act
            var actual = _mapper.Map(Data).ToList();

            // Assert
            actual.Count.Should().Be(1);
            actual[0].Should().BeEquivalentTo(expected);
        }

        [Fact]
        public void Map_WithAllValidValues_ShouldMap()
        {
            // Arrange
            const int sortIndex = 123;
            const string name = "Season Name";
            const string abbreviation = "A";
            const string color = "#112233";

            _values = new List<object>
            {
                sortIndex,
                name,
                abbreviation,
                color
            };

            var expected = new Season
            {
                IdHash = _rowHash.IdHash,
                Hash = _rowHash.Hash,
                ImportSheetId = _rowHash.ImportSheetId,
                SortIndex = sortIndex,
                Name = name,
                Abbreviation = abbreviation,
                Color = color
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
            const int sortIndex1 = 123;
            const string name1 = "Season Name 1";
            const string abbreviation1 = "A";

            const int sortIndex2 = 234;
            const string name2 = "Season Name 2";
            const string abbreviation2 = "B";

            var values1 = new List<object>
            {
                sortIndex1,
                name1,
                abbreviation1
            };

            var values2 = new List<object>
            {
                sortIndex2,
                name2,
                abbreviation2
            };

            var data = new List<SheetDataRow>
            {
                new(_columnNames, _rowHash, values1),
                new(_columnNames, _rowHash, values2)
            };

            var expected = new List<Season>
            {
                new()
                {
                    IdHash = _rowHash.IdHash,
                    Hash = _rowHash.Hash,
                    ImportSheetId = _rowHash.ImportSheetId,
                    SortIndex = sortIndex1,
                    Name = name1,
                    Abbreviation = abbreviation1,
                    Color = string.Empty
                },
                new()
                {
                    IdHash = _rowHash.IdHash,
                    Hash = _rowHash.Hash,
                    ImportSheetId = _rowHash.ImportSheetId,
                    SortIndex = sortIndex2,
                    Name = name2,
                    Abbreviation = abbreviation2,
                    Color = string.Empty
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
                x => x.ReportError(nameof(Season), _rowHash.IdHash, It.IsAny<Exception>()),
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
        [InlineData("notInt", "a", "b", "")]
        [InlineData(1, "", "b", "")]
        [InlineData(1, "a", "", "")]
        [InlineData(1, "a", "b", 1000)]
        public void Map_WithUnparsableValue_ShouldReportError(params object[] valuesAsArray)
        {
            // Arrange
            _values = valuesAsArray.ToList();

            // Act
            var actual = _mapper.Map(Data).ToList();

            // Assert
            actual.Count.Should().Be(0);
            _reporterMock.Verify(
                x => x.ReportError(nameof(Season), _rowHash.IdHash, It.IsAny<ParseException>()),
                Times.AtLeastOnce);
        }

        [Fact]
        public void Map_WithDifferentColumnOrder_ShouldParse()
        {
            // Arrange
            _columnNames = new List<string>
            {
                "Abbreviation",
                "SortIndex",
                "Name",
                "Color"
            };

            const int sortIndex = 123;
            const string name = "Season Name";
            const string abbreviation = "A";
            const string color = "#112233";

            _values = new List<object>
            {
                abbreviation,
                sortIndex,
                name,
                color
            };

            var expected = new Season
            {
                IdHash = _rowHash.IdHash,
                Hash = _rowHash.Hash,
                ImportSheetId = _rowHash.ImportSheetId,
                SortIndex = sortIndex,
                Name = name,
                Abbreviation = abbreviation,
                Color = color
            };

            // Act
            var actual = _mapper.Map(Data).ToList();

            // Assert
            actual.Count.Should().Be(1);
            actual[0].Should().BeEquivalentTo(expected);
        }
    }
}