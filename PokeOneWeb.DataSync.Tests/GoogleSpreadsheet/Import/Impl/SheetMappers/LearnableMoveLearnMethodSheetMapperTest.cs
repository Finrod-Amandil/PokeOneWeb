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
    public class LearnableMoveLearnMethodSheetMapperTest
    {
        private readonly Mock<ISpreadsheetImportReporter> _reporterMock;
        private readonly LearnableMoveLearnMethodSheetMapper _mapper;

        private readonly RowHash _rowHash = new() { IdHash = "Id Hash", Hash = "Hash", ImportSheetId = 1 };

        private List<string> _columnNames = new()
        {
            "PokemonVariety",
            "Move",
            "LearnMethod",
            "IsAvailable",
            "LevelLearnedAt",
            "RequiredItem",
            "TutorName",
            "Comments"
        };

        private List<object> _values;

        private List<SheetDataRow> Data => new() { new SheetDataRow(_columnNames, _rowHash, _values) };

        public LearnableMoveLearnMethodSheetMapperTest()
        {
            _reporterMock = new Mock<ISpreadsheetImportReporter>();
            _mapper = new LearnableMoveLearnMethodSheetMapper(_reporterMock.Object);
        }

        [Fact]
        public void Map_WithMinimalValidValues_ShouldMap()
        {
            // Arrange
            const string pokemonVariety = "Pokemon Variety";
            const string move = "Move Name";
            const string learnMethod = "Learn Method";
            const bool isAvailable = true;

            _values = new List<object>
            {
                pokemonVariety,
                move,
                learnMethod,
                isAvailable
            };

            var expected = new LearnableMoveLearnMethod
            {
                IdHash = _rowHash.IdHash,
                Hash = _rowHash.Hash,
                ImportSheetId = _rowHash.ImportSheetId,
                LearnableMove = new LearnableMove
                {
                    PokemonVarietyName = pokemonVariety,
                    MoveName = move
                },
                MoveLearnMethod = new MoveLearnMethod
                {
                    Name = learnMethod
                },
                IsAvailable = isAvailable,
                LevelLearnedAt = null,
                RequiredItemName = string.Empty,
                MoveTutorName = string.Empty,
                Comments = string.Empty
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
            const string pokemonVariety = "Pokemon Variety";
            const string move = "Move Name";
            const string learnMethod = "Learn Method";
            const bool isAvailable = true;
            const int levelLearnedAt = 10;
            const string requiredItemName = "TM01 - Item Name";
            const string tutorName = "Tutor Name";
            const string comments = "Comments";

            _values = new List<object>
            {
                pokemonVariety,
                move,
                learnMethod,
                isAvailable,
                levelLearnedAt,
                requiredItemName,
                tutorName,
                comments
            };

            var expected = new LearnableMoveLearnMethod
            {
                IdHash = _rowHash.IdHash,
                Hash = _rowHash.Hash,
                ImportSheetId = _rowHash.ImportSheetId,
                LearnableMove = new LearnableMove
                {
                    PokemonVarietyName = pokemonVariety,
                    MoveName = move
                },
                MoveLearnMethod = new MoveLearnMethod
                {
                    Name = learnMethod
                },
                IsAvailable = isAvailable,
                LevelLearnedAt = levelLearnedAt,
                RequiredItemName = requiredItemName,
                MoveTutorName = tutorName,
                Comments = comments
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
            const string move1 = "Move Name 1";
            const string learnMethod1 = "Learn Method 1";
            const bool isAvailable1 = true;

            const string pokemonVariety2 = "Pokemon Variety 2";
            const string move2 = "Move Name 2";
            const string learnMethod2 = "Learn Method 2";
            const bool isAvailable2 = false;

            var values1 = new List<object>
            {
                pokemonVariety1,
                move1,
                learnMethod1,
                isAvailable1
            };

            var values2 = new List<object>
            {
                pokemonVariety2,
                move2,
                learnMethod2,
                isAvailable2
            };

            var data = new List<SheetDataRow>
            {
                new(_columnNames, _rowHash, values1),
                new(_columnNames, _rowHash, values2)
            };

            var expected = new List<LearnableMoveLearnMethod>
            {
                new()
                {
                    IdHash = _rowHash.IdHash,
                    Hash = _rowHash.Hash,
                    ImportSheetId = _rowHash.ImportSheetId,
                    LearnableMove = new LearnableMove
                    {
                        PokemonVarietyName = pokemonVariety1,
                        MoveName = move1
                    },
                    MoveLearnMethod = new MoveLearnMethod
                    {
                        Name = learnMethod1
                    },
                    IsAvailable = isAvailable1,
                    LevelLearnedAt = null,
                    RequiredItemName = string.Empty,
                    MoveTutorName = string.Empty,
                    Comments = string.Empty
                },
                new()
                {
                    IdHash = _rowHash.IdHash,
                    Hash = _rowHash.Hash,
                    ImportSheetId = _rowHash.ImportSheetId,
                    LearnableMove = new LearnableMove
                    {
                        PokemonVarietyName = pokemonVariety2,
                        MoveName = move2
                    },
                    MoveLearnMethod = new MoveLearnMethod
                    {
                        Name = learnMethod2
                    },
                    IsAvailable = isAvailable2,
                    LevelLearnedAt = null,
                    RequiredItemName = string.Empty,
                    MoveTutorName = string.Empty,
                    Comments = string.Empty
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
                x => x.ReportError(nameof(LearnableMoveLearnMethod), _rowHash.IdHash, It.IsAny<Exception>()),
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
        [InlineData("", "b", "c", false, 1, "", "", "")]
        [InlineData("a", "", "c", false, 1, "", "", "")]
        [InlineData("a", "b", "", false, 1, "", "", "")]
        [InlineData("a", "b", "c", "notBool", 1, "", "", "")]
        [InlineData("a", "b", "c", false, "notInt", "", "", "")]
        [InlineData("a", "b", "c", false, 1, 1000, "", "")]
        [InlineData("a", "b", "c", false, 1, "", 1000, "")]
        [InlineData("a", "b", "c", false, 1, "", "", 1000)]
        public void Map_WithUnparsableValue_ShouldReportError(params object[] valuesAsArray)
        {
            // Arrange
            _values = valuesAsArray.ToList();

            // Act
            var actual = _mapper.Map(Data).ToList();

            // Assert
            actual.Count.Should().Be(0);
            _reporterMock.Verify(
                x => x.ReportError(nameof(LearnableMoveLearnMethod), _rowHash.IdHash, It.IsAny<ParseException>()),
                Times.AtLeastOnce);
        }

        [Fact]
        public void Map_WithDifferentColumnOrder_ShouldParse()
        {
            // Arrange
            _columnNames = new List<string>
            {
                "Comments",
                "LevelLearnedAt",
                "Move",
                "PokemonVariety",
                "TutorName",
                "LearnMethod",
                "IsAvailable",
                "RequiredItem",
            };

            const string pokemonVariety = "Pokemon Variety";
            const string move = "Move Name";
            const string learnMethod = "Learn Method";
            const bool isAvailable = true;
            const int levelLearnedAt = 10;
            const string requiredItemName = "TM01 - Item Name";
            const string tutorName = "Tutor Name";
            const string comments = "Comments";

            _values = new List<object>
            {
                comments,
                levelLearnedAt,
                move,
                pokemonVariety,
                tutorName,
                learnMethod,
                isAvailable,
                requiredItemName
            };

            var expected = new LearnableMoveLearnMethod
            {
                IdHash = _rowHash.IdHash,
                Hash = _rowHash.Hash,
                ImportSheetId = _rowHash.ImportSheetId,
                LearnableMove = new LearnableMove
                {
                    PokemonVarietyName = pokemonVariety,
                    MoveName = move
                },
                MoveLearnMethod = new MoveLearnMethod
                {
                    Name = learnMethod
                },
                IsAvailable = isAvailable,
                LevelLearnedAt = levelLearnedAt,
                RequiredItemName = requiredItemName,
                MoveTutorName = tutorName,
                Comments = comments
            };

            // Act
            var actual = _mapper.Map(Data).ToList();

            // Assert
            actual.Count.Should().Be(1);
            actual[0].Should().BeEquivalentTo(expected);
        }
    }
}