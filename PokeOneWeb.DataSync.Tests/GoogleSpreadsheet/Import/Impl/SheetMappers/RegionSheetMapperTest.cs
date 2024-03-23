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
    public class RegionSheetMapperTest
    {
        private readonly Mock<ISpreadsheetImportReporter> _reporterMock;
        private readonly RegionSheetMapper _mapper;

        private readonly RowHash _rowHash = new() { IdHash = "Id Hash", Hash = "Hash", ImportSheetId = 1 };

        private List<string> _columnNames = new()
        {
            "Name",
            "ResourceName",
            "Color",
            "Description",
            "IsReleased",
            "IsMainRegion",
            "IsSideRegion",
            "IsEventRegion",
            "EventName"
        };

        private List<object> _values;

        private List<SheetDataRow> Data => new() { new SheetDataRow(_columnNames, _rowHash, _values) };

        public RegionSheetMapperTest()
        {
            _reporterMock = new Mock<ISpreadsheetImportReporter>();
            _mapper = new RegionSheetMapper(_reporterMock.Object);
        }

        [Fact]
        public void Map_WithMinimalValidValues_ShouldMap()
        {
            // Arrange
            const string name = "Region Name";
            const string resourceName = "Resource Name";
            const string color = "#AABBCC";
            const string description = "";
            const bool isReleased = false;
            const bool isMainRegion = true;
            const bool isSideRegion = false;
            const bool isEventRegion = false;

            _values = new List<object>
            {
                name,
                resourceName,
                color,
                description,
                isReleased,
                isMainRegion,
                isSideRegion,
                isEventRegion
            };

            var expected = new Region
            {
                IdHash = _rowHash.IdHash,
                Hash = _rowHash.Hash,
                ImportSheetId = _rowHash.ImportSheetId,
                Name = name,
                ResourceName = resourceName,
                Color = color,
                Description = description,
                IsReleased = isReleased,
                IsMainRegion = isMainRegion,
                IsSideRegion = isSideRegion,
                IsEventRegion = isEventRegion,
                EventName = string.Empty
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
            const string name = "Region Name";
            const string resourceName = "Resource Name";
            const string color = "#AABBCC";
            const string description = "Description";
            const bool isReleased = false;
            const bool isMainRegion = false;
            const bool isSideRegion = false;
            const bool isEventRegion = true;
            const string eventName = "Event Name";

            _values = new List<object>
            {
                name,
                resourceName,
                color,
                description,
                isReleased,
                isMainRegion,
                isSideRegion,
                isEventRegion,
                eventName
            };

            var expected = new Region
            {
                IdHash = _rowHash.IdHash,
                Hash = _rowHash.Hash,
                ImportSheetId = _rowHash.ImportSheetId,
                Name = name,
                ResourceName = resourceName,
                Color = color,
                Description = description,
                IsReleased = isReleased,
                IsMainRegion = isMainRegion,
                IsSideRegion = isSideRegion,
                IsEventRegion = isEventRegion,
                EventName = eventName
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
            const string name1 = "Region Name 1";
            const string resourceName1 = "Resource Name 1";
            const string color1 = "#AABBCC";
            const string description1 = "Description 1";
            const bool isReleased1 = false;
            const bool isMainRegion1 = true;
            const bool isSideRegion1 = false;
            const bool isEventRegion1 = false;

            const string name2 = "Region Name 2";
            const string resourceName2 = "Resource Name 2";
            const string color2 = "#112233";
            const string description2 = "Description 2";
            const bool isReleased2 = true;
            const bool isMainRegion2 = false;
            const bool isSideRegion2 = true;
            const bool isEventRegion2 = false;

            var values1 = new List<object>
            {
                name1,
                resourceName1,
                color1,
                description1,
                isReleased1,
                isMainRegion1,
                isSideRegion1,
                isEventRegion1
            };

            var values2 = new List<object>
            {
                name2,
                resourceName2,
                color2,
                description2,
                isReleased2,
                isMainRegion2,
                isSideRegion2,
                isEventRegion2
            };

            var data = new List<SheetDataRow>
            {
                new(_columnNames, _rowHash, values1),
                new(_columnNames, _rowHash, values2)
            };

            var expected = new List<Region>
            {
                new()
                {
                    IdHash = _rowHash.IdHash,
                    Hash = _rowHash.Hash,
                    ImportSheetId = _rowHash.ImportSheetId,
                    Name = name1,
                    ResourceName = resourceName1,
                    Color = color1,
                    Description = description1,
                    IsReleased = isReleased1,
                    IsMainRegion = isMainRegion1,
                    IsSideRegion = isSideRegion1,
                    IsEventRegion = isEventRegion1,
                    EventName = string.Empty
                },
                new()
                {
                    IdHash = _rowHash.IdHash,
                    Hash = _rowHash.Hash,
                    ImportSheetId = _rowHash.ImportSheetId,
                    Name = name2,
                    ResourceName = resourceName2,
                    Color = color2,
                    Description = description2,
                    IsReleased = isReleased2,
                    IsMainRegion = isMainRegion2,
                    IsSideRegion = isSideRegion2,
                    IsEventRegion = isEventRegion2,
                    EventName = string.Empty
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
                x => x.ReportError(nameof(Region), _rowHash.IdHash, It.IsAny<Exception>()),
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
        [InlineData("", "b", "c", "", false, false, false, false, "")]
        [InlineData("a", "", "c", "", false, false, false, false, "")]
        [InlineData("a", "b", "", "", false, false, false, false, "")]
        [InlineData("a", "b", "c", 1000, false, false, false, false, "")]
        [InlineData("a", "b", "c", "", "notBool", false, false, false, "")]
        [InlineData("a", "b", "c", "", false, "notBool", false, false, "")]
        [InlineData("a", "b", "c", "", false, false, "notBool", false, "")]
        [InlineData("a", "b", "c", "", false, false, false, "notBool", "")]
        [InlineData("a", "b", "c", "", false, false, false, false, 1000)]
        public void Map_WithUnparsableValue_ShouldReportError(params object[] valuesAsArray)
        {
            // Arrange
            _values = valuesAsArray.ToList();

            // Act
            var actual = _mapper.Map(Data).ToList();

            // Assert
            actual.Count.Should().Be(0);
            _reporterMock.Verify(
                x => x.ReportError(nameof(Region), _rowHash.IdHash, It.IsAny<ParseException>()),
                Times.AtLeastOnce);
        }

        [Fact]
        public void Map_WithDifferentColumnOrder_ShouldParse()
        {
            // Arrange
            _columnNames = new List<string>
            {
                "IsEventRegion",
                "IsMainRegion",
                "ResourceName",
                "Color",
                "IsReleased",
                "Name",
                "IsSideRegion",
                "Description",
                "EventName"
            };

            const string name = "Region Name";
            const string resourceName = "Resource Name";
            const string color = "#AABBCC";
            const string description = "";
            const bool isReleased = false;
            const bool isMainRegion = true;
            const bool isSideRegion = false;
            const bool isEventRegion = false;

            _values = new List<object>
            {
                isEventRegion,
                isMainRegion,
                resourceName,
                color,
                isReleased,
                name,
                isSideRegion,
                description
            };

            var expected = new Region
            {
                IdHash = _rowHash.IdHash,
                Hash = _rowHash.Hash,
                ImportSheetId = _rowHash.ImportSheetId,
                Name = name,
                ResourceName = resourceName,
                Color = color,
                Description = description,
                IsReleased = isReleased,
                IsMainRegion = isMainRegion,
                IsSideRegion = isSideRegion,
                IsEventRegion = isEventRegion,
                EventName = string.Empty
            };

            // Act
            var actual = _mapper.Map(Data).ToList();

            // Assert
            actual.Count.Should().Be(1);
            actual[0].Should().BeEquivalentTo(expected);
        }
    }
}