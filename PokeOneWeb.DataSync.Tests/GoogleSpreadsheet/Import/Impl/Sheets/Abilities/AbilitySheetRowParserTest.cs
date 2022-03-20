using FluentAssertions;
using PokeOneWeb.DataSync.GoogleSpreadsheet.Import.Impl;
using PokeOneWeb.DataSync.GoogleSpreadsheet.Import.Impl.Sheets.Abilities;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace PokeOneWeb.DataSync.Tests.GoogleSpreadsheet.Import.Impl.Sheets.Abilities
{
    public class AbilitySheetRowParserTest
    {
        [Fact]
        public void ReadRow_WithMinimalValidValues_ShouldParse()
        {
            // Arrange
            var parser = new AbilitySheetRowParser();
            var name = "Ability Name";
            var values = new List<object> { name };

            var expected = new AbilitySheetDto { Name = name };

            // Act
            var result = parser.ReadRow(values);

            // Assert
            result.Should().BeEquivalentTo(expected);
        }

        [Fact]
        public void ReadRow_WithAllValidValues_ShouldParse()
        {
            // Arrange
            var parser = new AbilitySheetRowParser();

            var name = "Ability Name";
            var shortEffect = "Short Effect";
            var effect = "Effect";
            var atkBoost = 1.1M;
            var spaBoost = 1.2M;
            var defBoost = 1.3M;
            var spdBoost = 1.4M;
            var speBoost = 1.5M;
            var hpBoost = 1.6M;
            var boostConditions = "Boost Conditions";

            var values = new List<object>
            {
                name, shortEffect, effect,
                atkBoost, spaBoost, defBoost,
                spdBoost, speBoost, hpBoost,
                boostConditions
            };

            var expected = new AbilitySheetDto
            {
                Name = name,
                ShortEffect = shortEffect,
                Effect = effect,
                AtkBoost = atkBoost,
                SpaBoost = spaBoost,
                DefBoost = defBoost,
                SpdBoost = spdBoost,
                SpeBoost = speBoost,
                HpBoost = hpBoost,
                BoostConditions = boostConditions
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
            var parser = new AbilitySheetRowParser();
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
            var parser = new AbilitySheetRowParser();
            var values = new List<object>()
            {
                "0", "", "", 1, 1, 1, 1, 1, 1, "", "excessive value"
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
            var parser = new AbilitySheetRowParser();
            List<object> values = null;

            // Act
            var actual = () => parser.ReadRow(values);

            // Assert
            actual.Should().Throw<InvalidRowDataException>();
        }

        [Theory]
        [InlineData("")] // Name must be non-empty
        [InlineData("0", 0)] // ShortEffect must be string
        [InlineData("0", "", 0)] // Effect must be string
        [InlineData("0", "", "", "notDecimal")] // AtkBoost must be decimal
        [InlineData("0", "", "", 1, "notDecimal")] // SpaBoost must be decimal
        [InlineData("0", "", "", 1, 1, "notDecimal")] // DefBoost must be decimal
        [InlineData("0", "", "", 1, 1, 1, "notDecimal")] // SpdBoost must be decimal
        [InlineData("0", "", "", 1, 1, 1, 1, "notDecimal")] // SpeBoost must be decimal
        [InlineData("0", "", "", 1, 1, 1, 1, 1, "notDecimal")] // HpBoost must be decimal
        [InlineData("0", "", "", 1, 1, 1, 1, 1, 1, 0)] // BoostConditions must be string
        public void ReadRow_WithUnparsableValue_ShouldThrow(params object[] valuesAsArray)
        {
            // Arrange
            var parser = new AbilitySheetRowParser();
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
            var parser = new AbilitySheetRowParser();

            var name = "Ability Name";
            var shortEffect = "Short Effect";
            var effect = "Effect";
            var atkBoost = "";
            var spaBoost = "";
            var defBoost = "";
            var spdBoost = "";
            var speBoost = "";
            var hpBoost = "";
            var boostConditions = "Boost Conditions";

            var values = new List<object>
            {
                name, shortEffect, effect,
                atkBoost, spaBoost, defBoost,
                spdBoost, speBoost, hpBoost,
                boostConditions
            };

            var defaultValue = 1M;

            var expected = new AbilitySheetDto
            {
                Name = name,
                ShortEffect = shortEffect,
                Effect = effect,
                AtkBoost = defaultValue,
                SpaBoost = defaultValue,
                DefBoost = defaultValue,
                SpdBoost = defaultValue,
                SpeBoost = defaultValue,
                HpBoost = defaultValue,
                BoostConditions = boostConditions
            };

            // Act
            var result = parser.ReadRow(values);

            // Assert
            result.Should().BeEquivalentTo(expected);
        }
    }
}
