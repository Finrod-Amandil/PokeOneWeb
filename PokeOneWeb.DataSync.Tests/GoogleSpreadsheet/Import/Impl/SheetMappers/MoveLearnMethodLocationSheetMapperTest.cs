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
    public class MoveLearnMethodLocationSheetMapperTest
    {
        private readonly Mock<ISpreadsheetImportReporter> _reporterMock;
        private readonly MoveLearnMethodLocationSheetMapper _mapper;

        private readonly RowHash _rowHash = new() { IdHash = "Id Hash", Hash = "Hash" };

        private List<string> _columnNames = new()
        {
            "MoveLearnMethod",
            "TutorType",
            "NpcName",
            "Location",
            "PlacementDescription",
            "PokeDollarPrice",
            "PokeGoldPrice",
            "BigMushroomPrice",
            "HeartScalePrice"
        };

        private List<object> _values;

        private List<SheetDataRow> Data => new() { new SheetDataRow(_columnNames, _rowHash, _values) };

        public MoveLearnMethodLocationSheetMapperTest()
        {
            _reporterMock = new Mock<ISpreadsheetImportReporter>();
            _mapper = new MoveLearnMethodLocationSheetMapper(_reporterMock.Object);
        }

        [Fact]
        public void Map_WithMinimalValidValues_ShouldMap()
        {
            // Arrange
            const string moveLearnMethod = "Learn Method";
            const string tutorType = "Tutor Type";
            const string npcName = "NPC Name";
            const string location = "Location Name";

            _values = new List<object>
            {
                moveLearnMethod,
                tutorType,
                npcName,
                location
            };

            var expected = new MoveLearnMethodLocation
            {
                IdHash = _rowHash.IdHash,
                Hash = _rowHash.Hash,
                MoveLearnMethod = new MoveLearnMethod
                {
                    Name = moveLearnMethod
                },
                TutorType = tutorType,
                NpcName = npcName,
                LocationName = location,
                PlacementDescription = string.Empty,
                Price = new List<MoveLearnMethodLocationPrice>()
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
            const string moveLearnMethod = "Learn Method";
            const string tutorType = "Tutor Type";
            const string npcName = "NPC Name";
            const string location = "Location Name";
            const string placementDescription = "Placement Description";
            const int pokeDollarPrice = 1000;
            const int pokeGoldPrice = 100;
            const int bigMushroomPrice = 2;
            const int heartScalePrice = 3;

            _values = new List<object>
            {
                moveLearnMethod,
                tutorType,
                npcName,
                location,
                placementDescription,
                pokeDollarPrice,
                pokeGoldPrice,
                bigMushroomPrice,
                heartScalePrice
            };

            var expected = new MoveLearnMethodLocation
            {
                IdHash = _rowHash.IdHash,
                Hash = _rowHash.Hash,
                MoveLearnMethod = new MoveLearnMethod
                {
                    Name = moveLearnMethod
                },
                TutorType = tutorType,
                NpcName = npcName,
                LocationName = location,
                PlacementDescription = placementDescription,
                Price = new List<MoveLearnMethodLocationPrice>
                {
                    new()
                    {
                        CurrencyAmount = new CurrencyAmount
                        {
                            Amount = pokeDollarPrice,
                            CurrencyName = "Poké Dollar"
                        }
                    },
                    new()
                    {
                        CurrencyAmount = new CurrencyAmount
                        {
                            Amount = pokeGoldPrice,
                            CurrencyName = "Poké Gold"
                        }
                    },
                    new()
                    {
                        CurrencyAmount = new CurrencyAmount
                        {
                            Amount = bigMushroomPrice,
                            CurrencyName = "Big Mushroom"
                        }
                    },
                    new()
                    {
                        CurrencyAmount = new CurrencyAmount
                        {
                            Amount = heartScalePrice,
                            CurrencyName = "Heart Scale"
                        }
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
            const string moveLearnMethod1 = "Learn Method 1";
            const string tutorType1 = "Tutor Type 1";
            const string npcName1 = "NPC Name 1";
            const string location1 = "Location Name 1";

            const string moveLearnMethod2 = "Learn Method 2";
            const string tutorType2 = "Tutor Type 2";
            const string npcName2 = "NPC Name 2";
            const string location2 = "Location Name 2";

            var values1 = new List<object>
            {
                moveLearnMethod1,
                tutorType1,
                npcName1,
                location1
            };

            var values2 = new List<object>
            {
                moveLearnMethod2,
                tutorType2,
                npcName2,
                location2
            };

            var data = new List<SheetDataRow>
            {
                new(_columnNames, _rowHash, values1),
                new(_columnNames, _rowHash, values2)
            };

            var expected = new List<MoveLearnMethodLocation>
            {
                new()
                {
                    IdHash = _rowHash.IdHash,
                    Hash = _rowHash.Hash,
                    MoveLearnMethod = new MoveLearnMethod
                    {
                        Name = moveLearnMethod1
                    },
                    TutorType = tutorType1,
                    NpcName = npcName1,
                    LocationName = location1,
                    PlacementDescription = string.Empty,
                    Price = new List<MoveLearnMethodLocationPrice>()
                },
                new()
                {
                    IdHash = _rowHash.IdHash,
                    Hash = _rowHash.Hash,
                    MoveLearnMethod = new MoveLearnMethod
                    {
                        Name = moveLearnMethod2
                    },
                    TutorType = tutorType2,
                    NpcName = npcName2,
                    LocationName = location2,
                    PlacementDescription = string.Empty,
                    Price = new List<MoveLearnMethodLocationPrice>()
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
                x => x.ReportError(nameof(MoveLearnMethodLocation), _rowHash.IdHash, It.IsAny<Exception>()),
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
        [InlineData("", "b", "c", "d", "", 1, 1, 1, 1)]
        [InlineData("a", "", "c", "d", "", 1, 1, 1, 1)]
        [InlineData("a", "b", "", "d", "", 1, 1, 1, 1)]
        [InlineData("a", "b", "c", "", "", 1, 1, 1, 1)]
        [InlineData("a", "b", "c", "d", 1000, 1, 1, 1, 1)]
        [InlineData("a", "b", "c", "d", "", "notInt", 1, 1, 1)]
        [InlineData("a", "b", "c", "d", "", 1, "notInt", 1, 1)]
        [InlineData("a", "b", "c", "d", "", 1, 1, "notInt", 1)]
        [InlineData("a", "b", "c", "d", "", 1, 1, 1, "notInt")]
        public void Map_WithUnparsableValue_ShouldReportError(params object[] valuesAsArray)
        {
            // Arrange
            _values = valuesAsArray.ToList();

            // Act
            var actual = _mapper.Map(Data).ToList();

            // Assert
            actual.Count.Should().Be(0);
            _reporterMock.Verify(
                x => x.ReportError(nameof(MoveLearnMethodLocation), _rowHash.IdHash, It.IsAny<ParseException>()),
                Times.AtLeastOnce);
        }

        [Fact]
        public void Map_WithDifferentColumnOrder_ShouldParse()
        {
            // Arrange
            _columnNames = new List<string>
            {
                "BigMushroomPrice",
                "PlacementDescription",
                "TutorType",
                "MoveLearnMethod",
                "HeartScalePrice",
                "PokeDollarPrice",
                "NpcName",
                "Location",
                "PokeGoldPrice",
            };

            const string moveLearnMethod = "Learn Method";
            const string tutorType = "Tutor Type";
            const string npcName = "NPC Name";
            const string location = "Location Name";
            const string placementDescription = "Placement Description";
            const int pokeDollarPrice = 1000;
            const int pokeGoldPrice = 100;
            const int bigMushroomPrice = 2;
            const int heartScalePrice = 3;

            _values = new List<object>
            {
                bigMushroomPrice,
                placementDescription,
                tutorType,
                moveLearnMethod,
                heartScalePrice,
                pokeDollarPrice,
                npcName,
                location,
                pokeGoldPrice
            };

            var expected = new MoveLearnMethodLocation
            {
                IdHash = _rowHash.IdHash,
                Hash = _rowHash.Hash,
                MoveLearnMethod = new MoveLearnMethod
                {
                    Name = moveLearnMethod
                },
                TutorType = tutorType,
                NpcName = npcName,
                LocationName = location,
                PlacementDescription = placementDescription,
                Price = new List<MoveLearnMethodLocationPrice>
                {
                    new()
                    {
                        CurrencyAmount = new CurrencyAmount
                        {
                            Amount = pokeDollarPrice,
                            CurrencyName = "Poké Dollar"
                        }
                    },
                    new()
                    {
                        CurrencyAmount = new CurrencyAmount
                        {
                            Amount = pokeGoldPrice,
                            CurrencyName = "Poké Gold"
                        }
                    },
                    new()
                    {
                        CurrencyAmount = new CurrencyAmount
                        {
                            Amount = bigMushroomPrice,
                            CurrencyName = "Big Mushroom"
                        }
                    },
                    new()
                    {
                        CurrencyAmount = new CurrencyAmount
                        {
                            Amount = heartScalePrice,
                            CurrencyName = "Heart Scale"
                        }
                    }
                }
            };

            // Act
            var actual = _mapper.Map(Data).ToList();

            // Assert
            actual.Count.Should().Be(1);
            actual[0].Should().BeEquivalentTo(expected);
        }
    }
}