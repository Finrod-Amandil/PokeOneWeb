using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using PokeOneWeb.DataSync.GoogleSpreadsheet.Import.Impl;
using PokeOneWeb.DataSync.GoogleSpreadsheet.Import.Impl.Sheets.Natures;
using Xunit;

namespace PokeOneWeb.DataSync.Tests.GoogleSpreadsheet.Import.Impl.Sheets.Natures
{
    public class NatureSheetRowParserTest
    {
        [Fact]
        public void ReadRow_WithMinimalValidValues_ShouldParse()
        {
            // Arrange
            var parser = new NatureSheetRowParser();

            var name = "Nature name";
            var attack = 1;
            var specialAttack = 2;
            var defense = 3;
            var specialDefense = 4;
            var speed = 5;

            var values = new List<object>
            {
                name,
                attack,
                specialAttack,
                defense,
                specialDefense,
                speed
            };

            var expected = new NatureSheetDto
            {
                Name = name,
                Attack = attack,
                SpecialAttack = specialAttack,
                Defense = defense,
                SpecialDefense = specialDefense,
                Speed = speed
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
            var parser = new NatureSheetRowParser();
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
            var parser = new NatureSheetRowParser();
            var values = new List<object>
            {
                "0", 0, 0, 0, 0, 0, "excessive value"
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
            var parser = new NatureSheetRowParser();
            List<object> values = null;

            // Act
            var actual = () => parser.ReadRow(values);

            // Assert
            actual.Should().Throw<InvalidRowDataException>();
        }

        [Theory]
        [InlineData("")] // Name must be non-empty
        [InlineData("0", "notInt")] // Attack must be int
        [InlineData("0", 0, "notInt")] // SpecialAttack must be int
        [InlineData("0", 0, 0, "notInt")] // Defense must be int
        [InlineData("0", 0, 0, 0, "notInt")] // SpecialDefense must be int
        [InlineData("0", 0, 0, 0, 0, "notInt")] // Speed must be int
        public void ReadRow_WithUnparsableValue_ShouldThrow(params object[] valuesAsArray)
        {
            // Arrange
            var parser = new NatureSheetRowParser();
            var values = valuesAsArray.ToList();

            // Act
            var actual = () => parser.ReadRow(values);

            // Assert
            actual.Should().Throw<InvalidRowDataException>();
        }
    }
}