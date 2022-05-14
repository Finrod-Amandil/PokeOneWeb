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
    public class HuntingConfigurationSheetMapperTest
    {
        private readonly Mock<ISpreadsheetImportReporter> _reporterMock;
        private readonly HuntingConfigurationSheetMapper _mapper;

        private readonly RowHash _rowHash = new() { IdHash = "Id Hash", Hash = "Hash", ImportSheetId = 1 };
        private List<string> _columnNames = new() { "PokemonVariety", "Nature", "Ability" };
        private List<object> _values;

        private List<SheetDataRow> Data => new() { new SheetDataRow(_columnNames, _rowHash, _values) };

        public HuntingConfigurationSheetMapperTest()
        {
            _reporterMock = new Mock<ISpreadsheetImportReporter>();
            _mapper = new HuntingConfigurationSheetMapper(_reporterMock.Object);
        }

        [Fact]
        public void Map_WithMinimalValidValues_ShouldMap()
        {
            // Arrange
            const string pokemonVariety = "Pokemon Variety";
            const string nature = "Nature";
            const string ability = "Ability";

            _values = new List<object>
            {
                pokemonVariety,
                nature,
                ability
            };

            var expected = new HuntingConfiguration
            {
                IdHash = _rowHash.IdHash,
                Hash = _rowHash.Hash,
                ImportSheetId = _rowHash.ImportSheetId,
                PokemonVarietyName = pokemonVariety,
                NatureName = nature,
                AbilityName = ability
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
            const string pokemonVariety1 = "Pokemon Variety 1";
            const string nature1 = "Nature 1";
            const string ability1 = "Ability 1";

            const string pokemonVariety2 = "Pokemon Variety 2";
            const string nature2 = "Nature 2";
            const string ability2 = "Ability 2";

            List<object> values1 = new()
            {
                pokemonVariety1,
                nature1,
                ability1
            };

            List<object> values2 = new()
            {
                pokemonVariety2,
                nature2,
                ability2
            };

            var data = new List<SheetDataRow>
            {
                new(_columnNames, _rowHash, values1),
                new(_columnNames, _rowHash, values2)
            };

            var expected = new List<HuntingConfiguration>
            {
                new()
                {
                    IdHash = _rowHash.IdHash,
                    Hash = _rowHash.Hash,
                    ImportSheetId = _rowHash.ImportSheetId,
                    PokemonVarietyName = pokemonVariety1,
                    NatureName = nature1,
                    AbilityName = ability1
                },
                new()
                {
                    IdHash = _rowHash.IdHash,
                    Hash = _rowHash.Hash,
                    ImportSheetId = _rowHash.ImportSheetId,
                    PokemonVarietyName = pokemonVariety2,
                    NatureName = nature2,
                    AbilityName = ability2
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
                x => x.ReportError(
                    nameof(HuntingConfiguration),
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
        [InlineData("", "b", "c")]
        [InlineData("a", "", "c")]
        [InlineData("a", "b", "")]
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
                    nameof(HuntingConfiguration),
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
                "Nature",
                "Ability",
                "PokemonVariety"
            };

            const string pokemonVariety = "Pokemon Variety";
            const string nature = "Nature";
            const string ability = "Ability";

            _values = new List<object>
            {
                nature,
                ability,
                pokemonVariety
            };

            var expected = new HuntingConfiguration
            {
                IdHash = _rowHash.IdHash,
                Hash = _rowHash.Hash,
                ImportSheetId = _rowHash.ImportSheetId,
                PokemonVarietyName = pokemonVariety,
                NatureName = nature,
                AbilityName = ability
            };

            // Act
            var actual = _mapper.Map(Data).ToList();

            // Assert
            actual.Count.Should().Be(1);
            actual[0].Should().BeEquivalentTo(expected);
        }
    }
}