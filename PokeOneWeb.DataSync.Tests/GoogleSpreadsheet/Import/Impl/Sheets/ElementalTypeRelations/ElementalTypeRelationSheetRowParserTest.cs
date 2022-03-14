using FluentAssertions;
using PokeOneWeb.DataSync.GoogleSpreadsheet.Import.Impl;
using PokeOneWeb.DataSync.GoogleSpreadsheet.Import.Impl.Sheets.ElementalTypeRelations;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace PokeOneWeb.DataSync.Tests.GoogleSpreadsheet.Import.Impl.Sheets.ElementalTypeRelations
{
    public class ElementalTypeRelationSheetRowParserTest
    {
        [Fact]
        public void ReadRow_WithMinimalValidValues_ShouldParse()
        {
            // Arrange
            var parser = new ElementalTypeRelationSheetRowParser();
            var attackingTypeName = "Attacking Type Name";
            var defendingTypeName = "Defending Type Name";
            var effectivity = 1M;
            var values = new List<object>
            {
                attackingTypeName, defendingTypeName, effectivity
            };

            var expected = new ElementalTypeRelationSheetDto
            {
                AttackingTypeName = attackingTypeName,
                DefendingTypeName = defendingTypeName,
                Effectivity = effectivity
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
            var parser = new ElementalTypeRelationSheetRowParser();
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
            var parser = new ElementalTypeRelationSheetRowParser();
            var values = new List<object>
            {
                "0", 1, 1, "excessive value"
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
            var parser = new ElementalTypeRelationSheetRowParser();
            List<object> values = null;

            // Act
            var actual = () => parser.ReadRow(values);

            // Assert
            actual.Should().Throw<InvalidRowDataException>();
        }

        [Theory]
        [InlineData("")] // Attacking Type Name must be non-empty
        [InlineData("0", "")] // Defending Type Name must be non-empty
        [InlineData("0", "0", "")] // Effectivity must be int
        public void ReadRow_WithUnparsableValue_ShouldThrow(params object[] valuesAsArray)
        {
            // Arrange
            var parser = new ElementalTypeRelationSheetRowParser();
            var values = valuesAsArray.ToList();

            // Act
            var actual = () => parser.ReadRow(values);

            // Assert
            actual.Should().Throw<InvalidRowDataException>();
        }
    }
}
