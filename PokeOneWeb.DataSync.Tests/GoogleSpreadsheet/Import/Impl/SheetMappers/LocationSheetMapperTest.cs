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
    public class LocationSheetMapperTest
    {
        private readonly Mock<ISpreadsheetImportReporter> _reporterMock;
        private readonly LocationSheetMapper _mapper;

        private readonly RowHash _rowHash = new() { IdHash = "Id Hash", Hash = "Hash", ImportSheetId = 1 };

        private List<string> _columnNames = new()
        {
            "Region",
            "LocationGroup",
            "ResourceName",
            "Name",
            "SortIndex",
            "IsDiscoverable",
            "Notes"
        };

        private List<object> _values;

        private List<SheetDataRow> Data => new() { new SheetDataRow(_columnNames, _rowHash, _values) };

        public LocationSheetMapperTest()
        {
            _reporterMock = new Mock<ISpreadsheetImportReporter>();
            _mapper = new LocationSheetMapper(_reporterMock.Object);
        }

        [Fact]
        public void Map_WithMinimalValidValues_ShouldMap()
        {
            // Arrange
            const string region = "Region Name";
            const string locationGroup = "Location Group Name";
            const string resourceName = "Resource Name";
            const string name = "Location Name";
            const int sortIndex = 123;

            _values = new List<object>
            {
                region,
                locationGroup,
                resourceName,
                name,
                sortIndex
            };

            var expected = new Location
            {
                IdHash = _rowHash.IdHash,
                Hash = _rowHash.Hash,
                ImportSheetId = _rowHash.ImportSheetId,
                Name = name,
                LocationGroup = new LocationGroup
                {
                    Name = locationGroup,
                    ResourceName = resourceName,
                    RegionName = region
                },
                SortIndex = sortIndex,
                IsDiscoverable = false,
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
            const string region = "Region Name";
            const string locationGroup = "Location Group Name";
            const string resourceName = "Resource Name";
            const string name = "Location Name";
            const int sortIndex = 123;
            const bool isDiscoverable = true;
            const string notes = "Notes";

            _values = new List<object>
            {
                region,
                locationGroup,
                resourceName,
                name,
                sortIndex,
                isDiscoverable,
                notes
            };

            var expected = new Location
            {
                IdHash = _rowHash.IdHash,
                Hash = _rowHash.Hash,
                ImportSheetId = _rowHash.ImportSheetId,
                Name = name,
                LocationGroup = new LocationGroup
                {
                    Name = locationGroup,
                    ResourceName = resourceName,
                    RegionName = region
                },
                SortIndex = sortIndex,
                IsDiscoverable = isDiscoverable,
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
            const string region1 = "Region Name 1";
            const string locationGroup1 = "Location Group Name 1";
            const string resourceName1 = "Resource Name 1";
            const string name1 = "Location Name 1";
            const int sortIndex1 = 123;

            const string region2 = "Region Name 2";
            const string locationGroup2 = "Location Group Name 2";
            const string resourceName2 = "Resource Name 2";
            const string name2 = "Location Name 2";
            const int sortIndex2 = 123;

            var values1 = new List<object>
            {
                region1,
                locationGroup1,
                resourceName1,
                name1,
                sortIndex1
            };

            var values2 = new List<object>
            {
                region2,
                locationGroup2,
                resourceName2,
                name2,
                sortIndex2
            };

            var data = new List<SheetDataRow>
            {
                new(_columnNames, _rowHash, values1),
                new(_columnNames, _rowHash, values2)
            };

            var expected = new List<Location>
            {
                new()
                {
                    IdHash = _rowHash.IdHash,
                    Hash = _rowHash.Hash,
                    ImportSheetId = _rowHash.ImportSheetId,
                    Name = name1,
                    LocationGroup = new LocationGroup
                    {
                        Name = locationGroup1,
                        ResourceName = resourceName1,
                        RegionName = region1
                    },
                    SortIndex = sortIndex1,
                    IsDiscoverable = false,
                    Notes = string.Empty
                },
                new()
                {
                    IdHash = _rowHash.IdHash,
                    Hash = _rowHash.Hash,
                    ImportSheetId = _rowHash.ImportSheetId,
                    Name = name2,
                    LocationGroup = new LocationGroup
                    {
                        Name = locationGroup2,
                        ResourceName = resourceName2,
                        RegionName = region2
                    },
                    SortIndex = sortIndex2,
                    IsDiscoverable = false,
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
                x => x.ReportError(nameof(Location), _rowHash.IdHash, It.IsAny<Exception>()),
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
        [InlineData("", "b", "c", "d", 1, false, "")]
        [InlineData("a", "", "c", "d", 1, false, "")]
        [InlineData("a", "b", "", "d", 1, false, "")]
        [InlineData("a", "b", "c", "", 1, false, "")]
        [InlineData("a", "b", "c", "d", "notInt", false, "")]
        [InlineData("a", "b", "c", "d", 1, "notBool", "")]
        [InlineData("a", "b", "c", "d", 1, false, 1000)]
        public void Map_WithUnparsableValue_ShouldReportError(params object[] valuesAsArray)
        {
            // Arrange
            _values = valuesAsArray.ToList();

            // Act
            var actual = _mapper.Map(Data).ToList();

            // Assert
            actual.Count.Should().Be(0);
            _reporterMock.Verify(
                x => x.ReportError(nameof(Location), _rowHash.IdHash, It.IsAny<ParseException>()),
                Times.AtLeastOnce);
        }

        [Fact]
        public void Map_WithDifferentColumnOrder_ShouldParse()
        {
            // Arrange
            _columnNames = new List<string>
            {
                "Name",
                "Notes",
                "IsDiscoverable",
                "LocationGroup",
                "ResourceName",
                "SortIndex",
                "Region"
            };

            const string region = "Region Name";
            const string locationGroup = "Location Group Name";
            const string resourceName = "Resource Name";
            const string name = "Location Name";
            const int sortIndex = 123;
            const bool isDiscoverable = true;
            const string notes = "Notes";

            _values = new List<object>
            {
                name,
                notes,
                isDiscoverable,
                locationGroup,
                resourceName,
                sortIndex,
                region
            };

            var expected = new Location
            {
                IdHash = _rowHash.IdHash,
                Hash = _rowHash.Hash,
                ImportSheetId = _rowHash.ImportSheetId,
                Name = name,
                LocationGroup = new LocationGroup
                {
                    Name = locationGroup,
                    ResourceName = resourceName,
                    RegionName = region
                },
                SortIndex = sortIndex,
                IsDiscoverable = isDiscoverable,
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