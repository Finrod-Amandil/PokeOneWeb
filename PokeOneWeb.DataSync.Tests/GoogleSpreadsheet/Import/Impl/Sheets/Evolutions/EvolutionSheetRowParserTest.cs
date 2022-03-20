using FluentAssertions;
using PokeOneWeb.DataSync.GoogleSpreadsheet.Import.Impl;
using PokeOneWeb.DataSync.GoogleSpreadsheet.Import.Impl.Sheets.Evolutions;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace PokeOneWeb.DataSync.Tests.GoogleSpreadsheet.Import.Impl.Sheets.Evolutions
{
    public class EvolutionSheetRowParserTest
    {
        [Fact]
        public void ReadRow_WithMinimalValidValues_ShouldParse()
        {
            // Arrange
            var parser = new EvolutionSheetRowParser();

            var basePokemonSpeciesPokedexNumber = 1;
            var basePokemonSpeciesName = "Base Pokemon Species Name";
            var basePokemonVarietyName = "Base Pokemon Variety Name";
            var baseStage = 0;
            var evolvedPokemonVarietyName = "Evolved Pokemon Variety Name";
            var evolvedStage = 1;
            var evolutionTrigger = "Evolution Trigger";
            var isReversible = false;
            var isAvailable = true;
            var doInclude = true;

            var values = new List<object>
            {
                basePokemonSpeciesPokedexNumber,
                basePokemonSpeciesName,
                basePokemonVarietyName,
                baseStage,
                evolvedPokemonVarietyName,
                evolvedStage,
                evolutionTrigger,
                isReversible,
                isAvailable,
                doInclude
            };

            var expected = new EvolutionSheetDto
            {
                BasePokemonSpeciesPokedexNumber = basePokemonSpeciesPokedexNumber,
                BasePokemonSpeciesName = basePokemonSpeciesName,
                BasePokemonVarietyName = basePokemonVarietyName,
                BaseStage = baseStage,
                EvolvedPokemonVarietyName = evolvedPokemonVarietyName,
                EvolvedStage = evolvedStage,
                EvolutionTrigger = evolutionTrigger,
                IsReversible = isReversible,
                IsAvailable = isAvailable,
                DoInclude = doInclude
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
            var parser = new EvolutionSheetRowParser();
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
            var parser = new EvolutionSheetRowParser();
            var values = new List<object>()
            {
                1, "0", "0", 0, "0", 1, "0", false, false, false, "excessive value"
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
            var parser = new EvolutionSheetRowParser();
            List<object> values = null;

            // Act
            var actual = () => parser.ReadRow(values);

            // Assert
            actual.Should().Throw<InvalidRowDataException>();
        }

        [Theory]
        [InlineData("notInt")] // BasePokemonSpeciesPokedexNumber must be int
        [InlineData(1, "")] // BasePokemonSpeciesName must be non-empty
        [InlineData(1, "0", "")] // BasePokemonVarietyName must be non-empty
        [InlineData(1, "0", "0", "notInt")] // BaseStage must be int
        [InlineData(1, "0", "0", 0, "")] // EvolvedPokemonVarietyName must be non-empty
        [InlineData(1, "0", "0", 0, "0", "notInt")] // EvolvedStage must be int
        [InlineData(1, "0", "0", 0, "0", 1, "")] // EvolutionTrigger must be non-empty
        [InlineData(1, "0", "0", 0, "0", 1, "0", "notBoolean")] // isReversible must be boolean
        [InlineData(1, "0", "0", 0, "0", 1, "0", false, "notBoolean")] // isAvailable must be boolean
        [InlineData(1, "0", "0", 0, "0", 1, "0", false, false, "notBoolean")] // doInclude must be boolean
        public void ReadRow_WithUnparsableValue_ShouldThrow(params object[] valuesAsArray)
        {
            // Arrange
            var parser = new EvolutionSheetRowParser();
            var values = valuesAsArray.ToList();

            // Act
            var actual = () => parser.ReadRow(values);

            // Assert
            actual.Should().Throw<InvalidRowDataException>();
        }
    }
}
