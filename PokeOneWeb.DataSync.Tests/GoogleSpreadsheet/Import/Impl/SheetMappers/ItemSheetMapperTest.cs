using System;
using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using Moq;
using PokeOneWeb.Data;
using PokeOneWeb.Data.Entities;
using PokeOneWeb.DataSync.GoogleSpreadsheet.DataTypes;
using PokeOneWeb.DataSync.Import.Interfaces;
using PokeOneWeb.DataSync.Import.SheetMappers;
using PokeOneWeb.Shared.Exceptions;
using Xunit;

namespace PokeOneWeb.DataSync.Tests.GoogleSpreadsheet.Import.Impl.SheetMappers
{
    public class ItemSheetMapperTest
    {
        private readonly Mock<ISpreadsheetImportReporter> _reporterMock;
        private readonly ItemSheetMapper _mapper;

        private readonly RowHash _rowHash = new() { IdHash = "Id Hash", Hash = "Hash", ImportSheetId = 1 };

        private List<string> _columnNames = new()
        {
            "Name",
            "IsAvailable",
            "DoInclude",
            "ResourceName",
            "SortIndex",
            "BagCategory",
            "PokeoneItemId",
            "Description",
            "Effect",
            "SpriteName",
        };

        private List<object> _values;

        private List<SheetDataRow> Data => new() { new SheetDataRow(_columnNames, _rowHash, _values) };

        public ItemSheetMapperTest()
        {
            _reporterMock = new Mock<ISpreadsheetImportReporter>();
            _mapper = new ItemSheetMapper(_reporterMock.Object);
        }

        [Fact]
        public void Map_WithMinimalValidValues_ShouldMap()
        {
            // Arrange
            const string name = "Item Name";
            const bool isAvailable = false;
            const bool doInclude = true;
            const string resourceName = "Resource Name";
            const int sortIndex = 123;
            const string bagCategory = "Bag Category";

            _values = new List<object>
            {
                name,
                isAvailable,
                doInclude,
                resourceName,
                sortIndex,
                bagCategory
            };

            var expected = new Item
            {
                IdHash = _rowHash.IdHash,
                Hash = _rowHash.Hash,
                ImportSheetId = _rowHash.ImportSheetId,
                Name = name,
                IsAvailable = isAvailable,
                DoInclude = doInclude,
                ResourceName = resourceName,
                SortIndex = sortIndex,
                BagCategoryName = bagCategory,
                PokeoneItemId = null,
                Description = string.Empty,
                Effect = string.Empty,
                SpriteName = string.Empty
            };

            // Act
            var actual = _mapper.Map(Data).ToList();

            // Assert
            actual.Count.Should().Be(1);
            actual[0].Should().BeEquivalentTo(expected);
        }

        [Fact]
        public void Map_WithAllValidValues_ShouldMap()
        {
            // Arrange
            const string name = "Item Name";
            const bool isAvailable = false;
            const bool doInclude = true;
            const string resourceName = "Resource Name";
            const int sortIndex = 123;
            const string bagCategory = "Bag Category";
            const int pokeoneItemId = 234;
            const string description = "Item Description";
            const string effect = "Item Effect";
            const string spriteName = "item-sprite.png";

            _values = new List<object>
            {
                name,
                isAvailable,
                doInclude,
                resourceName,
                sortIndex,
                bagCategory,
                pokeoneItemId,
                description,
                effect,
                spriteName
            };

            var expected = new Item
            {
                IdHash = _rowHash.IdHash,
                Hash = _rowHash.Hash,
                ImportSheetId = _rowHash.ImportSheetId,
                Name = name,
                IsAvailable = isAvailable,
                DoInclude = doInclude,
                ResourceName = resourceName,
                SortIndex = sortIndex,
                BagCategoryName = bagCategory,
                PokeoneItemId = pokeoneItemId,
                Description = description,
                Effect = effect,
                SpriteName = spriteName
            };

            // Act
            var actual = _mapper.Map(Data).ToList();

            // Assert
            actual.Count.Should().Be(1);
            actual[0].Should().BeEquivalentTo(expected);
        }

