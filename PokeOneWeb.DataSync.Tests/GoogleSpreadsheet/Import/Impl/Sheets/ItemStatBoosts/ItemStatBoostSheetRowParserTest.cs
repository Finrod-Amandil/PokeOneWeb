using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using PokeOneWeb.DataSync.GoogleSpreadsheet.Import.Impl;
using PokeOneWeb.DataSync.GoogleSpreadsheet.Import.Impl.Sheets.ItemStatBoosts;
using Xunit;

namespace PokeOneWeb.DataSync.Tests.GoogleSpreadsheet.Import.Impl.Sheets.ItemStatBoosts
{
    public class ItemStatBoostSheetRowParserTest
    {
        [Fact]
        public void ReadRow_WithMinimalValidValues_ShouldParse()
        {
            // Arrange
            var parser = new ItemStatBoostSheetRowParser();

            var itemName = "Item Name";
            var atkBoost = 1.1M;
            var spaBoost = 1.2M;
            var defBoost = 1.3M;
            var spdBoost = 1.4M;
            var speBoost = 1.5M;
            var hpBoost = 1.6M;
            var values = new List<object>
            {
                itemName, atkBoost, spaBoost, defBoost, spdBoost, speBoost, hpBoost
            };

            var expected = new ItemStatBoostSheetDto()
            {
                ItemName = itemName,
                AtkBoost = atkBoost,
                SpaBoost = spaBoost,
                DefBoost = defBoost,
                SpdBoost = spdBoost,
                SpeBoost = speBoost,
                HpBoost = hpBoost
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
            var parser = new ItemStatBoostSheetRowParser();

            var itemName = "Item Name";
            var atkBoost = 1.1M;
            var spaBoost = 1.2M;
            var defBoost = 1.3M;
            var spdBoost = 1.4M;
            var speBoost = 1.5M;
            var hpBoost = 1.6M;
            var requiredPokemonName = "Required Pokemon Name";

            var values = new List<object>
            {
                itemName, atkBoost, spaBoost, defBoost, spdBoost, speBoost, hpBoost,
                requiredPokemonName
            };

            var expected = new ItemStatBoostSheetDto()
            {
                ItemName = itemName,
                AtkBoost = atkBoost,
                SpaBoost = spaBoost,
                DefBoost = defBoost,
                SpdBoost = spdBoost,
                SpeBoost = speBoost,
                HpBoost = hpBoost,
                RequiredPokemonName = requiredPokemonName
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
            var parser = new ItemStatBoostSheetRowParser();
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
            var parser = new ItemStatBoostSheetRowParser();
            var values = new List<object>()
            {
                "0", 1M, 1M, 1M, 1M, 1M, 1M, string.Empty, "excessive value"
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
            var parser = new ItemStatBoostSheetRowParser();
            List<object> values = null;

            // Act
            var actual = () => parser.ReadRow(values);

            // Assert
            actual.Should().Throw<InvalidRowDataException>();
        }

        [Theory]
        [InlineData("")] // ItemName must be non-empty
        [InlineData("0", "notDecimal")] // AtkBoost must be decimal
        [InlineData("0", 1, "notDecimal")] // SpaBoost must be decimal
        [InlineData("0", 1, 1, "notDecimal")] // DefBoost must be decimal
        [InlineData("0", 1, 1, 1, "notDecimal")] // SpdBoost must be decimal
        [InlineData("0", 1, 1, 1, 1, "notDecimal")] // SpeBoost must be decimal
        [InlineData("0", 1, 1, 1, 1, 1, "notDecimal")] // HpBoost must be decimal
        [InlineData("0", 1, 1, 1, 1, 1, 1, 0)] // RequiredPokemonName must be string
        public void ReadRow_WithUnparsableValue_ShouldThrow(params object[] valuesAsArray)
        {
            // Arrange
            var parser = new ItemStatBoostSheetRowParser();
            var values = valuesAsArray.ToList();

            // Act
            var actual = () => parser.ReadRow(values);

            // Assert
            actual.Should().Throw<InvalidRowDataException>();
        }
    }
}