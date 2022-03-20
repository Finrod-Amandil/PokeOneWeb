using FluentAssertions;
using PokeOneWeb.DataSync.GoogleSpreadsheet.Import.Impl;
using PokeOneWeb.DataSync.GoogleSpreadsheet.Import.Impl.Sheets.Regions;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace PokeOneWeb.DataSync.Tests.GoogleSpreadsheet.Import.Impl.Sheets.Regions
{
    public class RegionSheetRowParserTest
    {
        [Fact]
        public void ReadRow_WithMinimalValidValues_ShouldParse()
        {
            // Arrange
            var parser = new RegionSheetRowParser();

            var name = "Region Name";
            var resourceName = "Resource Name";

            var values = new List<object>
            {
                name,
                resourceName
            };

            var expected = new RegionSheetDto
            {
                Name = name,
                ResourceName = resourceName
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
            var parser = new RegionSheetRowParser();

            var name = "Region Name";
            var resourceName = "Resource Name";
            var color = "#000000";
            var isEventRegion = false;
            var eventName = "Event Name";

            var values = new List<object>
            {
                name,
                resourceName,
                color,
                isEventRegion,
                eventName
            };

            var expected = new RegionSheetDto
            {
                Name = name,
                ResourceName = resourceName,
                Color = color,
                IsEventRegion = isEventRegion,
                EventName = eventName
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
            var parser = new RegionSheetRowParser();
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
            var parser = new RegionSheetRowParser();
            var values = new List<object>()
            {
                "0", "0", "", false, "", "excessive value"
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
            var parser = new RegionSheetRowParser();
            List<object> values = null;

            // Act
            var actual = () => parser.ReadRow(values);

            // Assert
            actual.Should().Throw<InvalidRowDataException>();
        }

        [Theory]
        [InlineData("")] // Name must be non-empty
        [InlineData("0", "")] // ResourceName must be non-empty
        [InlineData("0", "0", 0)] // Color must be string
        [InlineData("0", "0", "", "")] // IsEventRegion must be boolean
        [InlineData("0", "0", "", false, 0)] // EventName must be string
        public void ReadRow_WithUnparsableValue_ShouldThrow(params object[] valuesAsArray)
        {
            // Arrange
            var parser = new RegionSheetRowParser();
            var values = valuesAsArray.ToList();

            // Act
            var actual = () => parser.ReadRow(values);

            // Assert
            actual.Should().Throw<InvalidRowDataException>();
        }
    }
}
