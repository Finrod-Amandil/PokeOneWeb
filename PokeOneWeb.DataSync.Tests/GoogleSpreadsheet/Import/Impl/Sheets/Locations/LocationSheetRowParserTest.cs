using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using PokeOneWeb.DataSync.GoogleSpreadsheet.Exceptions;
using PokeOneWeb.DataSync.GoogleSpreadsheet.Import.Impl.Sheets.Locations;
using Xunit;

namespace PokeOneWeb.DataSync.Tests.GoogleSpreadsheet.Import.Impl.Sheets.Locations
{
    public class LocationSheetRowParserTest
    {
        [Fact]
        public void ReadRow_WithMinimalValidValues_ShouldParse()
        {
            // Arrange
            var parser = new LocationXSheetRowParser();

            var regionName = "Region Name";
            var locationGroupName = "Location Group Name";
            var resourceName = "Resource Name";
            var locationName = "Location Name";
            var sortIndex = 1;
            var isDiscoverable = false;

            var values = new List<object>
            {
                regionName, locationGroupName, resourceName, locationName,
                sortIndex, isDiscoverable
            };

            var expected = new LocationSheetDto
            {
                RegionName = regionName,
                LocationGroupName = locationGroupName,
                ResourceName = resourceName,
                LocationName = locationName,
                SortIndex = sortIndex,
                IsDiscoverable = isDiscoverable
            };

            // Act
            var result = parser.ReadRow(values);

            // Assert
            result.Should().BeEquivalentTo(expected);
        }

        [Fact]
        public void ReadRow_WithAllValidValues_ShouldParse()
        {
            // Arrange
            var parser = new LocationXSheetRowParser();

            var regionName = "Region Name";
            var locationGroupName = "Location Group Name";
            var resourceName = "Resource Name";
            var locationName = "Location Name";
            var sortIndex = 1;
            var isDiscoverable = false;
            var notes = "Notes";

            var values = new List<object>
            {
                regionName, locationGroupName, resourceName, locationName,
                sortIndex, isDiscoverable, notes
            };

            var expected = new LocationSheetDto
            {
                RegionName = regionName,
                LocationGroupName = locationGroupName,
                ResourceName = resourceName,
                LocationName = locationName,
                SortIndex = sortIndex,
                IsDiscoverable = isDiscoverable,
                Notes = notes
            };

            // Act
            var result = parser.ReadRow(values);

            // Assert
            result.Should().BeEquivalentTo(expected);
        }

        [Fact]
        public void ReadRow_WithInsufficientValues_ShouldThrow()
        {
            // Arrange
            var parser = new LocationXSheetRowParser();
            var values = new List<object>();

            // Act
            var actual = () => parser.ReadRow(values);

            // Assert
            actual.Should().Throw<InvalidRowDataException>();
        }

        [Fact]
        public void ReadRow_WithTooManyValues_ShouldThrow()
        {
            // Arrange
            var parser = new LocationXSheetRowParser();
            var values = new List<object>()
            {
                "0", string.Empty, string.Empty, 1, 1, 1, 1, 1, 1, string.Empty, "excessive value"
            };

            // Act
            var actual = () => parser.ReadRow(values);

            // Assert
            actual.Should().Throw<InvalidRowDataException>();
        }

        [Fact]
        public void ReadRow_WithValuesNull_ShouldThrow()
        {
            // Arrange
            var parser = new LocationXSheetRowParser();
            List<object> values = null;

            // Act
            var actual = () => parser.ReadRow(values);

            // Assert
            actual.Should().Throw<InvalidRowDataException>();
        }

        [Theory]
        [InlineData("")] // RegionName must be non-empty
        [InlineData("0", "")] // LocationGroupName must be non-empty
        [InlineData("0", "0", "")] // ResourceName must be non-empty
        [InlineData("0", "0", "0", "")] // LocationName must be non-empty
        [InlineData("0", "0", "0", "0", "notInt")] // SortIndex must be int
        [InlineData("0", "0", "0", "0", 1, "")] // IsDiscoverable must be boolean
        [InlineData("0", "0", "0", "0", 1, false, 0)] // Notes must be string
        public void ReadRow_WithUnparsableValue_ShouldThrow(params object[] valuesAsArray)
        {
            // Arrange
            var parser = new LocationXSheetRowParser();
            var values = valuesAsArray.ToList();

            // Act
            var actual = () => parser.ReadRow(values);

            // Assert
            actual.Should().Throw<InvalidRowDataException>();
        }
    }
}