using System;
using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using Moq;
using PokeOneWeb.Data;
using PokeOneWeb.Data.Entities;
using PokeOneWeb.DataSync.GoogleSpreadsheet.DataTypes;
using PokeOneWeb.DataSync.GoogleSpreadsheet.Exceptions;
using PokeOneWeb.DataSync.Import.Interfaces;
using PokeOneWeb.DataSync.Import.SheetMappers;
using PokeOneWeb.Shared.Exceptions;
using Xunit;

namespace PokeOneWeb.DataSync.Tests.GoogleSpreadsheet.Import.Impl.SheetMappers
{
    public class BuildSheetMapperTest
    {
        private readonly Mock<ISpreadsheetImportReporter> _reporterMock;
        private readonly BuildSheetMapper _mapper;

        private readonly RowHash _rowHash = new() { IdHash = "Id Hash", Hash = "Hash" };

        private List<string> _columnNames = new()
        {
            "PokemonVarietyName",
            "Name",
            "Description",
            "Move1Options",
            "Move2Options",
            "Move3Options",
            "Move4Options",
            "ItemOptions",
            "NatureOptions",
            "Ability",
            "EvDistribution"
        };

        private List<object> _values;

        private List<SheetDataRow> Data => new() { new SheetDataRow(_columnNames, _rowHash, _values) };

        public BuildSheetMapperTest()
        {
            _reporterMock = new Mock<ISpreadsheetImportReporter>();
            _mapper = new BuildSheetMapper(_reporterMock.Object);
        }