        [Fact]
        public void Map_WithMultipleRows_ShouldMap()
        {
            // Arrange
            const string name1 = "Item Name 1";
            const bool isAvailable1 = false;
            const bool doInclude1 = true;
            const string resourceName1 = "Resource Name 1";
            const int sortIndex1 = 123;
            const string bagCategory1 = "Bag Category 1";

            const string name2 = "Item Name 2";
            const bool isAvailable2 = true;
            const bool doInclude2 = false;
            const string resourceName2 = "Resource Name 2";
            const int sortIndex2 = 234;
            const string bagCategory2 = "Bag Category 2";

            var values1 = new List<object>
            {
                name1,
                isAvailable1,
                doInclude1,
                resourceName1,
                sortIndex1,
                bagCategory1
            };

            var values2 = new List<object>
            {
                name2,
                isAvailable2,
                doInclude2,
                resourceName2,
                sortIndex2,
                bagCategory2
            };

            var data = new List<SheetDataRow>
            {
                new(_columnNames, _rowHash, values1),
                new(_columnNames, _rowHash, values2)
            };

            var expected = new List<Item>
            {
                new()
                {
                    IdHash = _rowHash.IdHash,
                    Hash = _rowHash.Hash,
                    ImportSheetId = _rowHash.ImportSheetId,
                    Name = name1,
                    IsAvailable = isAvailable1,
                    DoInclude = doInclude1,
                    ResourceName = resourceName1,
                    SortIndex = sortIndex1,
                    BagCategoryName = bagCategory1,
                    PokeoneItemId = null,
                    Description = string.Empty,
                    Effect = string.Empty,
                    SpriteName = string.Empty
                },
                new()
                {
                    IdHash = _rowHash.IdHash,
                    Hash = _rowHash.Hash,
                    ImportSheetId = _rowHash.ImportSheetId,
                    Name = name2,
                    IsAvailable = isAvailable2,
                    DoInclude = doInclude2,
                    ResourceName = resourceName2,
                    SortIndex = sortIndex2,
                    BagCategoryName = bagCategory2,
                    PokeoneItemId = null,
                    Description = string.Empty,
                    Effect = string.Empty,
                    SpriteName = string.Empty
                }
            };

            // Act
            var actual = _mapper.Map(data).ToList();

            // Assert
            actual.Count.Should().Be(2);
            actual.Should().BeEquivalentTo(expected);
        }

        [Fact]
        public void Map_WithMissingRequiredValues_ShouldNotParseAndReportErrors()
        {
            // Arrange
            _values = new List<object>();

            // Act
            var actual = _mapper.Map(Data).ToList();

            // Assert
            actual.Count.Should().Be(0);
            _reporterMock.Verify(
                x => x.ReportError(nameof(Item), _rowHash.IdHash, It.IsAny<Exception>()),
                Times.AtLeastOnce);
        }

        [Fact]
        public void Map_WithNull_ShouldThrow()
        {
            // Arrange
            List<SheetDataRow> data = null;

            // Act
            var actual = () => _mapper.Map(data).ToList();

            // Assert
            actual.Should().Throw<ArgumentNullException>();
        }

        [Fact]
        public void Map_WithEmptyList_ShouldReturnEmptyList()
        {
            // Arrange
            var data = new List<SheetDataRow>();

            // Act
            var actual = _mapper.Map(data).ToList();

            // Assert
            actual.Count.Should().Be(0);
        }

        [Theory]
        [InlineData("", false, false, "b", 1, "c", 1, "", "", "")]
        [InlineData("a", "notBool", false, "b", 1, "c", 1, "", "", "")]
        [InlineData("a", false, "notBool", "b", 1, "c", 1, "", "", "")]
        [InlineData("a", false, false, "", 1, "c", 1, "", "", "")]
        [InlineData("a", false, false, "b", "notInt", "c", 1, "", "", "")]
        [InlineData("a", false, false, "b", 1, "", 1, "", "", "")]
        [InlineData("a", false, false, "b", 1, "c", "notInt", "", "", "")]
        [InlineData("a", false, false, "b", 1, "c", 1, 1000, "", "")]
        [InlineData("a", false, false, "b", 1, "c", 1, "", 1000, "")]
        [InlineData("a", false, false, "b", 1, "c", 1, "", "", 1000)]
        public void Map_WithUnparsableValue_ShouldReportError(params object[] valuesAsArray)
        {
            // Arrange
            _values = valuesAsArray.ToList();

            // Act
            var actual = _mapper.Map(Data).ToList();

            // Assert
            actual.Count.Should().Be(0);
            _reporterMock.Verify(
                x => x.ReportError(nameof(Item), _rowHash.IdHash, It.IsAny<ParseException>()),
                Times.AtLeastOnce);
        }

        [Fact]
        public void Map_WithDifferentColumnOrder_ShouldParse()
        {
            // Arrange
            _columnNames = new List<string>
            {
                "PokeoneItemId",
                "BagCategory",
                "IsAvailable",
                "Name",
                "SpriteName",
                "DoInclude",
                "ResourceName",
                "Description",
                "Effect",
                "SortIndex",
            };

            const string name = "Item Name";
            const bool isAvailable = false;
            const bool doInclude = true;
            const string resourceName = "Resource Name";
            const int sortIndex = 123;
            const string bagCategory = "Bag Category";
            const int pokeoneItemId = 234;
            const string description = "Item Description";
            const string effect = "Item Effect";
            const string spriteName = "item-sprite.png";

            _values = new List<object>
            {
                pokeoneItemId,
                bagCategory,
                isAvailable,
                name,
                spriteName,
                doInclude,
                resourceName,
                description,
                effect,
                sortIndex
            };

            var expected = new Item
            {
                IdHash = _rowHash.IdHash,
                Hash = _rowHash.Hash,
                ImportSheetId = _rowHash.ImportSheetId,
                Name = name,
                IsAvailable = isAvailable,
                DoInclude = doInclude,
                ResourceName = resourceName,
                SortIndex = sortIndex,
                BagCategoryName = bagCategory,
                PokeoneItemId = pokeoneItemId,
                Description = description,
                Effect = effect,
                SpriteName = spriteName
            };

            // Act
            var actual = _mapper.Map(Data).ToList();

            // Assert
            actual.Count.Should().Be(1);
            actual[0].Should().BeEquivalentTo(expected);
        }
    }
}