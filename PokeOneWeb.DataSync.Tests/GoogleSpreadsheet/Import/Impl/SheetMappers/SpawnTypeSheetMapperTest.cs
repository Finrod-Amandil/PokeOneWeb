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
    public class SpawnTypeSheetMapperTest
    {
        private readonly Mock<ISpreadsheetImportReporter> _reporterMock;
        private readonly SpawnTypeSheetMapper _mapper;

        private readonly RowHash _rowHash = new() { IdHash = "Id Hash", Hash = "Hash", ImportSheetId = 1 };

        private List<string> _columnNames = new()
        {
            "Name",
            "SortIndex",
            "IsSyncable",
            "IsInfinite",
            "Color"
        };

        private List<object> _values;

        private List<SheetDataRow> Data => new() { new SheetDataRow(_columnNames, _rowHash, _values) };

        public SpawnTypeSheetMapperTest()
        {
            _reporterMock = new Mock<ISpreadsheetImportReporter>();
            _mapper = new SpawnTypeSheetMapper(_reporterMock.Object);
        }

        [Fact]
        public void Map_WithMinimalValidValues_ShouldMap()
        {
            // Arrange
            const string name = "Spawn Type Name";
            const int sortIndex = 123;
            const bool isSyncable = true;
            const bool isInfinite = true;
            const string color = "#AABBCC";

            _values = new List<object>
            {
                name,
                sortIndex,
                isSyncable,
                isInfinite,
                color
            };

            var expected = new SpawnType
            {
                IdHash = _rowHash.IdHash,
                Hash = _rowHash.Hash,
                ImportSheetId = _rowHash.ImportSheetId,
                Name = name,
                SortIndex = sortIndex,
                IsSyncable = isSyncable,
                IsInfinite = isInfinite,
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
            const string name1 = "Spawn Type Name 1";
            const int sortIndex1 = 123;
            const bool isSyncable1 = true;
            const bool isInfinite1 = true;
            const string color1 = "#AABBCC";

            const string name2 = "Spawn Type Name 2";
            const int sortIndex2 = 234;
            const bool isSyncable2 = false;
            const bool isInfinite2 = false;
            const string color2 = "#112233";

            List<object> values1 = new()
            {
                name1,
                sortIndex1,
                isSyncable1,
                isInfinite1,
                color1
            };

            List<object> values2 = new()
            {
                name2,
                sortIndex2,
                isSyncable2,
                isInfinite2,
                color2
            };

            var data = new List<SheetDataRow>
            {
                new(_columnNames, _rowHash, values1),
                new(_columnNames, _rowHash, values2)
            };

            var expected = new List<SpawnType>
            {
                new()
                {
                    IdHash = _rowHash.IdHash,
                    Hash = _rowHash.Hash,
                    ImportSheetId = _rowHash.ImportSheetId,
                    Name = name1,
                    SortIndex = sortIndex1,
                    IsSyncable = isSyncable1,
                    IsInfinite = isInfinite1,
                    Color = color1
                },
                new()
                {
                    IdHash = _rowHash.IdHash,
                    Hash = _rowHash.Hash,
                    ImportSheetId = _rowHash.ImportSheetId,
                    Name = name2,
                    SortIndex = sortIndex2,
                    IsSyncable = isSyncable2,
                    IsInfinite = isInfinite2,
                    Color = color2
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
                    nameof(SpawnType),
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
        [InlineData("", 1, false, false, "b")]
        [InlineData("a", "notInt", false, false, "b")]
        [InlineData("a", 1, "notBool", false, "b")]
        [InlineData("a", 1, false, "notBool", "b")]
        [InlineData("a", 1, false, false, "")]
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
                    nameof(SpawnType),
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
                "IsSyncable",
                "Color",
                "SortIndex",
                "IsInfinite",
                "Name"
            };

            const string name = "Spawn Type Name";
            const int sortIndex = 123;
            const bool isSyncable = true;
            const bool isInfinite = true;
            const string color = "#AABBCC";

            _values = new List<object>
            {
                isSyncable,
                color,
                sortIndex,
                isInfinite,
                name
            };

            var expected = new SpawnType
            {
                IdHash = _rowHash.IdHash,
                Hash = _rowHash.Hash,
                ImportSheetId = _rowHash.ImportSheetId,
                Name = name,
                SortIndex = sortIndex,
                IsSyncable = isSyncable,
                IsInfinite = isInfinite,
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