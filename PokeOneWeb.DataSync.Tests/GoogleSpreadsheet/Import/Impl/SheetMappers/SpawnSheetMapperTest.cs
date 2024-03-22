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
    public class SpawnSheetMapperTest
    {
        private readonly Mock<ISpreadsheetImportReporter> _reporterMock;
        private readonly SpawnSheetMapper _mapper;

        private readonly RowHash _rowHash = new() { IdHash = "Id Hash", Hash = "Hash" };

        private List<string> _columnNames = new()
        {
            "Location",
            "PokemonForm",
            "Season",
            "TimeOfDay",
            "SpawnType",
            "SpawnCommonality",
            "SpawnProbability",
            "EncounterCount",
            "IsConfirmed",
            "LowestLevel",
            "HighestLevel",
            "Notes"
        };

        private List<object> _values;

        private List<SheetDataRow> Data => new() { new SheetDataRow(_columnNames, _rowHash, _values) };

        public SpawnSheetMapperTest()
        {
            _reporterMock = new Mock<ISpreadsheetImportReporter>();
            _mapper = new SpawnSheetMapper(_reporterMock.Object);
        }

        [Fact]
        public void Map_WithMinimalValidValues_ShouldMap()
        {
            // Arrange
            const string location = "Location Name";
            const string pokemonForm = "Pokemon Form Name";
            const string season = "Any";
            const string timeOfDay = "Any";
            const string spawnType = "Spawn Type";

            _values = new List<object>
            {
                location,
                pokemonForm,
                season,
                timeOfDay,
                spawnType
            };

            var expected = new Spawn
            {
                IdHash = _rowHash.IdHash,
                Hash = _rowHash.Hash,
                LocationName = location,
                PokemonFormName = pokemonForm,
                SpawnTypeName = spawnType,
                SpawnCommonality = string.Empty,
                SpawnProbability = null,
                EncounterCount = null,
                IsConfirmed = true,
                LowestLevel = 0,
                HighestLevel = 0,
                Notes = string.Empty,
                SpawnOpportunities = new List<SpawnOpportunity>
                {
                    new()
                    {
                        SeasonAbbreviation = season,
                        TimeOfDayAbbreviation = timeOfDay
                    }
                }
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
            const string location = "Location Name";
            const string pokemonForm = "Pokemon Form Name";
            const string season = "Any";
            const string timeOfDay = "Any";
            const string spawnType = "Spawn Type";
            const string spawnCommonality = "Common";
            const string spawnProbabilityString = "34.5%";
            var spawnProbability = 0.345M;
            const int encounterCount = 318;
            const bool isConfirmed = false;
            const int lowestLevel = 40;
            const int highestLevel = 50;
            const string notes = "Notes";

            _values = new List<object>
            {
                location,
                pokemonForm,
                season,
                timeOfDay,
                spawnType,
                spawnCommonality,
                spawnProbabilityString,
                encounterCount,
                isConfirmed,
                lowestLevel,
                highestLevel,
                notes
            };

            var expected = new Spawn
            {
                IdHash = _rowHash.IdHash,
                Hash = _rowHash.Hash,
                LocationName = location,
                PokemonFormName = pokemonForm,
                SpawnTypeName = spawnType,
                SpawnCommonality = spawnCommonality,
                SpawnProbability = spawnProbability,
                EncounterCount = encounterCount,
                IsConfirmed = isConfirmed,
                LowestLevel = lowestLevel,
                HighestLevel = highestLevel,
                Notes = notes,
                SpawnOpportunities = new List<SpawnOpportunity>
                {
                    new()
                    {
                        SeasonAbbreviation = season,
                        TimeOfDayAbbreviation = timeOfDay
                    }
                }
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
            const string location1 = "Location Name 1";
            const string pokemonForm1 = "Pokemon Form Name 1";
            const string season1 = "W";
            const string timeOfDay1 = "M";
            const string spawnType1 = "Spawn Type 1";

            const string location2 = "Location Name 2";
            const string pokemonForm2 = "Pokemon Form Name 2";
            const string season2 = "Any";
            const string timeOfDay2 = "Any";
            const string spawnType2 = "Spawn Type 2";

            var values1 = new List<object>
            {
                location1,
                pokemonForm1,
                season1,
                timeOfDay1,
                spawnType1
            };

            var values2 = new List<object>
            {
                location2,
                pokemonForm2,
                season2,
                timeOfDay2,
                spawnType2
            };

            var data = new List<SheetDataRow>
            {
                new(_columnNames, _rowHash, values1),
                new(_columnNames, _rowHash, values2)
            };

            var expected = new List<Spawn>
            {
                new()
                {
                    IdHash = _rowHash.IdHash,
                    Hash = _rowHash.Hash,
                    LocationName = location1,
                    PokemonFormName = pokemonForm1,
                    SpawnTypeName = spawnType1,
                    SpawnCommonality = string.Empty,
                    SpawnProbability = null,
                    EncounterCount = null,
                    IsConfirmed = true,
                    LowestLevel = 0,
                    HighestLevel = 0,
                    Notes = string.Empty,
                    SpawnOpportunities = new List<SpawnOpportunity>
                    {
                        new()
                        {
                            SeasonAbbreviation = season1,
                            TimeOfDayAbbreviation = timeOfDay1
                        }
                    }
                },
                new()
                {
                    IdHash = _rowHash.IdHash,
                    Hash = _rowHash.Hash,
                    LocationName = location2,
                    PokemonFormName = pokemonForm2,
                    SpawnTypeName = spawnType2,
                    SpawnCommonality = string.Empty,
                    SpawnProbability = null,
                    EncounterCount = null,
                    IsConfirmed = true,
                    LowestLevel = 0,
                    HighestLevel = 0,
                    Notes = string.Empty,
                    SpawnOpportunities = new List<SpawnOpportunity>
                    {
                        new()
                        {
                            SeasonAbbreviation = season2,
                            TimeOfDayAbbreviation = timeOfDay2
                        }
                    }
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
                x => x.ReportError(nameof(Spawn), _rowHash.IdHash, It.IsAny<Exception>()),
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
        [InlineData("", "b", "Any", "Any", "c", "", "", 1, false, 1, 1, "")]
        [InlineData("a", "", "Any", "Any", "c", "", "", 1, false, 1, 1, "")]
        [InlineData("a", "b", "", "Any", "c", "", "", 1, false, 1, 1, "")]
        [InlineData("a", "b", "Any", "", "c", "", "", 1, false, 1, 1, "")]
        [InlineData("a", "b", "Any", "Any", "", "", "", 1, false, 1, 1, "")]
        [InlineData("a", "b", "Any", "Any", "c", 1000, "", 1, false, 1, 1, "")]
        [InlineData("a", "b", "Any", "Any", "c", "", "notDecimal%", 1, false, 1, 1, "")]
        [InlineData("a", "b", "Any", "Any", "c", "", "", "notInt", false, 1, 1, "")]
        [InlineData("a", "b", "Any", "Any", "c", "", "", 1, "notBool", 1, 1, "")]
        [InlineData("a", "b", "Any", "Any", "c", "", "", 1, false, "notInt", 1, "")]
        [InlineData("a", "b", "Any", "Any", "c", "", "", 1, false, 1, "notInt", "")]
        [InlineData("a", "b", "Any", "Any", "c", "", "", 1, false, 1, 1, 1000)]
        public void Map_WithUnparsableValue_ShouldReportError(params object[] valuesAsArray)
        {
            // Arrange
            _values = valuesAsArray.ToList();

            // Act
            var actual = _mapper.Map(Data).ToList();

            // Assert
            actual.Count.Should().Be(0);
            _reporterMock.Verify(
                x => x.ReportError(nameof(Spawn), _rowHash.IdHash, It.IsAny<ParseException>()),
                Times.AtLeastOnce);
        }

        [Fact]
        public void Map_WithDifferentColumnOrder_ShouldParse()
        {
            // Arrange
            _columnNames = new()
            {
                "HighestLevel",
                "PokemonForm",
                "TimeOfDay",
                "Location",
                "Season",
                "EncounterCount",
                "SpawnType",
                "Notes",
                "SpawnCommonality",
                "SpawnProbability",
                "IsConfirmed",
                "LowestLevel"
            };

            const string location = "Location Name";
            const string pokemonForm = "Pokemon Form Name";
            const string season = "Any";
            const string timeOfDay = "Any";
            const string spawnType = "Spawn Type";
            const string spawnCommonality = "Common";
            const string spawnProbabilityString = "34.5%";
            var spawnProbability = 0.345M;
            const int encounterCount = 318;
            const bool isConfirmed = false;
            const int lowestLevel = 40;
            const int highestLevel = 50;
            const string notes = "Notes";

            _values = new List<object>
            {
                highestLevel,
                pokemonForm,
                timeOfDay,
                location,
                season,
                encounterCount,
                spawnType,
                notes,
                spawnCommonality,
                spawnProbabilityString,
                isConfirmed,
                lowestLevel
            };

            var expected = new Spawn
            {
                IdHash = _rowHash.IdHash,
                Hash = _rowHash.Hash,
                LocationName = location,
                PokemonFormName = pokemonForm,
                SpawnTypeName = spawnType,
                SpawnCommonality = spawnCommonality,
                SpawnProbability = spawnProbability,
                EncounterCount = encounterCount,
                IsConfirmed = isConfirmed,
                LowestLevel = lowestLevel,
                HighestLevel = highestLevel,
                Notes = notes,
                SpawnOpportunities = new List<SpawnOpportunity>
                {
                    new()
                    {
                        SeasonAbbreviation = season,
                        TimeOfDayAbbreviation = timeOfDay
                    }
                }
            };

            // Act
            var actual = _mapper.Map(Data).ToList();

            // Assert
            actual.Count.Should().Be(1);
            actual[0].Should().BeEquivalentTo(expected);
        }

        public static IEnumerable<object[]> GetValidSpawnOpportunityCases()
        {
            yield return new object[]
            {
                "Any", "Any",
                new List<SpawnOpportunity>
                {
                    new() { SeasonAbbreviation = "Any", TimeOfDayAbbreviation = "Any" }
                }
            };
            yield return new object[]
            {
                "D", "S",
                new List<SpawnOpportunity>
                {
                    new() { SeasonAbbreviation = "D", TimeOfDayAbbreviation = "S" }
                }
            };
            yield return new object[]
            {
                "D", "Any",
                new List<SpawnOpportunity>
                {
                    new() { SeasonAbbreviation = "D", TimeOfDayAbbreviation = "Any" }
                }
            };
            yield return new object[]
            {
                "Any", "S",
                new List<SpawnOpportunity>
                {
                    new() { SeasonAbbreviation = "Any", TimeOfDayAbbreviation = "S" }
                }
            };
            yield return new object[]
            {
                "MDEN", "Any",
                new List<SpawnOpportunity>
                {
                    new() { SeasonAbbreviation = "M", TimeOfDayAbbreviation = "Any" },
                    new() { SeasonAbbreviation = "D", TimeOfDayAbbreviation = "Any" },
                    new() { SeasonAbbreviation = "E", TimeOfDayAbbreviation = "Any" },
                    new() { SeasonAbbreviation = "N", TimeOfDayAbbreviation = "Any" },
                }
            };
            yield return new object[]
            {
                "Any", "PSAW",
                new List<SpawnOpportunity>
                {
                    new() { SeasonAbbreviation = "Any", TimeOfDayAbbreviation = "P" },
                    new() { SeasonAbbreviation = "Any", TimeOfDayAbbreviation = "S" },
                    new() { SeasonAbbreviation = "Any", TimeOfDayAbbreviation = "A" },
                    new() { SeasonAbbreviation = "Any", TimeOfDayAbbreviation = "W" },
                }
            };
            yield return new object[]
            {
                "MDEN", "PSAW",
                new List<SpawnOpportunity>
                {
                    new() { SeasonAbbreviation = "M", TimeOfDayAbbreviation = "P" },
                    new() { SeasonAbbreviation = "D", TimeOfDayAbbreviation = "P" },
                    new() { SeasonAbbreviation = "E", TimeOfDayAbbreviation = "P" },
                    new() { SeasonAbbreviation = "N", TimeOfDayAbbreviation = "P" },
                    new() { SeasonAbbreviation = "M", TimeOfDayAbbreviation = "S" },
                    new() { SeasonAbbreviation = "D", TimeOfDayAbbreviation = "S" },
                    new() { SeasonAbbreviation = "E", TimeOfDayAbbreviation = "S" },
                    new() { SeasonAbbreviation = "N", TimeOfDayAbbreviation = "S" },
                    new() { SeasonAbbreviation = "M", TimeOfDayAbbreviation = "A" },
                    new() { SeasonAbbreviation = "D", TimeOfDayAbbreviation = "A" },
                    new() { SeasonAbbreviation = "E", TimeOfDayAbbreviation = "A" },
                    new() { SeasonAbbreviation = "N", TimeOfDayAbbreviation = "A" },
                    new() { SeasonAbbreviation = "M", TimeOfDayAbbreviation = "W" },
                    new() { SeasonAbbreviation = "D", TimeOfDayAbbreviation = "W" },
                    new() { SeasonAbbreviation = "E", TimeOfDayAbbreviation = "W" },
                    new() { SeasonAbbreviation = "N", TimeOfDayAbbreviation = "W" },
                }
            };
        }

        [Theory]
        [MemberData(nameof(GetValidSpawnOpportunityCases))]
        public void Map_WithValidSpawnOpportunities_ShouldMap(
            string season, string timeOfDay, List<SpawnOpportunity> spawnOpportunities)
        {
            // Arrange
            const string location = "Location Name";
            const string pokemonForm = "Pokemon Form Name";
            const string spawnType = "Spawn Type";

            _values = new List<object>
            {
                location,
                pokemonForm,
                season,
                timeOfDay,
                spawnType
            };

            var expected = new Spawn
            {
                IdHash = _rowHash.IdHash,
                Hash = _rowHash.Hash,
                LocationName = location,
                PokemonFormName = pokemonForm,
                SpawnTypeName = spawnType,
                SpawnCommonality = string.Empty,
                SpawnProbability = null,
                EncounterCount = null,
                IsConfirmed = true,
                LowestLevel = 0,
                HighestLevel = 0,
                Notes = string.Empty,
                SpawnOpportunities = spawnOpportunities
            };

            // Act
            var actual = _mapper.Map(Data).ToList();

            // Assert
            actual.Count.Should().Be(1);
            actual[0].Should().BeEquivalentTo(expected);
        }

        public static IEnumerable<object[]> GetInvalidSpawnOpportunityCases()
        {
            yield return new object[] { string.Empty, string.Empty };
            yield return new object[] { "Any", string.Empty };
            yield return new object[] { string.Empty, "Any" };
        }

        [Theory]
        [MemberData(nameof(GetInvalidSpawnOpportunityCases))]
        public void Map_WithInvalidSpawnOpportunities_ShouldReportError(string season, string timeOfDay)
        {
            // Arrange
            const string location = "Location Name";
            const string pokemonForm = "Pokemon Form Name";
            const string spawnType = "Spawn Type";

            _values = new List<object>
            {
                location,
                pokemonForm,
                season,
                timeOfDay,
                spawnType
            };

            // Act
            var actual = _mapper.Map(Data).ToList();

            // Assert
            actual.Count.Should().Be(0);
            _reporterMock.Verify(
                x => x.ReportError(nameof(Spawn), _rowHash.IdHash, It.IsAny<ParseException>()),
                Times.AtLeastOnce);
        }
    }
}