using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using PokeOneWeb.DataSync.GoogleSpreadsheet.Exceptions;
using PokeOneWeb.DataSync.GoogleSpreadsheet.Import.Impl.Sheets.Regions;
using Xunit;

namespace PokeOneWeb.DataSync.Tests.GoogleSpreadsheet.Import.Impl.Sheets.Regions
{
    public class RegionSheetRowParserTest
    {
        [Fact]
        public void ReadRow_WithMinimalValidValues_ShouldParse()
        {
            // Arrange
            var parser = new RegionXSheetRowParser();

            var name = "Region Name";
            var resourceName = "Resource Name";
            var color = "#000000";
            var description = "Description";
            var isReleased = true;
            var isMainRegion = false;
            var isSideRegion = true;
            var isEventRegion = false;

            var values = new List<object>
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

            var expected = new RegionSheetDto
            {
                Name = name,
                ResourceName = resourceName,
                Color = color,
                Description = description,
                IsReleased = isReleased,
                IsMainRegion = isMainRegion,
                IsSideRegion = isSideRegion,
                IsEventRegion = isEventRegion
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
            var parser = new RegionXSheetRowParser();

            var name = "Region Name";
            var resourceName = "Resource Name";
            var color = "#000000";
            var description = "Description";
            var isReleased = true;
            var isMainRegion = false;
            var isSideRegion = true;
            var isEventRegion = false;
            var eventName = "Event Name";

            var values = new List<object>
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

            var expected = new RegionSheetDto
            {
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
            var result = parser.ReadRow(values);

            // Assert
            result.Should().BeEquivalentTo(expected);
        }

        [Fact]
        public void ReadRow_WithInsufficientValues_ShouldThrow()
        {
            // Arrange
            var parser = new RegionXSheetRowParser();
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
            var parser = new RegionXSheetRowParser();
            var values = new List<object>()
            {
                "0", "0", string.Empty, string.Empty, false, false, false, false, string.Empty, "excessive value"
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
            var parser = new RegionXSheetRowParser();
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
        [InlineData("0", "0", "", 0)] // Description must be string
        [InlineData("0", "0", "", "", "notBoolean")] // IsReleased must be boolean
        [InlineData("0", "0", "", "", false, "notBoolean")] // IsMainRegion must be boolean
        [InlineData("0", "0", "", "", false, false, "notBoolean")] // IsSideRegion must be boolean
        [InlineData("0", "0", "", "", false, false, false, "notBoolean")] // IsEventRegion must be boolean
        [InlineData("0", "0", "", "", false, false, false, false, 0)] // EventName must be string
        public void ReadRow_WithUnparsableValue_ShouldThrow(params object[] valuesAsArray)
        {
            // Arrange
            var parser = new RegionXSheetRowParser();
            var values = valuesAsArray.ToList();

            // Act
            var actual = () => parser.ReadRow(values);

            // Assert
            actual.Should().Throw<InvalidRowDataException>();
        }
    }
}