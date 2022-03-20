using FluentAssertions;
using PokeOneWeb.DataSync.GoogleSpreadsheet.Import.Impl;
using PokeOneWeb.DataSync.GoogleSpreadsheet.Import.Impl.Sheets.SeasonTimesOfDay;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace PokeOneWeb.DataSync.Tests.GoogleSpreadsheet.Import.Impl.Sheets.SeasonTimesOfDay
{
    public class SeasonTimeOfDaySheetRowParserTest
    {
        [Fact]
        public void ReadRow_WithMinimalValidValues_ShouldParse()
        {
            // Arrange
            var parser = new SeasonTimeOfDaySheetRowParser();

            var seasonName = "Season Name";
            var timeOfDayName = "TimeOfDay Name";
            var startHour = 1;
            var endHour = 2;

            var values = new List<object>
            {
                seasonName,
                timeOfDayName,
                startHour,
                endHour
            };

            var expected = new SeasonTimeOfDaySheetDto
            {
                SeasonName = seasonName,
                TimeOfDayName = timeOfDayName,
                StartHour = startHour,
                EndHour = endHour
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
            var parser = new SeasonTimeOfDaySheetRowParser();
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
            var parser = new SeasonTimeOfDaySheetRowParser();
            var values = new List<object>
            {
                "0", "0", 0, 0, "excessive value"
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
            var parser = new SeasonTimeOfDaySheetRowParser();
            List<object> values = null;

            // Act
            var actual = () => parser.ReadRow(values);

            // Assert
            actual.Should().Throw<InvalidRowDataException>();
        }

        [Theory]
        [InlineData("")] // SeasonName must be non-empty
        [InlineData("0", "")] // TimeOfDayName must be non-empty
        [InlineData("0", "0", "")] // StartHour must be int
        [InlineData("0", "0", 0, "")] // EndHour must be int
        public void ReadRow_WithUnparsableValue_ShouldThrow(params object[] valuesAsArray)
        {
            // Arrange
            var parser = new SeasonTimeOfDaySheetRowParser();
            var values = valuesAsArray.ToList();

            // Act
            var actual = () => parser.ReadRow(values);

            // Assert
            actual.Should().Throw<InvalidRowDataException>();
        }
    }
}
