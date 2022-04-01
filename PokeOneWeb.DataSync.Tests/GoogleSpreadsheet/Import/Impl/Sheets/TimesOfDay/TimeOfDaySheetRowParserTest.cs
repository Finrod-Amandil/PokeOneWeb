using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using PokeOneWeb.DataSync.GoogleSpreadsheet.Import.Impl;
using PokeOneWeb.DataSync.GoogleSpreadsheet.Import.Impl.Sheets.TimesOfDay;
using Xunit;

namespace PokeOneWeb.DataSync.Tests.GoogleSpreadsheet.Import.Impl.Sheets.TimesOfDay
{
    public class TimeOfDaySheetRowParserTest
    {
        [Fact]
        public void ReadRow_WithMinimalValidValues_ShouldParse()
        {
            // Arrange
            var parser = new TimeOfDaySheetRowParser();

            var sortIndex = 0;
            var name = "Time of Day Name";
            var abbreviation = "M";

            var values = new List<object>
            {
                sortIndex,
                name,
                abbreviation
            };

            var expected = new TimeOfDaySheetDto
            {
                SortIndex = sortIndex,
                Name = name,
                Abbreviation = abbreviation
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
            var parser = new TimeOfDaySheetRowParser();

            var sortIndex = 0;
            var name = "Time of Day Name";
            var abbreviation = "M";
            var color = "#000000";

            var values = new List<object>
            {
                sortIndex,
                name,
                abbreviation,
                color
            };

            var expected = new TimeOfDaySheetDto
            {
                SortIndex = sortIndex,
                Name = name,
                Abbreviation = abbreviation,
                Color = color
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
            var parser = new TimeOfDaySheetRowParser();
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
            var parser = new TimeOfDaySheetRowParser();
            var values = new List<object>
            {
                0, "0", "0", string.Empty, "excessive value"
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
            var parser = new TimeOfDaySheetRowParser();
            List<object> values = null;

            // Act
            var actual = () => parser.ReadRow(values);

            // Assert
            actual.Should().Throw<InvalidRowDataException>();
        }

        [Theory]
        [InlineData("notInt")] // SortIndex must be int
        [InlineData(0, "")] // Name must be non-empty
        [InlineData(0, "0", "")] // Abbreviation must be non-empty
        [InlineData(0, "0", "0", 0)] // Color must be string
        public void ReadRow_WithUnparsableValue_ShouldThrow(params object[] valuesAsArray)
        {
            // Arrange
            var parser = new TimeOfDaySheetRowParser();
            var values = valuesAsArray.ToList();

            // Act
            var actual = () => parser.ReadRow(values);

            // Assert
            actual.Should().Throw<InvalidRowDataException>();
        }
    }
}