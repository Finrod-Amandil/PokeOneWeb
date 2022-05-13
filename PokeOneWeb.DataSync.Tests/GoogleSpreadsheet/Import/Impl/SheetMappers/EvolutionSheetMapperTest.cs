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
    public class EvolutionSheetMapperTest
    {
        private readonly Mock<ISpreadsheetImportReporter> _reporterMock;
        private readonly EvolutionSheetMapper _mapper;

        private readonly RowHash _rowHash = new() { IdHash = "Id Hash", Hash = "Hash", ImportSheetId = 1 };

        private List<string> _columnNames = new()
        {
            "BasePokemonSpecies",
            "BasePokemonVariety",
            "BaseStage",
            "EvolvedPokemonVariety",
            "EvolvedStage",
            "EvolutionTrigger",
            "IsReversible",
            "IsAvailable",
            "DoInclude"
        };

        private List<object> _values;

        private List<SheetDataRow> Data => new() { new SheetDataRow(_columnNames, _rowHash, _values) };

        public EvolutionSheetMapperTest()
        {
            _reporterMock = new Mock<ISpreadsheetImportReporter>();
            _mapper = new EvolutionSheetMapper(_reporterMock.Object);
        }

        [Fact]
        public void Map_WithMinimalValidValues_ShouldMap()
        {
            // Arrange
            const string basePokemonSpecies = "Base Pokemon Species";
            const string basePokemonVariety = "Base Pokemon Variety";
            const int baseStage = 0;
            const string evolvedPokemonVariety = "Evolved Pokemon Variety";
            const int evolvedStage = 1;

            _values = new List<object>
            {
                basePokemonSpecies,
                basePokemonVariety,
                baseStage,
                evolvedPokemonVariety,
                evolvedStage
            };

            var expected = new Evolution
            {
                IdHash = _rowHash.IdHash,
                Hash = _rowHash.Hash,
                ImportSheetId = _rowHash.ImportSheetId,
                BasePokemonSpeciesName = basePokemonSpecies,
                BasePokemonVarietyName = basePokemonVariety,
                BaseStage = baseStage,
                EvolvedPokemonVarietyName = evolvedPokemonVariety,
                EvolvedStage = evolvedStage,
                EvolutionTrigger = string.Empty,
                IsReversible = false,
                IsAvailable = true,
                DoInclude = true
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
            const string basePokemonSpecies = "Base Pokemon Species";
            const string basePokemonVariety = "Base Pokemon Variety";
            const int baseStage = 0;
            const string evolvedPokemonVariety = "Evolved Pokemon Variety";
            const int evolvedStage = 1;
            const string evolutionTrigger = "Evolution Trigger";
            const bool isReversible = true;
            const bool isAvailable = false;
            const bool doInclude = false;

            _values = new List<object>
            {
                basePokemonSpecies,
                basePokemonVariety,
                baseStage,
                evolvedPokemonVariety,
                evolvedStage,
                evolutionTrigger,
                isReversible,
                isAvailable,
                doInclude
            };

            var expected = new Evolution
            {
                IdHash = _rowHash.IdHash,
                Hash = _rowHash.Hash,
                ImportSheetId = _rowHash.ImportSheetId,
                BasePokemonSpeciesName = basePokemonSpecies,
                BasePokemonVarietyName = basePokemonVariety,
                BaseStage = baseStage,
                EvolvedPokemonVarietyName = evolvedPokemonVariety,
                EvolvedStage = evolvedStage,
                EvolutionTrigger = evolutionTrigger,
                IsReversible = isReversible,
                IsAvailable = isAvailable,
                DoInclude = doInclude
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
            const string basePokemonSpecies1 = "Base Pokemon Species 1";
            const string basePokemonVariety1 = "Base Pokemon Variety 1";
            const int baseStage1 = 0;
            const string evolvedPokemonVariety1 = "Evolved Pokemon Variety 1";
            const int evolvedStage1 = 1;

            const string basePokemonSpecies2 = "Base Pokemon Species 2";
            const string basePokemonVariety2 = "Base Pokemon Variety 2";
            const int baseStage2 = 2;
            const string evolvedPokemonVariety2 = "Evolved Pokemon Variety 2";
            const int evolvedStage2 = 3;

            var values1 = new List<object>
            {
                basePokemonSpecies1,
                basePokemonVariety1,
                baseStage1,
                evolvedPokemonVariety1,
                evolvedStage1
            };

            var values2 = new List<object>
            {
                basePokemonSpecies2,
                basePokemonVariety2,
                baseStage2,
                evolvedPokemonVariety2,
                evolvedStage2
            };

            var data = new List<SheetDataRow>
            {
                new(_columnNames, _rowHash, values1),
                new(_columnNames, _rowHash, values2)
            };

            var expected = new List<Evolution>
            {
                new()
                {
                    IdHash = _rowHash.IdHash,
                    Hash = _rowHash.Hash,
                    ImportSheetId = _rowHash.ImportSheetId,
                    BasePokemonSpeciesName = basePokemonSpecies1,
                    BasePokemonVarietyName = basePokemonVariety1,
                    BaseStage = baseStage1,
                    EvolvedPokemonVarietyName = evolvedPokemonVariety1,
                    EvolvedStage = evolvedStage1,
                    EvolutionTrigger = string.Empty,
                    IsReversible = false,
                    IsAvailable = true,
                    DoInclude = true
                },
                new()
                {
                    IdHash = _rowHash.IdHash,
                    Hash = _rowHash.Hash,
                    ImportSheetId = _rowHash.ImportSheetId,
                    BasePokemonSpeciesName = basePokemonSpecies2,
                    BasePokemonVarietyName = basePokemonVariety2,
                    BaseStage = baseStage2,
                    EvolvedPokemonVarietyName = evolvedPokemonVariety2,
                    EvolvedStage = evolvedStage2,
                    EvolutionTrigger = string.Empty,
                    IsReversible = false,
                    IsAvailable = true,
                    DoInclude = true
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
                x => x.ReportError(nameof(Evolution), _rowHash.IdHash, It.IsAny<Exception>()),
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
        [InlineData("", "b", 1, "c", 1, "", false, false, false)]
        [InlineData("a", "", 1, "c", 1, "", false, false, false)]
        [InlineData("a", "b", "notInt", "c", 1, "", false, false, false)]
        [InlineData("a", "b", 1, "", 1, "", false, false, false)]
        [InlineData("a", "b", 1, "c", "notInt", "", false, false, false)]
        [InlineData("a", "b", 1, "c", 1, 1000, false, false, false)]
        [InlineData("a", "b", 1, "c", 1, "", "notBool", false, false)]
        [InlineData("a", "b", 1, "c", 1, "", false, "notBool", false)]
        [InlineData("a", "b", 1, "c", 1, "", false, false, "notBool")]
        public void Map_WithUnparsableValue_ShouldReportError(params object[] valuesAsArray)
        {
            // Arrange
            _values = valuesAsArray.ToList();

            // Act
            var actual = _mapper.Map(Data).ToList();

            // Assert
            actual.Count.Should().Be(0);
            _reporterMock.Verify(
                x => x.ReportError(nameof(Evolution), _rowHash.IdHash, It.IsAny<ParseException>()),
                Times.AtLeastOnce);
        }

        [Fact]
        public void Map_WithDifferentColumnOrder_ShouldParse()
        {
            // Arrange
            _columnNames = new List<string>
            {
                "IsReversible",
                "EvolvedStage",
                "BaseStage",
                "DoInclude",
                "BasePokemonVariety",
                "EvolutionTrigger",
                "BasePokemonSpecies",
                "EvolvedPokemonVariety",
                "IsAvailable",
            };

            const string basePokemonSpecies = "Base Pokemon Species";
            const string basePokemonVariety = "Base Pokemon Variety";
            const int baseStage = 0;
            const string evolvedPokemonVariety = "Evolved Pokemon Variety";
            const int evolvedStage = 1;
            const string evolutionTrigger = "Evolution Trigger";
            const bool isReversible = true;
            const bool isAvailable = false;
            const bool doInclude = false;

            _values = new List<object>
            {
                isReversible,
                evolvedStage,
                baseStage,
                doInclude,
                basePokemonVariety,
                evolutionTrigger,
                basePokemonSpecies,
                evolvedPokemonVariety,
                isAvailable
            };

            var expected = new Evolution
            {
                IdHash = _rowHash.IdHash,
                Hash = _rowHash.Hash,
                ImportSheetId = _rowHash.ImportSheetId,
                BasePokemonSpeciesName = basePokemonSpecies,
                BasePokemonVarietyName = basePokemonVariety,
                BaseStage = baseStage,
                EvolvedPokemonVarietyName = evolvedPokemonVariety,
                EvolvedStage = evolvedStage,
                EvolutionTrigger = evolutionTrigger,
                IsReversible = isReversible,
                IsAvailable = isAvailable,
                DoInclude = doInclude
            };

            // Act
            var actual = _mapper.Map(Data).ToList();

            // Assert
            actual.Count.Should().Be(1);
            actual[0].Should().BeEquivalentTo(expected);
        }
    }
}