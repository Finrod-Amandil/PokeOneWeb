using FluentAssertions;
using PokeOneWeb.DataSync.GoogleSpreadsheet.Import.Impl;
using PokeOneWeb.DataSync.GoogleSpreadsheet.Import.Impl.Sheets.Items;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace PokeOneWeb.DataSync.Tests.GoogleSpreadsheet.Import.Impl.Sheets.Items
{
    public class ItemSheetRowParserTest
    {
        [Fact]
        public void ReadRow_WithMinimalValidValues_ShouldParse()
        {
            // Arrange
            var parser = new ItemSheetRowParser();

            var name = "Item Name";
            var isAvailable = false;
            var doInclude = true;
            var resourceName = "item-resource-name";
            var sortIndex = 1;
            var bagCategoryName = "Bag Category Name";

            var values = new List<object>
            {
                name,
                isAvailable,
                doInclude,
                resourceName,
                sortIndex,
                bagCategoryName
            };

            var expected = new ItemSheetDto
            {
                Name = name,
                IsAvailable = isAvailable,
                DoInclude = doInclude,
                ResourceName = resourceName,
                SortIndex = sortIndex,
                BagCategoryName = bagCategoryName
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
            var parser = new ItemSheetRowParser();

            var name = "Item Name";
            var isAvailable = false;
            var doInclude = true;
            var resourceName = "item-resource-name";
            var sortIndex = 1;
            var bagCategoryName = "Bag Category Name";
            var pokeOneItemId = 1;
            var description = "Item Description";
            var effect = "Item Effect";
            var spriteName = "item-sprite-name.png";

            var values = new List<object>
            {
                name,
                isAvailable,
                doInclude,
                resourceName,
                sortIndex,
                bagCategoryName,
                pokeOneItemId,
                description,
                effect,
                spriteName
            };

            var expected = new ItemSheetDto
            {
                Name = name,
                IsAvailable = isAvailable,
                DoInclude = doInclude,
                ResourceName = resourceName,
                SortIndex = sortIndex,
                BagCategoryName = bagCategoryName,
                PokeOneItemId = pokeOneItemId,
                Description = description,
                Effect = effect,
                SpriteName = spriteName
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
            var parser = new ItemSheetRowParser();
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
            var parser = new ItemSheetRowParser();
            var values = new List<object>
            {
                "0", false, false, "0", 1, "0", 1, "", "", "", "excessive value"
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
            var parser = new ItemSheetRowParser();
            List<object> values = null;

            // Act
            var actual = () => parser.ReadRow(values);

            // Assert
            actual.Should().Throw<InvalidRowDataException>();
        }

        [Theory]
        [InlineData("")] // Name must be non-empty
        [InlineData("0", "")] // IsAvailable must be boolean
        [InlineData("0", false, "")] // DoInclude must be boolean
        [InlineData("0", false, false, "")] // ResourceName must be non-empty
        [InlineData("0", false, false, "0", "")] // SortIndex must be int
        [InlineData("0", false, false, "0", 1, "")] // BagCategoryName must be non-empty
        [InlineData("0", false, false, "0", 1, "0", "")] // PokeOneItemId must be int
        [InlineData("0", false, false, "0", 1, "0", 1, 0)] // Description must be string
        [InlineData("0", false, false, "0", 1, "0", 1, "", 0)] // Effect must be string
        [InlineData("0", false, false, "0", 1, "0", 1, "", "", 0)] // SpriteName must be string
        public void ReadRow_WithUnparsableValue_ShouldThrow(params object[] valuesAsArray)
        {
            // Arrange
            var parser = new ItemSheetRowParser();
            var values = valuesAsArray.ToList();

            // Act
            var actual = () => parser.ReadRow(values);

            // Assert
            actual.Should().Throw<InvalidRowDataException>();
        }
    }
}
