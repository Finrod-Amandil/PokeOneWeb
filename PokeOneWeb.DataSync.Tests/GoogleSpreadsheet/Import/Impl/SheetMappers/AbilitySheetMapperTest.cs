using System;
using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using Moq;
using PokeOneWeb.Data;
using PokeOneWeb.Data.Entities;
using PokeOneWeb.DataSync.GoogleSpreadsheet.DataTypes;
using PokeOneWeb.DataSync.GoogleSpreadsheet.Import;
using PokeOneWeb.DataSync.GoogleSpreadsheet.Import.Impl.SheetMappers;
using PokeOneWeb.Shared.Exceptions;
using Xunit;

namespace PokeOneWeb.DataSync.Tests.GoogleSpreadsheet.Import.Impl.SheetMappers
{
    public class AbilitySheetMapperTest
    {
        private readonly Mock<ISpreadsheetImportReporter> _reporterMock;
        private readonly AbilitySheetMapper _mapper;

        private readonly RowHash _rowHash = new() { IdHash = "Id Hash", Hash = "Hash", ImportSheetId = 1 };
        private List<string> _columnNames = new() { "Name", "ShortEffect", "Effect", "Atk", "Spa", "Def", "Spd", "Spe", "Hp", "Condition" };
        private List<object> _values;

        private List<SheetDataRow> Data => new() { new SheetDataRow(_columnNames, _rowHash, _values) };

        public AbilitySheetMapperTest()
        {
            _reporterMock = new Mock<ISpreadsheetImportReporter>();
            _mapper = new AbilitySheetMapper(_reporterMock.Object);
        }

