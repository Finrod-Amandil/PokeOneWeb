using FluentAssertions;
using PokeOneWeb.DataSync.GoogleSpreadsheet.Import.Impl;
using PokeOneWeb.DataSync.GoogleSpreadsheet.Import.Impl.Sheets.Spawns;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace PokeOneWeb.DataSync.Tests.GoogleSpreadsheet.Import.Impl.Sheets.Spawns
{
    public class SpawnSheetRowParserTest
    {
        [Fact]
        public void ReadRow_WithMinimalValidValues_ShouldParse()
        {
            // Arrange
            var parser = new SpawnSheetRowParser();

            var locationName = "Location Name";
            var pokemonForm = "Pokemon Form";
            var season = "Season";
            var timeOfDay = "TimeOfDay";
            var spawnType = "Spawn Type";

            var values = new List<object>
            {
                locationName,
                pokemonForm,
                season,
                timeOfDay,
                spawnType
            };

            var expected = new SpawnSheetDto
            {
                LocationName = locationName,
                PokemonForm = pokemonForm,
                Season = season,
                TimeOfDay = timeOfDay,
                SpawnType = spawnType
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
            var parser = new SpawnSheetRowParser();

            var locationName = "Location Name";
            var pokemonForm = "Pokemon Form";
            var season = "Season";
            var timeOfDay = "TimeOfDay";
            var spawnType = "Spawn Type";
            var spawnCommonality = "Common";
            var spawnProbability = "50%";
            var encounterCount = 10;
            var isConfirmed = true;
            var lowestLvl = 20;
            var highestLvl = 30;
            var notes = "Notes";

            var values = new List<object>
            {
                locationName,
                pokemonForm,
                season,
                timeOfDay,
                spawnType,
                spawnCommonality,
                spawnProbability,
                encounterCount,
                isConfirmed,
                lowestLvl,
                highestLvl,
                notes
            };

            var expected = new SpawnSheetDto
            {
                LocationName = locationName,
                PokemonForm = pokemonForm,
                Season = season,
                TimeOfDay = timeOfDay,
                SpawnType = spawnType,
                SpawnCommonality = spawnCommonality,
                SpawnProbability = spawnProbability,
                EncounterCount = encounterCount,
                IsConfirmed = isConfirmed,
                LowestLvl = lowestLvl,
                HighestLvl = highestLvl,
                Notes = notes
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
            var parser = new SpawnSheetRowParser();
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
            var parser = new SpawnSheetRowParser();
            var values = new List<object>
            {
                "0", "0", "0", "0", "0", "", "", 0, false, 0, 0, "", "excessive value"
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
            var parser = new SpawnSheetRowParser();
            List<object> values = null;

            // Act
            var actual = () => parser.ReadRow(values);

            // Assert
            actual.Should().Throw<InvalidRowDataException>();
        }

        [Theory]
        [InlineData("")] // LocationName must be non-empty
        [InlineData("0", "")] // PokemonForm must be non-empty
        [InlineData("0", "0", "")] // Season must be non-empty
        [InlineData("0", "0", "0", "")] // TimeOfDay must be non-empty
        [InlineData("0", "0", "0", "0", "")] // SpawnType must be non-empty
        [InlineData("0", "0", "0", "0", "0", 0)] // SpawnCommonality must string
        [InlineData("0", "0", "0", "0", "0", "", 0)] // SpawnProbability must be string
        [InlineData("0", "0", "0", "0", "0", "", "", "notInt")] // EncounterCount must be int
        [InlineData("0", "0", "0", "0", "0", "", "", 0, "notBoolean")] // IsConfirmed must be boolean
        [InlineData("0", "0", "0", "0", "0", "", "", 0, false, "notInt")] // LowestLvl must be int
        [InlineData("0", "0", "0", "0", "0", "", "", 0, false, 0, "notInt")] // HighestLvl must be int
        [InlineData("0", "0", "0", "0", "0", "", "", 0, false, 0, 0, 0)] // Notes must be string
        public void ReadRow_WithUnparsableValue_ShouldThrow(params object[] valuesAsArray)
        {
            // Arrange
            var parser = new SpawnSheetRowParser();
            var values = valuesAsArray.ToList();

            // Act
            var actual = () => parser.ReadRow(values);

            // Assert
            actual.Should().Throw<InvalidRowDataException>();
        }

        [Fact]
        public void ReadRow_WithMissingOptionalNonStringValues_ShouldParse()
        {
            // Arrange
            var parser = new SpawnSheetRowParser();

            var locationName = "Location Name";
            var pokemonForm = "Pokemon Form";
            var season = "Season";
            var timeOfDay = "TimeOfDay";
            var spawnType = "Spawn Type";
            var spawnCommonality = "Common";
            var spawnProbability = "50%";
            var encounterCount = "";
            var isConfirmed = "";
            var lowestLvl = "";
            var highestLvl = "";
            var notes = "Notes";

            var values = new List<object>
            {
                locationName,
                pokemonForm,
                season,
                timeOfDay,
                spawnType,
                spawnCommonality,
                spawnProbability,
                encounterCount,
                isConfirmed,
                lowestLvl,
                highestLvl,
                notes
            };

            var defaultValue = 0;
            var defaultIsConfirmed = true;

            var expected = new SpawnSheetDto
            {
                LocationName = locationName,
                PokemonForm = pokemonForm,
                Season = season,
                TimeOfDay = timeOfDay,
                SpawnType = spawnType,
                SpawnCommonality = spawnCommonality,
                SpawnProbability = spawnProbability,
                EncounterCount = defaultValue,
                IsConfirmed = defaultIsConfirmed,
                LowestLvl = defaultValue,
                HighestLvl = defaultValue,
                Notes = notes
            };

            // Act
            var result = parser.ReadRow(values);

            // Assert
            result.Should().BeEquivalentTo(expected);
        }
    }
}
