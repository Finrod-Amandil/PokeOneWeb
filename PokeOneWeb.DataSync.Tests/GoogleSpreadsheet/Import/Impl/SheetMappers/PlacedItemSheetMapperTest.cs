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
    public class PlacedItemSheetMapperTest
    {
        private readonly Mock<ISpreadsheetImportReporter> _reporterMock;
        private readonly PlacedItemSheetMapper _mapper;

        private readonly RowHash _rowHash = new() { IdHash = "Id Hash", Hash = "Hash", ImportSheetId = 1 };

        private List<string> _columnNames = new()
        {
            "Location",
            "Quantity",
            "Item",
            "SortIndex",
            "Index",
            "PlacementDescription",
            "IsHidden",
            "IsConfirmed",
            "Requirements",
            "ScreenshotName",
            "Notes"
        };

        private List<object> _values;

        private List<SheetDataRow> Data => new() { new SheetDataRow(_columnNames, _rowHash, _values) };

        public PlacedItemSheetMapperTest()
        {
            _reporterMock = new Mock<ISpreadsheetImportReporter>();
            _mapper = new PlacedItemSheetMapper(_reporterMock.Object);
        }

        [Fact]
        public void Map_WithMinimalValidValues_ShouldMap()
        {
            // Arrange
            const string location = "Location Name";
            const int quantity = 5;
            const string item = "Item Name";
            const int sortIndex = 7;
            const int index = 2;

            _values = new List<object>
            {
                location,
                quantity,
                item,
                sortIndex,
                index
            };

            var expected = new PlacedItem
            {
                IdHash = _rowHash.IdHash,
                Hash = _rowHash.Hash,
                ImportSheetId = _rowHash.ImportSheetId,
                LocationName = location,
                Quantity = quantity,
                ItemName = item,
                SortIndex = sortIndex,
                Index = index,
                PlacementDescription = string.Empty,
                IsHidden = false,
                IsConfirmed = true,
                Requirements = string.Empty,
                ScreenshotName = string.Empty,
                Notes = string.Empty
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
            const string location = "Location Name";
            const int quantity = 5;
            const string item = "Item Name";
            const int sortIndex = 7;
            const int index = 2;
            const string placementDescription = "Placement Description";
            const bool isHidden = true;
            const bool isConfirmed = false;
            const string requirements = "Requirements";
            const string screenshotName = "screenshot.png";
            const string notes = "Notes";

            _values = new List<object>
            {
                location,
                quantity,
                item,
                sortIndex,
                index,
                placementDescription,
                isHidden,
                isConfirmed,
                requirements,
                screenshotName,
                notes
            };

            var expected = new PlacedItem
            {
                IdHash = _rowHash.IdHash,
                Hash = _rowHash.Hash,
                ImportSheetId = _rowHash.ImportSheetId,
                LocationName = location,
                Quantity = quantity,
                ItemName = item,
                SortIndex = sortIndex,
                Index = index,
                PlacementDescription = placementDescription,
                IsHidden = isHidden,
                IsConfirmed = isConfirmed,
                Requirements = requirements,
                ScreenshotName = screenshotName,
                Notes = notes
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
            const string location1 = "Location Name 1";
            const int quantity1 = 5;
            const string item1 = "Item Name 1";
            const int sortIndex1 = 7;
            const int index1 = 2;

            const string location2 = "Location Name 2";
            const int quantity2 = 1;
            const string item2 = "Item Name 2";
            const int sortIndex2 = 4;
            const int index2 = 1;

            var values1 = new List<object>
            {
                location1,
                quantity1,
                item1,
                sortIndex1,
                index1
            };

            var values2 = new List<object>
            {
                location2,
                quantity2,
                item2,
                sortIndex2,
                index2
            };

            var data = new List<SheetDataRow>
            {
                new(_columnNames, _rowHash, values1),
                new(_columnNames, _rowHash, values2)
            };

            var expected = new List<PlacedItem>
            {
                new()
                {
                    IdHash = _rowHash.IdHash,
                    Hash = _rowHash.Hash,
                    ImportSheetId = _rowHash.ImportSheetId,
                    LocationName = location1,
                    Quantity = quantity1,
                    ItemName = item1,
                    SortIndex = sortIndex1,
                    Index = index1,
                    PlacementDescription = string.Empty,
                    IsHidden = false,
                    IsConfirmed = true,
                    Requirements = string.Empty,
                    ScreenshotName = string.Empty,
                    Notes = string.Empty
                },
                new()
                {
                    IdHash = _rowHash.IdHash,
                    Hash = _rowHash.Hash,
                    ImportSheetId = _rowHash.ImportSheetId,
                    LocationName = location2,
                    Quantity = quantity2,
                    ItemName = item2,
                    SortIndex = sortIndex2,
                    Index = index2,
                    PlacementDescription = string.Empty,
                    IsHidden = false,
                    IsConfirmed = true,
                    Requirements = string.Empty,
                    ScreenshotName = string.Empty,
                    Notes = string.Empty
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
                x => x.ReportError(nameof(PlacedItem), _rowHash.IdHash, It.IsAny<Exception>()),
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
        [InlineData("", 1, "b", 1, 1, "", false, false, "", "", "")]
        [InlineData("a", "notInt", "b", 1, 1, "", false, false, "", "", "")]
        [InlineData("a", 1, "", 1, 1, "", false, false, "", "", "")]
        [InlineData("a", 1, "b", "notInt", 1, "", false, false, "", "", "")]
        [InlineData("a", 1, "b", 1, "notInt", "", false, false, "", "", "")]
        [InlineData("a", 1, "b", 1, 1, 1000, false, false, "", "", "")]
        [InlineData("a", 1, "b", 1, 1, "", "notBool", false, "", "", "")]
        [InlineData("a", 1, "b", 1, 1, "", false, "notBool", "", "", "")]
        [InlineData("a", 1, "b", 1, 1, "", false, false, 1000, "", "")]
        [InlineData("a", 1, "b", 1, 1, "", false, false, "", 1000, "")]
        [InlineData("a", 1, "b", 1, 1, "", false, false, "", "", 1000)]
        public void Map_WithUnparsableValue_ShouldReportError(params object[] valuesAsArray)
        {
            // Arrange
            _values = valuesAsArray.ToList();

            // Act
            var actual = _mapper.Map(Data).ToList();

            // Assert
            actual.Count.Should().Be(0);
            _reporterMock.Verify(
                x => x.ReportError(nameof(PlacedItem), _rowHash.IdHash, It.IsAny<ParseException>()),
                Times.AtLeastOnce);
        }

        [Fact]
        public void Map_WithDifferentColumnOrder_ShouldParse()
        {
            // Arrange
            _columnNames = new List<string>
            {
                "PlacementDescription",
                "ScreenshotName",
                "Notes",
                "Requirements",
                "IsConfirmed",
                "SortIndex",
                "Location",
                "IsHidden",
                "Quantity",
                "Item",
                "Index",
            };

            const string location = "Location Name";
            const int quantity = 5;
            const string item = "Item Name";
            const int sortIndex = 7;
            const int index = 2;
            const string placementDescription = "Placement Description";
            const bool isHidden = true;
            const bool isConfirmed = false;
            const string requirements = "Requirements";
            const string screenshotName = "screenshot.png";
            const string notes = "Notes";

            _values = new List<object>
            {
                placementDescription,
                screenshotName,
                notes,
                requirements,
                isConfirmed,
                sortIndex,
                location,
                isHidden,
                quantity,
                item,
                index
            };

            var expected = new PlacedItem
            {
                IdHash = _rowHash.IdHash,
                Hash = _rowHash.Hash,
                ImportSheetId = _rowHash.ImportSheetId,
                LocationName = location,
                Quantity = quantity,
                ItemName = item,
                SortIndex = sortIndex,
                Index = index,
                PlacementDescription = placementDescription,
                IsHidden = isHidden,
                IsConfirmed = isConfirmed,
                Requirements = requirements,
                ScreenshotName = screenshotName,
                Notes = notes
            };

            // Act
            var actual = _mapper.Map(Data).ToList();

            // Assert
            actual.Count.Should().Be(1);
            actual[0].Should().BeEquivalentTo(expected);
        }
    }
}