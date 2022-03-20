using FluentAssertions;
using PokeOneWeb.DataSync.GoogleSpreadsheet.Import.Impl;
using PokeOneWeb.DataSync.GoogleSpreadsheet.Import.Impl.Sheets.MoveTutorMoves;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace PokeOneWeb.DataSync.Tests.GoogleSpreadsheet.Import.Impl.Sheets.MoveTutorMoves
{
    public class MoveTutorMoveSheetRowParserTest
    {
        [Fact]
        public void ReadRow_WithMinimalValidValues_ShouldParse()
        {
            // Arrange
            var parser = new MoveTutorMoveSheetRowParser();

            var moveTutorName = "Move Tutor Name";
            var moveName = "Move Name";

            var values = new List<object>
            {
                moveTutorName,
                moveName
            };

            var expected = new MoveTutorMoveSheetDto
            {
                MoveTutorName = moveTutorName,
                MoveName = moveName
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
            var parser = new MoveTutorMoveSheetRowParser();

            var moveTutorName = "Move Tutor Name";
            var moveName = "Move Name";
            var redShardPrice = 1;
            var blueShardPrice = 2;
            var greenShardPrice = 3;
            var yellowShardPrice = 4;
            var pwtBpPrice = 10;
            var bfBpPrice = 20;
            var pokeDollarPrice = 1000;
            var pokeGoldPrice = 100;

            var values = new List<object>
            {
                moveTutorName,
                moveName,
                redShardPrice,
                blueShardPrice,
                greenShardPrice,
                yellowShardPrice,
                pwtBpPrice,
                bfBpPrice,
                pokeDollarPrice,
                pokeGoldPrice
            };

            var expected = new MoveTutorMoveSheetDto
            {
                MoveTutorName = moveTutorName,
                MoveName = moveName,
                RedShardPrice = redShardPrice,
                BlueShardPrice = blueShardPrice,
                GreenShardPrice = greenShardPrice,
                YellowShardPrice = yellowShardPrice,
                PWTBPPrice = pwtBpPrice,
                BFBPPrice = bfBpPrice,
                PokeDollarPrice = pokeDollarPrice,
                PokeGoldPrice = pokeGoldPrice
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
            var parser = new MoveTutorMoveSheetRowParser();
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
            var parser = new MoveTutorMoveSheetRowParser();
            var values = new List<object>()
            {
                "0", "0", 0, 0, 0, 0, 0, 0, 0, 0, "excessive value"
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
            var parser = new MoveTutorMoveSheetRowParser();
            List<object> values = null;

            // Act
            var actual = () => parser.ReadRow(values);

            // Assert
            actual.Should().Throw<InvalidRowDataException>();
        }

        [Theory]
        [InlineData("")] // MoveTutorName must be non-empty
        [InlineData("0", "")] // MoveName must be non-empty
        [InlineData("0", "0", "")] // RedShardPrice must be int
        [InlineData("0", "0", 0, "")] // BlueShardPrice must be int
        [InlineData("0", "0", 0, 0, "")] // GreenShardPrice must be int
        [InlineData("0", "0", 0, 0, 0, "")] // YellowShardPrice must be int
        [InlineData("0", "0", 0, 0, 0, 0, "")] // PWTBPPrice must be int
        [InlineData("0", "0", 0, 0, 0, 0, 0, "")] // BFBPPrice must be int
        [InlineData("0", "0", 0, 0, 0, 0, 0, 0, "")] // PokeDollarPrice must be int
        [InlineData("0", "0", 0, 0, 0, 0, 0, 0, 0, "")] // PokeGoldPrice must be int
        public void ReadRow_WithUnparsableValue_ShouldThrow(params object[] valuesAsArray)
        {
            // Arrange
            var parser = new MoveTutorMoveSheetRowParser();
            var values = valuesAsArray.ToList();

            // Act
            var actual = () => parser.ReadRow(values);

            // Assert
            actual.Should().Throw<InvalidRowDataException>();
        }
    }
}
