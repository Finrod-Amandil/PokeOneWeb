using FluentAssertions;
using PokeOneWeb.DataSync.GoogleSpreadsheet.Import.Impl;
using PokeOneWeb.DataSync.GoogleSpreadsheet.Import.Impl.Sheets.BagCategories;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace PokeOneWeb.DataSync.Tests.GoogleSpreadsheet.Import.Impl.Sheets.BagCategories
{
    public class BagCategorySheetRowParserTest
    {
        [Fact]
        public void ReadRow_WithMinimalValidValues_ShouldParse()
        {
            // Arrange
            var parser = new BagCategorySheetRowParser();
            var name = "BagCategory Name";
            var sortIndex = 1;
            var values = new List<object> { name, sortIndex };

            var expected = new BagCategorySheetDto
            {
                Name = name,
                SortIndex = sortIndex
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
            var parser = new BagCategorySheetRowParser();
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
            var parser = new BagCategorySheetRowParser();
            var values = new List<object>
            {
                "0", 1, "excessive value"
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
            var parser = new BagCategorySheetRowParser();
            List<object> values = null;

            // Act
            var actual = () => parser.ReadRow(values);

            // Assert
            actual.Should().Throw<InvalidRowDataException>();
        }

        [Theory]
        [InlineData("")] // Name must be non-empty
        [InlineData("0", "")] // SortIndex must be int
        public void ReadRow_WithUnparsableValue_ShouldThrow(params object[] valuesAsArray)
        {
            // Arrange
            var parser = new BagCategorySheetRowParser();
            var values = valuesAsArray.ToList();

            // Act
            var actual = () => parser.ReadRow(values);

            // Assert
            actual.Should().Throw<InvalidRowDataException>();
        }
    }
}
