using FluentAssertions;
using PokeOneWeb.DataSync.GoogleSpreadsheet.Import.Impl;
using PokeOneWeb.DataSync.GoogleSpreadsheet.Import.Impl.Sheets.LearnableMoveLearnMethods;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace PokeOneWeb.DataSync.Tests.GoogleSpreadsheet.Import.Impl.Sheets.LearnableMoveLearnMethods
{
    public class LearnableMoveLearnMethodSheetRowParserTest
    {
        [Fact]
        public void ReadRow_WithMinimalValidValues_ShouldParse()
        {
            // Arrange
            var parser = new LearnableMoveLearnMethodSheetRowParser();

            var pokemonSpeciesPokedexNumber = 1;
            var pokemonVarietyName = "Pokemon Variety Name";
            var moveName = "Move Name";
            var learnMethod = "Learn Method";
            var isAvailable = true;

            var values = new List<object>
            {
                pokemonSpeciesPokedexNumber, pokemonVarietyName,
                moveName, learnMethod, isAvailable
            };

            var expected = new LearnableMoveLearnMethodSheetDto
            {
                PokemonSpeciesPokedexNumber = pokemonSpeciesPokedexNumber,
                PokemonVarietyName = pokemonVarietyName,
                MoveName = moveName,
                LearnMethod = learnMethod,
                IsAvailable = isAvailable
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
            var parser = new LearnableMoveLearnMethodSheetRowParser();

            var pokemonSpeciesPokedexNumber = 1;
            var pokemonVarietyName = "Pokemon Variety Name";
            var moveName = "Move Name";
            var learnMethod = "Learn Method";
            var isAvailable = true;
            var generation = "Generation I";
            var levelLearnedAt = 1;
            var requiredItemName = "HM01 - Cut";
            var tutorName = "Tutor Name";
            var tutorLocation = "Tutor Location";
            var comments = "Comment";

            var values = new List<object>
            {
                pokemonSpeciesPokedexNumber, pokemonVarietyName,
                moveName, learnMethod, isAvailable,
                generation, levelLearnedAt, requiredItemName,
                tutorName, tutorLocation, comments
            };

            var expected = new LearnableMoveLearnMethodSheetDto
            {
                PokemonSpeciesPokedexNumber = pokemonSpeciesPokedexNumber,
                PokemonVarietyName = pokemonVarietyName,
                MoveName = moveName,
                LearnMethod = learnMethod,
                IsAvailable = isAvailable,
                Generation = generation,
                LevelLearnedAt = levelLearnedAt,
                RequiredItemName = requiredItemName,
                TutorName = tutorName,
                TutorLocation = tutorLocation,
                Comments = comments
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
            var parser = new LearnableMoveLearnMethodSheetRowParser();
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
            var parser = new LearnableMoveLearnMethodSheetRowParser();
            var values = new List<object>()
            {
                0, "0", "0", "0", false, "", 1, "", "", "", "", "excessive value"
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
            var parser = new LearnableMoveLearnMethodSheetRowParser();
            List<object> values = null;

            // Act
            var actual = () => parser.ReadRow(values);

            // Assert
            actual.Should().Throw<InvalidRowDataException>();
        }

        [Theory]
        [InlineData("")] // PokemonSpeciesPokedexNumber must be int
        [InlineData(1, "")] // PokemonVarietyName must be non-empty
        [InlineData(1, "0", "")] // MoveName must be non-empty
        [InlineData(1, "0", "0", "")] // LearnMethod must be non-empty
        [InlineData(1, "0", "0", "0", "x")] // IsAvailable must be boolean
        [InlineData(1, "0", "0", "0", false, 0)] // Generation must be string
        [InlineData(1, "0", "0", "0", false, "", "x")] // LevelLearnedAt must be int
        [InlineData(1, "0", "0", "0", false, "", 1, 0)] // RequiredItemName must be string
        [InlineData(1, "0", "0", "0", false, "", 1, "", 0)] // TutorName must be string
        [InlineData(1, "0", "0", "0", false, "", 1, "", "", 0)] // TutorLocation must be string
        [InlineData(1, "0", "0", "0", false, "", 1, "", "", "", 0)] // Comments must be string
        public void ReadRow_WithUnparsableValue_ShouldThrow(params object[] valuesAsArray)
        {
            // Arrange
            var parser = new LearnableMoveLearnMethodSheetRowParser();
            var values = valuesAsArray.ToList();

            // Act
            var actual = () => parser.ReadRow(values);

            // Assert
            actual.Should().Throw<InvalidRowDataException>();
        }
    }
}