        [Fact]
        public void Map_WithMinimalValidValues_ShouldMap()
        {
            // Arrange
            const string name = "Ability Name";
            const string shortEffect = "Short Effect Description";
            const string effect = "Effect Description";

            _values = new List<object>
            {
                name,
                shortEffect,
                effect
            };

            var expected = new Ability
            {
                IdHash = _rowHash.IdHash,
                Hash = _rowHash.Hash,
                ImportSheetId = _rowHash.ImportSheetId,
                Name = name,
                EffectShortDescription = shortEffect,
                EffectDescription = effect,
                AttackBoost = 1M,
                SpecialAttackBoost = 1M,
                DefenseBoost = 1M,
                SpecialDefenseBoost = 1M,
                SpeedBoost = 1M,
                HitPointsBoost = 1M,
                BoostConditions = string.Empty
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
            const string name = "Ability Name";
            const string shortEffect = "Short Effect Description";
            const string effect = "Effect Description";
            const decimal atkBoost = 1.1M;
            const decimal spaBoost = 1.2M;
            const decimal defBoost = 1.3M;
            const decimal spdBoost = 1.4M;
            const decimal speBoost = 1.5M;
            const decimal hpBoost = 1.6M;
            const string boostConditions = "Boost Conditions";

            _values = new List<object>
            {
                name,
                shortEffect,
                effect,
                atkBoost,
                spaBoost,
                defBoost,
                spdBoost,
                speBoost,
                hpBoost,
                boostConditions
            };

            var expected = new Ability
            {
                IdHash = _rowHash.IdHash,
                Hash = _rowHash.Hash,
                ImportSheetId = _rowHash.ImportSheetId,
                Name = name,
                EffectShortDescription = shortEffect,
                EffectDescription = effect,
                AttackBoost = atkBoost,
                SpecialAttackBoost = spaBoost,
                DefenseBoost = defBoost,
                SpecialDefenseBoost = spdBoost,
                SpeedBoost = speBoost,
                HitPointsBoost = hpBoost,
                BoostConditions = boostConditions
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
            const string name1 = "Ability Name 1";
            const string shortEffect1 = "Short Effect Description 1";
            const string effect1 = "Effect Description 1";

            const string name2 = "Ability Name 2";
            const string shortEffect2 = "Short Effect Description 2";
            const string effect2 = "Effect Description 2";

            List<object> values1 = new()
            {
                name1,
                shortEffect1,
                effect1
            };

            List<object> values2 = new()
            {
                name2,
                shortEffect2,
                effect2
            };

            var data = new List<SheetDataRow>
            {
                new(_columnNames, _rowHash, values1),
                new(_columnNames, _rowHash, values2)
            };

            var expected1 = new Ability
            {
                IdHash = _rowHash.IdHash,
                Hash = _rowHash.Hash,
                ImportSheetId = _rowHash.ImportSheetId,
                Name = name1,
                EffectShortDescription = shortEffect1,
                EffectDescription = effect1,
                AttackBoost = 1M,
                SpecialAttackBoost = 1M,
                DefenseBoost = 1M,
                SpecialDefenseBoost = 1M,
                SpeedBoost = 1M,
                HitPointsBoost = 1M,
                BoostConditions = string.Empty
            };

            var expected2 = new Ability
            {
                IdHash = _rowHash.IdHash,
                Hash = _rowHash.Hash,
                ImportSheetId = _rowHash.ImportSheetId,
                Name = name2,
                EffectShortDescription = shortEffect2,
                EffectDescription = effect2,
                AttackBoost = 1M,
                SpecialAttackBoost = 1M,
                DefenseBoost = 1M,
                SpecialDefenseBoost = 1M,
                SpeedBoost = 1M,
                HitPointsBoost = 1M,
                BoostConditions = string.Empty
            };

            // Act
            var actual = _mapper.Map(data).ToList();

            // Assert
            actual.Count.Should().Be(2);
            actual.Single(x => x.Name.Equals(name1)).Should().BeEquivalentTo(expected1);
            actual.Single(x => x.Name.Equals(name2)).Should().BeEquivalentTo(expected2);
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
                x => x.ReportError(nameof(Ability), _rowHash.IdHash, It.IsAny<Exception>()),
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
        [InlineData("", "b", "c", 1, 1, 1, 1, 1, 1, "")]
        [InlineData("a", "", "c", 1, 1, 1, 1, 1, 1, "")]
        [InlineData("a", "b", "", 1, 1, 1, 1, 1, 1, "")]
        [InlineData("a", "b", "c", "notDecimal", 1, 1, 1, 1, 1, "")]
        [InlineData("a", "b", "c", 1, "notDecimal", 1, 1, 1, 1, "")]
        [InlineData("a", "b", "c", 1, 1, "notDecimal", 1, 1, 1, "")]
        [InlineData("a", "b", "c", 1, 1, 1, "notDecimal", 1, 1, "")]
        [InlineData("a", "b", "c", 1, 1, 1, 1, "notDecimal", 1, "")]
        [InlineData("a", "b", "c", 1, 1, 1, 1, 1, "notDecimal", "")]
        [InlineData("a", "b", "c", 1, 1, 1, 1, 1, 1, 1000)]
        public void Map_WithUnparsableValue_ShouldReportError(params object[] valuesAsArray)
        {
            // Arrange
            _values = valuesAsArray.ToList();

            // Act
            var actual = _mapper.Map(Data).ToList();

            // Assert
            actual.Count.Should().Be(0);
            _reporterMock.Verify(
                x => x.ReportError(nameof(Ability), _rowHash.IdHash, It.IsAny<ParseException>()),
                Times.AtLeastOnce);
        }

        [Fact]
        public void Map_WithDifferentColumnOrder_ShouldParse()
        {
            // Arrange
            _columnNames = new List<string>
            {
                "Def",
                "Effect",
                "Spa",
                "Name",
                "ShortEffect",
                "Atk",
                "Condition",
                "Spd",
                "Spe",
                "Hp"
            };

            const string name = "Ability Name";
            const string shortEffect = "Short Effect Description";
            const string effect = "Effect Description";
            const decimal atkBoost = 1.1M;
            const decimal spaBoost = 1.2M;
            const decimal defBoost = 1.3M;
            const decimal spdBoost = 1.4M;
            const decimal speBoost = 1.5M;
            const decimal hpBoost = 1.6M;
            const string boostConditions = "Boost Conditions";

            _values = new List<object>
            {
                defBoost,
                effect,
                spaBoost,
                name,
                shortEffect,
                atkBoost,
                boostConditions,
                spdBoost,
                speBoost,
                hpBoost
            };

            var expected = new Ability
            {
                IdHash = _rowHash.IdHash,
                Hash = _rowHash.Hash,
                ImportSheetId = _rowHash.ImportSheetId,
                Name = name,
                EffectShortDescription = shortEffect,
                EffectDescription = effect,
                AttackBoost = atkBoost,
                SpecialAttackBoost = spaBoost,
                DefenseBoost = defBoost,
                SpecialDefenseBoost = spdBoost,
                SpeedBoost = speBoost,
                HitPointsBoost = hpBoost,
                BoostConditions = boostConditions
            };

            // Act
            var actual = _mapper.Map(Data).ToList();

            // Assert
            actual.Count.Should().Be(1);
            actual[0].Should().BeEquivalentTo(expected);
        }
    }
}