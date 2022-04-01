using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using PokeOneWeb.DataSync.GoogleSpreadsheet.Import.Impl;
using PokeOneWeb.DataSync.GoogleSpreadsheet.Import.Impl.Sheets.SpawnTypes;
using Xunit;

namespace PokeOneWeb.DataSync.Tests.GoogleSpreadsheet.Import.Impl.Sheets.SpawnTypes
{
    public class SpawnTypeSheetRowParserTest
    {
        [Fact]
        public void ReadRow_WithMinimalValidValues_ShouldParse()
        {
            // Arrange
            var parser = new SpawnTypeSheetRowParser();

            var name = "SpawnType Name";
            var sortIndex = 1;
            var isSyncable = false;
            var isInfinite = true;
            var color = "#000000";

            var values = new List<object>
            {
                name,
                sortIndex,
                isSyncable,
                isInfinite,
                color
            };

            var expected = new SpawnTypeSheetDto
            {
                Name = name,
                SortIndex = sortIndex,
                IsSyncable = isSyncable,
                IsInfinite = isInfinite,
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
            var parser = new SpawnTypeSheetRowParser();
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
            var parser = new SpawnTypeSheetRowParser();
            var values = new List<object>
            {
                "0", 0, false, false, "0", "excessive value"
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
            var parser = new SpawnTypeSheetRowParser();
            List<object> values = null;

            // Act
            var actual = () => parser.ReadRow(values);

            // Assert
            actual.Should().Throw<InvalidRowDataException>();
        }

        [Theory]
        [InlineData("")] // Name must be non-empty
        [InlineData("0", "notInt")] // SortIndex must be int
        [InlineData("0", 0, "notBoolean")] // IsSyncable must be boolean
        [InlineData("0", 0, false, "notBoolean")] // IsInfinite must be boolean
        [InlineData("0", 0, false, false, 0)] // Color must be string
        public void ReadRow_WithUnparsableValue_ShouldThrow(params object[] valuesAsArray)
        {
            // Arrange
            var parser = new SpawnTypeSheetRowParser();
            var values = valuesAsArray.ToList();

            // Act
            var actual = () => parser.ReadRow(values);

            // Assert
            actual.Should().Throw<InvalidRowDataException>();
        }
    }
}