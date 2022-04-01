using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using PokeOneWeb.DataSync.GoogleSpreadsheet.Import.Impl;
using PokeOneWeb.DataSync.GoogleSpreadsheet.Import.Impl.Sheets.MoveLearnMethodLocations;
using Xunit;

namespace PokeOneWeb.DataSync.Tests.GoogleSpreadsheet.Import.Impl.Sheets.MoveLearnMethodLocations
{
    public class MoveLearnMethodLocationSheetRowParserTest
    {
        [Fact]
        public void ReadRow_WithMinimalValidValues_ShouldParse()
        {
            // Arrange
            var parser = new MoveLearnMethodLocationSheetRowParser();

            var moveLearnMethodName = "Move Learn Method Name";
            var tutorType = "Tutor Type";
            var npcName = "NPC Name";
            var locationName = "Location Name";

            var values = new List<object>
            {
                moveLearnMethodName, tutorType, npcName, locationName
            };

            var expected = new MoveLearnMethodLocationSheetDto
            {
                MoveLearnMethodName = moveLearnMethodName,
                TutorType = tutorType,
                NpcName = npcName,
                LocationName = locationName
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
            var parser = new MoveLearnMethodLocationSheetRowParser();

            var moveLearnMethodName = "Move Learn Method Name";
            var tutorType = "Tutor Type";
            var npcName = "NPC Name";
            var locationName = "Location Name";
            var placementDescription = "Placement Description";
            var pokeDollarPrice = 1000;
            var pokeGoldPrice = 100;
            var bigMushroomPrice = 1;
            var heartScalePrice = 1;

            var values = new List<object>
            {
                moveLearnMethodName, tutorType, npcName, locationName,
                placementDescription, pokeDollarPrice, pokeGoldPrice,
                bigMushroomPrice, heartScalePrice
            };

            var expected = new MoveLearnMethodLocationSheetDto
            {
                MoveLearnMethodName = moveLearnMethodName,
                TutorType = tutorType,
                NpcName = npcName,
                LocationName = locationName,
                PlacementDescription = placementDescription,
                PokeDollarPrice = pokeDollarPrice,
                PokeGoldPrice = pokeGoldPrice,
                BigMushroomPrice = bigMushroomPrice,
                HeartScalePrice = heartScalePrice
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
            var parser = new MoveLearnMethodLocationSheetRowParser();
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
            var parser = new MoveLearnMethodLocationSheetRowParser();
            var values = new List<object>
            {
                "0", "0", "0", "0", string.Empty, 0, 0, 0, 0, "excessive value"
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
            var parser = new MoveLearnMethodLocationSheetRowParser();
            List<object> values = null;

            // Act
            var actual = () => parser.ReadRow(values);

            // Assert
            actual.Should().Throw<InvalidRowDataException>();
        }

        [Theory]
        [InlineData("")] // MoveLearnMethodName must be non-empty
        [InlineData("0", "")] // TutorType must be non-empty
        [InlineData("0", "0", "")] // NpcName must be non-empty
        [InlineData("0", "0", "0", "")] // LocationName must be non-empty
        [InlineData("0", "0", "0", "0", 0)] // PlacementDescription must be string
        [InlineData("0", "0", "0", "0", "", "notInt")] // PokeDollarPrice must be int
        [InlineData("0", "0", "0", "0", "", 0, "notInt")] // PokeGoldPrice must be int
        [InlineData("0", "0", "0", "0", "", 0, 0, "notInt")] // BigMushroomPrice must be int
        [InlineData("0", "0", "0", "0", "", 0, 0, 0, "notInt")] // HeartScalePrice must be int
        public void ReadRow_WithUnparsableValue_ShouldThrow(params object[] valuesAsArray)
        {
            // Arrange
            var parser = new MoveLearnMethodLocationSheetRowParser();
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
            var parser = new MoveLearnMethodLocationSheetRowParser();

            var moveLearnMethodName = "Move Learn Method Name";
            var tutorType = "Tutor Type";
            var npcName = "NPC Name";
            var locationName = "Location Name";
            var placementDescription = "Placement Description";
            var pokeDollarPrice = string.Empty;
            var pokeGoldPrice = string.Empty;
            var bigMushroomPrice = string.Empty;
            var heartScalePrice = string.Empty;

            var values = new List<object>
            {
                moveLearnMethodName, tutorType, npcName, locationName,
                placementDescription, pokeDollarPrice, pokeGoldPrice,
                bigMushroomPrice, heartScalePrice
            };

            var defaultValue = 0;

            var expected = new MoveLearnMethodLocationSheetDto
            {
                MoveLearnMethodName = moveLearnMethodName,
                TutorType = tutorType,
                NpcName = npcName,
                LocationName = locationName,
                PlacementDescription = placementDescription,
                PokeDollarPrice = defaultValue,
                PokeGoldPrice = defaultValue,
                BigMushroomPrice = defaultValue,
                HeartScalePrice = defaultValue
            };

            // Act
            var result = parser.ReadRow(values);

            // Assert
            result.Should().BeEquivalentTo(expected);
        }
    }
}