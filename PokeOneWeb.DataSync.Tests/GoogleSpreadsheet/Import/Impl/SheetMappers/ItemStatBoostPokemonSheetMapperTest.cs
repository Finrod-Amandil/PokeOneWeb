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
    public class ItemStatBoostPokemonSheetMapperTest
    {
        private readonly Mock<ISpreadsheetImportReporter> _reporterMock;
        private readonly ItemStatBoostPokemonSheetMapper _mapper;

        private readonly RowHash _rowHash = new() { IdHash = "Id Hash", Hash = "Hash", ImportSheetId = 1 };

        private List<string> _columnNames = new()
        {
            "Item",
            "AtkBoost",
            "SpaBoost",
            "DefBoost",
            "SpdBoost",
            "SpeBoost",
            "HpBoost",
            "RequiredPokemon"
        };

        private List<object> _values;

        private List<SheetDataRow> Data => new() { new SheetDataRow(_columnNames, _rowHash, _values) };

        public ItemStatBoostPokemonSheetMapperTest()
        {
            _reporterMock = new Mock<ISpreadsheetImportReporter>();
            _mapper = new ItemStatBoostPokemonSheetMapper(_reporterMock.Object);
        }

        [Fact]
        public void Map_WithMinimalValidValues_ShouldMap()
        {
            // Arrange
            const string item = "Item Name";

            _values = new List<object>
            {
                item
            };

            var expected = new ItemStatBoostPokemon
            {
                IdHash = _rowHash.IdHash,
                Hash = _rowHash.Hash,
                ImportSheetId = _rowHash.ImportSheetId,
                ItemStatBoost = new ItemStatBoost
                {
                    ItemName = item,
                    AttackBoost = 1M,
                    SpecialAttackBoost = 1M,
                    DefenseBoost = 1M,
                    SpecialDefenseBoost = 1M,
                    SpeedBoost = 1M,
                    HitPointsBoost = 1M
                },
                PokemonVarietyName = string.Empty
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
            const string item = "Item Name";
            const decimal atkBoost = 1.1M;
            const decimal spaBoost = 1.2M;
            const decimal defBoost = 1.3M;
            const decimal spdBoost = 1.4M;
            const decimal speBoost = 1.5M;
            const decimal hpBoost = 1.6M;
            const string requiredPokemon = "Pokemon Name";

            _values = new List<object>
            {
                item,
                atkBoost,
                spaBoost,
                defBoost,
                spdBoost,
                speBoost,
                hpBoost,
                requiredPokemon
            };

            var expected = new ItemStatBoostPokemon
            {
                IdHash = _rowHash.IdHash,
                Hash = _rowHash.Hash,
                ImportSheetId = _rowHash.ImportSheetId,
                ItemStatBoost = new ItemStatBoost
                {
                    ItemName = item,
                    AttackBoost = atkBoost,
                    SpecialAttackBoost = spaBoost,
                    DefenseBoost = defBoost,
                    SpecialDefenseBoost = spdBoost,
                    SpeedBoost = speBoost,
                    HitPointsBoost = hpBoost
                },
                PokemonVarietyName = requiredPokemon
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
            const string item1 = "Item Name 1";

            const string item2 = "Item Name 2";

            var values1 = new List<object>
            {
                item1
            };

            var values2 = new List<object>
            {
                item2
            };

            var data = new List<SheetDataRow>
            {
                new(_columnNames, _rowHash, values1),
                new(_columnNames, _rowHash, values2)
            };

            var expected = new List<ItemStatBoostPokemon>
            {
                new()
                {
                    IdHash = _rowHash.IdHash,
                    Hash = _rowHash.Hash,
                    ImportSheetId = _rowHash.ImportSheetId,
                    ItemStatBoost = new ItemStatBoost
                    {
                        ItemName = item1,
                        AttackBoost = 1M,
                        SpecialAttackBoost = 1M,
                        DefenseBoost = 1M,
                        SpecialDefenseBoost = 1M,
                        SpeedBoost = 1M,
                        HitPointsBoost = 1M
                    },
                    PokemonVarietyName = string.Empty
                },
                new()
                {
                    IdHash = _rowHash.IdHash,
                    Hash = _rowHash.Hash,
                    ImportSheetId = _rowHash.ImportSheetId,
                    ItemStatBoost = new ItemStatBoost
                    {
                        ItemName = item2,
                        AttackBoost = 1M,
                        SpecialAttackBoost = 1M,
                        DefenseBoost = 1M,
                        SpecialDefenseBoost = 1M,
                        SpeedBoost = 1M,
                        HitPointsBoost = 1M
                    },
                    PokemonVarietyName = string.Empty
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
                x => x.ReportError(nameof(ItemStatBoostPokemon), _rowHash.IdHash, It.IsAny<Exception>()),
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
        [InlineData("", 1, 1, 1, 1, 1, 1, "")]
        [InlineData("a", "notInt", 1, 1, 1, 1, 1, "")]
        [InlineData("a", 1, "notInt", 1, 1, 1, 1, "")]
        [InlineData("a", 1, 1, "notInt", 1, 1, 1, "")]
        [InlineData("a", 1, 1, 1, "notInt", 1, 1, "")]
        [InlineData("a", 1, 1, 1, 1, "notInt", 1, "")]
        [InlineData("a", 1, 1, 1, 1, 1, "notInt", "")]
        [InlineData("a", 1, 1, 1, 1, 1, 1, 1000)]
        public void Map_WithUnparsableValue_ShouldReportError(params object[] valuesAsArray)
        {
            // Arrange
            _values = valuesAsArray.ToList();

            // Act
            var actual = _mapper.Map(Data).ToList();

            // Assert
            actual.Count.Should().Be(0);
            _reporterMock.Verify(
                x => x.ReportError(nameof(ItemStatBoostPokemon), _rowHash.IdHash, It.IsAny<ParseException>()),
                Times.AtLeastOnce);
        }

        [Fact]
        public void Map_WithDifferentColumnOrder_ShouldParse()
        {
            // Arrange
            _columnNames = new List<string>
            {
                "SpdBoost",
                "SpaBoost",
                "SpeBoost",
                "Item",
                "AtkBoost",
                "RequiredPokemon",
                "DefBoost",
                "HpBoost"
            };

            const string item = "Item Name";
            const decimal atkBoost = 1.1M;
            const decimal spaBoost = 1.2M;
            const decimal defBoost = 1.3M;
            const decimal spdBoost = 1.4M;
            const decimal speBoost = 1.5M;
            const decimal hpBoost = 1.6M;
            const string requiredPokemon = "Pokemon Name";

            _values = new List<object>
            {
                spdBoost,
                spaBoost,
                speBoost,
                item,
                atkBoost,
                requiredPokemon,
                defBoost,
                hpBoost
            };

            var expected = new ItemStatBoostPokemon
            {
                IdHash = _rowHash.IdHash,
                Hash = _rowHash.Hash,
                ImportSheetId = _rowHash.ImportSheetId,
                ItemStatBoost = new ItemStatBoost
                {
                    ItemName = item,
                    AttackBoost = atkBoost,
                    SpecialAttackBoost = spaBoost,
                    DefenseBoost = defBoost,
                    SpecialDefenseBoost = spdBoost,
                    SpeedBoost = speBoost,
                    HitPointsBoost = hpBoost
                },
                PokemonVarietyName = requiredPokemon
            };

            // Act
            var actual = _mapper.Map(Data).ToList();

            // Assert
            actual.Count.Should().Be(1);
            actual[0].Should().BeEquivalentTo(expected);
        }
    }
}