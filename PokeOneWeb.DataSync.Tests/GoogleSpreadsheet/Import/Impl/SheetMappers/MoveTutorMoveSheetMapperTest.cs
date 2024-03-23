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
    public class MoveTutorMoveSheetMapperTest
    {
        private readonly Mock<ISpreadsheetImportReporter> _reporterMock;
        private readonly MoveTutorMoveSheetMapper _mapper;

        private readonly RowHash _rowHash = new() { IdHash = "Id Hash", Hash = "Hash" };

        private List<string> _columnNames = new()
        {
            "TutorName",
            "Move",
            "RedShardPrice",
            "BlueShardPrice",
            "GreenShardPrice",
            "YellowShardPrice",
            "PWTBPPrice",
            "BFBPPrice",
            "PokeDollarPrice",
            "PokeGoldPrice"
        };

        private List<object> _values;

        private List<SheetDataRow> Data => new() { new SheetDataRow(_columnNames, _rowHash, _values) };

        public MoveTutorMoveSheetMapperTest()
        {
            _reporterMock = new Mock<ISpreadsheetImportReporter>();
            _mapper = new MoveTutorMoveSheetMapper(_reporterMock.Object);
        }

        [Fact]
        public void Map_WithMinimalValidValues_ShouldMap()
        {
            // Arrange
            const string tutorName = "Tutor Name";
            const string move = "Move Name";

            _values = new List<object>
            {
                tutorName,
                move
            };

            var expected = new MoveTutorMove
            {
                IdHash = _rowHash.IdHash,
                Hash = _rowHash.Hash,
                MoveTutorName = tutorName,
                MoveName = move,
                Price = new List<MoveTutorMovePrice>()
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
            const string tutorName = "Tutor Name";
            const string move = "Move Name";
            const int redShardPrice = 1;
            const int blueShardPrice = 2;
            const int greenShardPrice = 3;
            const int yellowShardPrice = 4;
            const int pwtBpPrice = 50;
            const int bfBpPrice = 60;
            const int pokeDollarPrice = 1000;
            const int pokeGoldPrice = 100;

            _values = new List<object>
            {
                tutorName,
                move,
                redShardPrice,
                blueShardPrice,
                greenShardPrice,
                yellowShardPrice,
                pwtBpPrice,
                bfBpPrice,
                pokeDollarPrice,
                pokeGoldPrice
            };

            var expected = new MoveTutorMove
            {
                IdHash = _rowHash.IdHash,
                Hash = _rowHash.Hash,
                MoveTutorName = tutorName,
                MoveName = move,
                Price = new List<MoveTutorMovePrice>
                {
                    new()
                    {
                        CurrencyAmount = new CurrencyAmount { Amount = redShardPrice, CurrencyName = "Red Shard" }
                    },
                    new()
                    {
                        CurrencyAmount = new CurrencyAmount { Amount = blueShardPrice, CurrencyName = "Blue Shard" }
                    },
                    new()
                    {
                        CurrencyAmount = new CurrencyAmount { Amount = greenShardPrice, CurrencyName = "Green Shard" }
                    },
                    new()
                    {
                        CurrencyAmount = new CurrencyAmount { Amount = yellowShardPrice, CurrencyName = "Yellow Shard" }
                    },
                    new()
                    {
                        CurrencyAmount = new CurrencyAmount { Amount = pwtBpPrice, CurrencyName = "Battle Points (PWT)" }
                    },
                    new()
                    {
                        CurrencyAmount = new CurrencyAmount { Amount = bfBpPrice, CurrencyName = "Battle Points (BF)" }
                    },
                    new()
                    {
                        CurrencyAmount = new CurrencyAmount { Amount = pokeDollarPrice, CurrencyName = "Poké Dollar" }
                    },
                    new()
                    {
                        CurrencyAmount = new CurrencyAmount { Amount = pokeGoldPrice, CurrencyName = "Poké Gold" }
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
            const string tutorName1 = "Tutor Name 1";
            const string move1 = "Move Name 1";

            const string tutorName2 = "Tutor Name 2";
            const string move2 = "Move Name 2";

            var values1 = new List<object>
            {
                tutorName1,
                move1
            };

            var values2 = new List<object>
            {
                tutorName2,
                move2
            };

            var data = new List<SheetDataRow>
            {
                new(_columnNames, _rowHash, values1),
                new(_columnNames, _rowHash, values2)
            };

            var expected = new List<MoveTutorMove>
            {
                new()
                {
                    IdHash = _rowHash.IdHash,
                    Hash = _rowHash.Hash,
                    MoveTutorName = tutorName1,
                    MoveName = move1,
                    Price = new List<MoveTutorMovePrice>()
                },
                new()
                {
                    IdHash = _rowHash.IdHash,
                    Hash = _rowHash.Hash,
                    MoveTutorName = tutorName2,
                    MoveName = move2,
                    Price = new List<MoveTutorMovePrice>()
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
                x => x.ReportError(nameof(MoveTutorMove), _rowHash.IdHash, It.IsAny<Exception>()),
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
        [InlineData("", "b", 1, 1, 1, 1, 1, 1, 1, 1)]
        [InlineData("a", "", 1, 1, 1, 1, 1, 1, 1, 1)]
        [InlineData("a", "b", "notInt", 1, 1, 1, 1, 1, 1, 1)]
        [InlineData("a", "b", 1, "notInt", 1, 1, 1, 1, 1, 1)]
        [InlineData("a", "b", 1, 1, "notInt", 1, 1, 1, 1, 1)]
        [InlineData("a", "b", 1, 1, 1, "notInt", 1, 1, 1, 1)]
        [InlineData("a", "b", 1, 1, 1, 1, "notInt", 1, 1, 1)]
        [InlineData("a", "b", 1, 1, 1, 1, 1, "notInt", 1, 1)]
        [InlineData("a", "b", 1, 1, 1, 1, 1, 1, "notInt", 1)]
        [InlineData("a", "b", 1, 1, 1, 1, 1, 1, 1, "notInt")]
        public void Map_WithUnparsableValue_ShouldReportError(params object[] valuesAsArray)
        {
            // Arrange
            _values = valuesAsArray.ToList();

            // Act
            var actual = _mapper.Map(Data).ToList();

            // Assert
            actual.Count.Should().Be(0);
            _reporterMock.Verify(
                x => x.ReportError(nameof(MoveTutorMove), _rowHash.IdHash, It.IsAny<ParseException>()),
                Times.AtLeastOnce);
        }

        [Fact]
        public void Map_WithDifferentColumnOrder_ShouldParse()
        {
            // Arrange
            _columnNames = new()
            {
                "GreenShardPrice",
                "BFBPPrice",
                "Move",
                "BlueShardPrice",
                "TutorName",
                "RedShardPrice",
                "PokeGoldPrice",
                "PokeDollarPrice",
                "YellowShardPrice",
                "PWTBPPrice"
            };

            const string tutorName = "Tutor Name";
            const string move = "Move Name";
            const int redShardPrice = 1;
            const int blueShardPrice = 2;
            const int greenShardPrice = 3;
            const int yellowShardPrice = 4;
            const int pwtBpPrice = 50;
            const int bfBpPrice = 60;
            const int pokeDollarPrice = 1000;
            const int pokeGoldPrice = 100;

            _values = new List<object>
            {
                greenShardPrice,
                bfBpPrice,
                move,
                blueShardPrice,
                tutorName,
                redShardPrice,
                pokeGoldPrice,
                pokeDollarPrice,
                yellowShardPrice,
                pwtBpPrice
            };

            var expected = new MoveTutorMove
            {
                IdHash = _rowHash.IdHash,
                Hash = _rowHash.Hash,
                MoveTutorName = tutorName,
                MoveName = move,
                Price = new List<MoveTutorMovePrice>
                {
                    new()
                    {
                        CurrencyAmount = new CurrencyAmount { Amount = redShardPrice, CurrencyName = "Red Shard" }
                    },
                    new()
                    {
                        CurrencyAmount = new CurrencyAmount { Amount = blueShardPrice, CurrencyName = "Blue Shard" }
                    },
                    new()
                    {
                        CurrencyAmount = new CurrencyAmount { Amount = greenShardPrice, CurrencyName = "Green Shard" }
                    },
                    new()
                    {
                        CurrencyAmount = new CurrencyAmount { Amount = yellowShardPrice, CurrencyName = "Yellow Shard" }
                    },
                    new()
                    {
                        CurrencyAmount = new CurrencyAmount { Amount = pwtBpPrice, CurrencyName = "Battle Points (PWT)" }
                    },
                    new()
                    {
                        CurrencyAmount = new CurrencyAmount { Amount = bfBpPrice, CurrencyName = "Battle Points (BF)" }
                    },
                    new()
                    {
                        CurrencyAmount = new CurrencyAmount { Amount = pokeDollarPrice, CurrencyName = "Poké Dollar" }
                    },
                    new()
                    {
                        CurrencyAmount = new CurrencyAmount { Amount = pokeGoldPrice, CurrencyName = "Poké Gold" }
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