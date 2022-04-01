using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using PokeOneWeb.DataSync.GoogleSpreadsheet.Import.Impl;
using PokeOneWeb.DataSync.GoogleSpreadsheet.Import.Impl.Sheets.Builds;
using Xunit;

namespace PokeOneWeb.DataSync.Tests.GoogleSpreadsheet.Import.Impl.Sheets.Builds
{
    public class BuildSheetRowParserTest
    {
        [Fact]
        public void ReadRow_WithMinimalValidValues_ShouldParse()
        {
            // Arrange
            var parser = new BuildSheetRowParser();
            var pokemonVarietyName = "Build PokemonVarietyName";
            var values = new List<object> { pokemonVarietyName };

            var expected = new BuildSheetDto { PokemonVarietyName = pokemonVarietyName };

            // Act
            var result = parser.ReadRow(values);

            // Assert
            result.Should().BeEquivalentTo(expected);
        }

        [Fact]
        public void ReadRow_WithAllValidValues_ShouldParse()
        {
            // Arrange
            var parser = new BuildSheetRowParser();

            var pokemonVarietyName = "PokemonVarietyName";
            var buildName = "BuildName";
            var buildDescription = "BuildDescription";
            var move1 = "Move 1";
            var move2 = "Move 2";
            var move3 = "Move 3";
            var move4 = "Move 4";
            var item = "Item";
            var nature = "Nature";
            var ability = "Ability";
            var evDistribution = "EvDistribution";

            var values = new List<object>
            {
                pokemonVarietyName, buildName, buildDescription,
                move1, move2, move3, move4,
                item, nature, ability, evDistribution
            };

            var expected = new BuildSheetDto
            {
                PokemonVarietyName = pokemonVarietyName,
                BuildName = buildName,
                BuildDescription = buildDescription,
                Move1 = move1,
                Move2 = move2,
                Move3 = move3,
                Move4 = move4,
                Item = item,
                Nature = nature,
                Ability = ability,
                EvDistribution = evDistribution
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
            var parser = new BuildSheetRowParser();
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
            var parser = new BuildSheetRowParser();
            var values = new List<object>()
            {
                "0", string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, "excessive value"
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
            var parser = new BuildSheetRowParser();
            List<object> values = null;

            // Act
            var actual = () => parser.ReadRow(values);

            // Assert
            actual.Should().Throw<InvalidRowDataException>();
        }

        [Theory]
        [InlineData("")] // PokemonVarietyName must be non-empty
        [InlineData("", 0)] // BuildName must string
        [InlineData("", "", 0)] // BuildDescription must string
        [InlineData("", "", "", 0)] // Move1 must string
        [InlineData("", "", "", "", 0)] // Move2 must string
        [InlineData("", "", "", "", "", 0)] // Move3 must string
        [InlineData("", "", "", "", "", "", 0)] // Move4 must string
        [InlineData("", "", "", "", "", "", "", 0)] // Item must string
        [InlineData("", "", "", "", "", "", "", "", 0)] // Nature must string
        [InlineData("", "", "", "", "", "", "", "", "", 0)] // Ability must string
        [InlineData("", "", "", "", "", "", "", "", "", "", 0)] // EvDistribution must string
        public void ReadRow_WithUnparsableValue_ShouldThrow(params object[] valuesAsArray)
        {
            // Arrange
            var parser = new BuildSheetRowParser();
            var values = valuesAsArray.ToList();

            // Act
            var actual = () => parser.ReadRow(values);

            // Assert
            actual.Should().Throw<InvalidRowDataException>();
        }
    }
}