        [Fact]
        public void Map_WithMinimalValidValues_ShouldMap()
        {
            // Arrange
            const string pokemonVarietyName = "Pokemon Variety Name";
            const string buildName = "Build Name";

            _values = new List<object>
            {
                pokemonVarietyName,
                buildName
            };

            var expected = new Build
            {
                IdHash = _rowHash.IdHash,
                Hash = _rowHash.Hash,
                PokemonVarietyName = pokemonVarietyName,
                Name = buildName,
                Description = string.Empty,
                MoveOptions = new List<MoveOption>(),
                ItemOptions = new List<ItemOption>(),
                NatureOptions = new List<NatureOption>(),
                AbilityName = string.Empty,
                AttackEv = 0,
                SpecialAttackEv = 0,
                DefenseEv = 0,
                SpecialDefenseEv = 0,
                SpeedEv = 0,
                HitPointsEv = 0
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
            const string pokemonVarietyName = "Pokemon Variety Name";
            const string buildName = "Build Name";
            const string description = "Description";
            const string move1Name = "Move 1 Name";
            const string move2Name = "Move 2 Name";
            const string move3Name = "Move 3 Name";
            const string move4Name = "Move 4 Name";
            const string itemName = "Item Name";
            const string natureName = "Nature";
            const string abilityName = "Ability Name";
            const int attackEv = 252;
            const int specialAttackEv = 0;
            const int defenseEv = 0;
            const int specialDefenseEv = 0;
            const int speedEv = 252;
            const int hitPointsEv = 4;
            var evDistribution = $"{attackEv} Atk/{speedEv} Spe/{hitPointsEv} HP";

            _values = new List<object>
            {
                pokemonVarietyName,
                buildName,
                description,
                move1Name,
                move2Name,
                move3Name,
                move4Name,
                itemName,
                natureName,
                abilityName,
                evDistribution
            };

            var expected = new Build
            {
                IdHash = _rowHash.IdHash,
                Hash = _rowHash.Hash,
                PokemonVarietyName = pokemonVarietyName,
                Name = buildName,
                Description = description,
                MoveOptions = new List<MoveOption>
                {
                    new() { Slot = 1, MoveName = move1Name },
                    new() { Slot = 2, MoveName = move2Name },
                    new() { Slot = 3, MoveName = move3Name },
                    new() { Slot = 4, MoveName = move4Name }
                },
                ItemOptions = new List<ItemOption>
                {
                    new() { ItemName = itemName }
                },
                NatureOptions = new List<NatureOption>
                {
                    new() { NatureName = natureName }
                },
                AbilityName = abilityName,
                AttackEv = attackEv,
                SpecialAttackEv = specialAttackEv,
                DefenseEv = defenseEv,
                SpecialDefenseEv = specialDefenseEv,
                SpeedEv = speedEv,
                HitPointsEv = hitPointsEv
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
            const string pokemonVarietyName1 = "Pokemon Variety Name 1";
            const string buildName1 = "Build Name 1";

            const string pokemonVarietyName2 = "Pokemon Variety Name 2";
            const string buildName2 = "Build Name 2";

            List<object> values1 = new()
            {
                pokemonVarietyName1,
                buildName1
            };

            List<object> values2 = new()
            {
                pokemonVarietyName2,
                buildName2
            };

            var data = new List<SheetDataRow>
            {
                new(_columnNames, _rowHash, values1),
                new(_columnNames, _rowHash, values2)
            };

            var expected1 = new Build
            {
                IdHash = _rowHash.IdHash,
                Hash = _rowHash.Hash,
                PokemonVarietyName = pokemonVarietyName1,
                Name = buildName1,
                Description = string.Empty,
                MoveOptions = new List<MoveOption>(),
                ItemOptions = new List<ItemOption>(),
                NatureOptions = new List<NatureOption>(),
                AbilityName = string.Empty,
                AttackEv = 0,
                SpecialAttackEv = 0,
                DefenseEv = 0,
                SpecialDefenseEv = 0,
                SpeedEv = 0,
                HitPointsEv = 0
            };

            var expected2 = new Build
            {
                IdHash = _rowHash.IdHash,
                Hash = _rowHash.Hash,
                PokemonVarietyName = pokemonVarietyName2,
                Name = buildName2,
                Description = string.Empty,
                MoveOptions = new List<MoveOption>(),
                ItemOptions = new List<ItemOption>(),
                NatureOptions = new List<NatureOption>(),
                AbilityName = string.Empty,
                AttackEv = 0,
                SpecialAttackEv = 0,
                DefenseEv = 0,
                SpecialDefenseEv = 0,
                SpeedEv = 0,
                HitPointsEv = 0
            };

            // Act
            var actual = _mapper.Map(data).ToList();

            // Assert
            actual.Count.Should().Be(2);
            actual.Single(x => x.Name.Equals(buildName1)).Should().BeEquivalentTo(expected1);
            actual.Single(x => x.Name.Equals(buildName2)).Should().BeEquivalentTo(expected2);
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
                x => x.ReportError(
                    nameof(Build),
                    _rowHash.IdHash,
                    It.IsAny<Exception>()),
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
        [InlineData("", "b", "", "", "", "", "", "", "", "", "")]
        [InlineData("a", "", "", "", "", "", "", "", "", "", "")]
        [InlineData("a", "b", 0, "", "", "", "", "", "", "", "")]
        [InlineData("a", "b", "", 0, "", "", "", "", "", "", "")]
        [InlineData("a", "b", "", "", 0, "", "", "", "", "", "")]
        [InlineData("a", "b", "", "", "", 0, "", "", "", "", "")]
        [InlineData("a", "b", "", "", "", "", 0, "", "", "", "")]
        [InlineData("a", "b", "", "", "", "", "", 0, "", "", "")]
        [InlineData("a", "b", "", "", "", "", "", "", 0, "", "")]
        [InlineData("a", "b", "", "", "", "", "", "", "", 0, "")]
        [InlineData("a", "b", "", "", "", "", "", "", "", "", 0)]
        public void Map_WithUnparsableValue_ShouldReportError(params object[] valuesAsArray)
        {
            // Arrange
            _values = valuesAsArray.ToList();

            // Act
            var actual = _mapper.Map(Data).ToList();

            // Assert
            actual.Count.Should().Be(0);
            _reporterMock.Verify(
                x => x.ReportError(
                    nameof(Build),
                    _rowHash.IdHash,
                    It.IsAny<ParseException>()),
                Times.AtLeastOnce);
        }

        [Fact]
        public void Map_WithDifferentColumnOrder_ShouldParse()
        {
            // Arrange
            _columnNames = new List<string>
            {
                "EvDistribution",
                "Move4Options",
                "Move1Options",
                "Ability",
                "ItemOptions",
                "Name",
                "Description",
                "Move2Options",
                "PokemonVarietyName",
                "NatureOptions",
                "Move3Options"
            };

            const string pokemonVarietyName = "Pokemon Variety Name";
            const string buildName = "Build Name";
            const string description = "Description";
            const string move1Name = "Move 1 Name";
            const string move2Name = "Move 2 Name";
            const string move3Name = "Move 3 Name";
            const string move4Name = "Move 4 Name";
            const string itemName = "Item Name";
            const string natureName = "Nature";
            const string abilityName = "Ability Name";
            const int attackEv = 252;
            const int specialAttackEv = 0;
            const int defenseEv = 0;
            const int specialDefenseEv = 0;
            const int speedEv = 252;
            const int hitPointsEv = 4;
            var evDistribution = $"{attackEv} Atk/{speedEv} Spe/{hitPointsEv} HP";

            _values = new List<object>
            {
                evDistribution,
                move4Name,
                move1Name,
                abilityName,
                itemName,
                buildName,
                description,
                move2Name,
                pokemonVarietyName,
                natureName,
                move3Name
            };

            var expected = new Build
            {
                IdHash = _rowHash.IdHash,
                Hash = _rowHash.Hash,
                PokemonVarietyName = pokemonVarietyName,
                Name = buildName,
                Description = description,
                MoveOptions = new List<MoveOption>
                {
                    new() { Slot = 1, MoveName = move1Name },
                    new() { Slot = 2, MoveName = move2Name },
                    new() { Slot = 3, MoveName = move3Name },
                    new() { Slot = 4, MoveName = move4Name }
                },
                ItemOptions = new List<ItemOption>
                {
                    new() { ItemName = itemName }
                },
                NatureOptions = new List<NatureOption>
                {
                    new() { NatureName = natureName }
                },
                AbilityName = abilityName,
                AttackEv = attackEv,
                SpecialAttackEv = specialAttackEv,
                DefenseEv = defenseEv,
                SpecialDefenseEv = specialDefenseEv,
                SpeedEv = speedEv,
                HitPointsEv = hitPointsEv
            };

            // Act
            var actual = _mapper.Map(Data).ToList();

            // Assert
            actual.Count.Should().Be(1);
            actual[0].Should().BeEquivalentTo(expected);
        }

        [Fact]
        public void Map_WithMultipleOptions_ShouldMap()
        {
            // Arrange
            const string pokemonVarietyName = "Pokemon Variety Name";
            const string buildName = "Build Name";
            const string description = "Description";

            const string move11Name = "Move 1.1 Name";
            const string move12Name = "Move 1.2 Name";
            const string move13Name = "Move 1.3 Name";
            const string move14Name = "Move 1.4 Name";
            var move1Options = $"   {move11Name} / {move12Name}/{move13Name}     /{move14Name} ";

            const string move21Name = "Move 2.1 Name";
            const string move22Name = "Move 2.2 Name";
            const string move23Name = "Move 2.3 Name";
            const string move24Name = "Move 2.4 Name";
            var move2Options = $"//{move21Name} / {move22Name} /// {move23Name} / {move24Name} /";

            const string move31Name = "Move 3.1 Name";
            const string move32Name = "Move 3.2 Name";
            const string move33Name = "Move 3.3 Name";
            const string move34Name = "Move 3.4 Name";
            var move3Options = $"{move31Name} / {move32Name} / {move33Name} / {move34Name}";

            const string move41Name = "Move 4.1 Name";
            const string move42Name = "Move 4.2 Name";
            const string move43Name = "Move 4.3 Name";
            const string move44Name = "Move 4.4 Name";
            var move4Options = $"{move41Name} / {move42Name} / {move43Name} / {move44Name}";

            const string item1Name = "Item 1 Name";
            const string item2Name = "Item 2 Name";
            const string item3Name = "Item 3 Name";
            const string item4Name = "Item 4 Name";
            var itemOptions = $"  {item1Name} ///{item2Name}/{item3Name}    / {item4Name} //";

            const string nature1Name = "Nature 1 Name";
            const string nature2Name = "Nature 2 Name";
            const string nature3Name = "Nature 3 Name";
            const string nature4Name = "Nature 4 Name";
            var natureOptions = $"  //// {nature1Name}/{nature2Name}/   {nature3Name}//   {nature4Name}    ";

            const string abilityName = "Ability Name";
            const int attackEv = 252;
            const int specialAttackEv = 0;
            const int defenseEv = 0;
            const int specialDefenseEv = 0;
            const int speedEv = 252;
            const int hitPointsEv = 4;
            var evDistribution = $"{attackEv} Atk/{speedEv} Spe/{hitPointsEv} HP";

            _values = new List<object>
            {
                pokemonVarietyName,
                buildName,
                description,
                move1Options,
                move2Options,
                move3Options,
                move4Options,
                itemOptions,
                natureOptions,
                abilityName,
                evDistribution
            };

            var expected = new Build
            {
                IdHash = _rowHash.IdHash,
                Hash = _rowHash.Hash,
                PokemonVarietyName = pokemonVarietyName,
                Name = buildName,
                Description = description,
                MoveOptions = new List<MoveOption>
                {
                    new() { Slot = 1, MoveName = move11Name },
                    new() { Slot = 1, MoveName = move12Name },
                    new() { Slot = 1, MoveName = move13Name },
                    new() { Slot = 1, MoveName = move14Name },
                    new() { Slot = 2, MoveName = move21Name },
                    new() { Slot = 2, MoveName = move22Name },
                    new() { Slot = 2, MoveName = move23Name },
                    new() { Slot = 2, MoveName = move24Name },
                    new() { Slot = 3, MoveName = move31Name },
                    new() { Slot = 3, MoveName = move32Name },
                    new() { Slot = 3, MoveName = move33Name },
                    new() { Slot = 3, MoveName = move34Name },
                    new() { Slot = 4, MoveName = move41Name },
                    new() { Slot = 4, MoveName = move42Name },
                    new() { Slot = 4, MoveName = move43Name },
                    new() { Slot = 4, MoveName = move44Name }
                },
                ItemOptions = new List<ItemOption>
                {
                    new() { ItemName = item1Name },
                    new() { ItemName = item2Name },
                    new() { ItemName = item3Name },
                    new() { ItemName = item4Name }
                },
                NatureOptions = new List<NatureOption>
                {
                    new() { NatureName = nature1Name },
                    new() { NatureName = nature2Name },
                    new() { NatureName = nature3Name },
                    new() { NatureName = nature4Name }
                },
                AbilityName = abilityName,
                AttackEv = attackEv,
                SpecialAttackEv = specialAttackEv,
                DefenseEv = defenseEv,
                SpecialDefenseEv = specialDefenseEv,
                SpeedEv = speedEv,
                HitPointsEv = hitPointsEv
            };

            // Act
            var actual = _mapper.Map(Data).ToList();

            // Assert
            actual.Count.Should().Be(1);
            actual[0].Should().BeEquivalentTo(expected);
        }

        [Theory]
        [InlineData("300 Atk")] // Single stat exceeds max value
        [InlineData("253 Atk")] // Single stat exceeds max value
        [InlineData("200 Atk / 200 Spe / 200 HP")] // Sum exceeds max evs
        [InlineData("252 Atk / 252 Spe / 7 HP")] // Sum exceeds max evs
        [InlineData("100Atk/100Spa/100Def/100Spd/100Spe/100Hp")] // Sum exceeds max evs
        [InlineData("200 Atk / 200 Atk")] // Same stat specified twice
        [InlineData("??? Atk")] // Non-numerical value
        [InlineData("Atk")] // Missing value
        [InlineData("100.5 Atk")] // Non-int value
        [InlineData("-100 Atk")] // Negative value
        public void Map_WithInvalidEvDistributionPatternsShouldReportError(string evDistribution)
        {
            // Arrange
            const string pokemonVarietyName = "Pokemon Variety Name";
            const string buildName = "Build Name";
            const string description = "Description";
            const string move1Name = "Move 1 Name";
            const string move2Name = "Move 2 Name";
            const string move3Name = "Move 3 Name";
            const string move4Name = "Move 4 Name";
            const string itemName = "Item Name";
            const string natureName = "Nature";
            const string abilityName = "Ability Name";

            _values = new List<object>
            {
                pokemonVarietyName,
                buildName,
                description,
                move1Name,
                move2Name,
                move3Name,
                move4Name,
                itemName,
                natureName,
                abilityName,
                evDistribution
            };

            // Act
            var actual = _mapper.Map(Data).ToList();

            // Assert
            actual.Count.Should().Be(0);
            _reporterMock.Verify(
                x => x.ReportError(
                    nameof(Build),
                    _rowHash.IdHash,
                    It.IsAny<InvalidRowDataException>()),
                Times.AtLeastOnce);
        }

        [Theory]
        [InlineData("", 0, 0, 0, 0, 0, 0)]
        [InlineData("252 Atk", 252, 0, 0, 0, 0, 0)]
        [InlineData("252 Spa", 0, 252, 0, 0, 0, 0)]
        [InlineData("252 Def", 0, 0, 252, 0, 0, 0)]
        [InlineData("252 Spd", 0, 0, 0, 252, 0, 0)]
        [InlineData("252 Spe", 0, 0, 0, 0, 252, 0)]
        [InlineData("252 Hp", 0, 0, 0, 0, 0, 252)]
        [InlineData("  252    Atk   ", 252, 0, 0, 0, 0, 0)]
        [InlineData("252Atk", 252, 0, 0, 0, 0, 0)]
        [InlineData("252 atk", 252, 0, 0, 0, 0, 0)]
        [InlineData("252 ATK", 252, 0, 0, 0, 0, 0)]
        [InlineData("0 Atk", 0, 0, 0, 0, 0, 0)]
        [InlineData("252 Atk / 252 Spa", 252, 252, 0, 0, 0, 0)]
        [InlineData("252 Atk / 252 Spa / 6 Def", 252, 252, 6, 0, 0, 0)]
        [InlineData("1 Atk/2 Spa/3 Def/4 Spd/5 Spe/6 Hp", 1, 2, 3, 4, 5, 6)]
        [InlineData("3 Def/6 Hp/1 Atk/5 Spe/2 Spa/4 Spd", 1, 2, 3, 4, 5, 6)]
        [InlineData("/", 0, 0, 0, 0, 0, 0)]
        [InlineData("///", 0, 0, 0, 0, 0, 0)]
        [InlineData("252Atk///", 252, 0, 0, 0, 0, 0)]
        [InlineData("252Atk//252Spa/", 252, 252, 0, 0, 0, 0)]
        public void Map_WithValidEvDistributionPatternsShouldMap(
            string evDistribution,
            int atkEv, int spaEv, int defEv, int spdEv, int speEv, int hpEv)
        {
            // Arrange
            const string pokemonVarietyName = "Pokemon Variety Name";
            const string buildName = "Build Name";
            const string description = "Description";
            const string move1Name = "Move 1 Name";
            const string move2Name = "Move 2 Name";
            const string move3Name = "Move 3 Name";
            const string move4Name = "Move 4 Name";
            const string itemName = "Item Name";
            const string natureName = "Nature";
            const string abilityName = "Ability Name";

            _values = new List<object>
            {
                pokemonVarietyName,
                buildName,
                description,
                move1Name,
                move2Name,
                move3Name,
                move4Name,
                itemName,
                natureName,
                abilityName,
                evDistribution
            };

            var expected = new Build
            {
                IdHash = _rowHash.IdHash,
                Hash = _rowHash.Hash,
                PokemonVarietyName = pokemonVarietyName,
                Name = buildName,
                Description = description,
                MoveOptions = new List<MoveOption>
                {
                    new() { Slot = 1, MoveName = move1Name },
                    new() { Slot = 2, MoveName = move2Name },
                    new() { Slot = 3, MoveName = move3Name },
                    new() { Slot = 4, MoveName = move4Name }
                },
                ItemOptions = new List<ItemOption>
                {
                    new() { ItemName = itemName }
                },
                NatureOptions = new List<NatureOption>
                {
                    new() { NatureName = natureName }
                },
                AbilityName = abilityName,
                AttackEv = atkEv,
                SpecialAttackEv = spaEv,
                DefenseEv = defEv,
                SpecialDefenseEv = spdEv,
                SpeedEv = speEv,
                HitPointsEv = hpEv
            };

            // Act
            var actual = _mapper.Map(Data).ToList();

            // Assert
            actual.Count.Should().Be(1);
            actual[0].Should().BeEquivalentTo(expected);
        }
    }
}