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
    public class NatureSheetMapperTest
    {
        private readonly Mock<ISpreadsheetImportReporter> _reporterMock;
        private readonly NatureSheetMapper _mapper;

        private readonly RowHash _rowHash = new() { IdHash = "Id Hash", Hash = "Hash", ImportSheetId = 1 };

        private List<string> _columnNames = new()
        {
            "Name",
            "Atk",
            "Spa",
            "Def",
            "Spd",
            "Spe"
        };

        private List<object> _values;

        private List<SheetDataRow> Data => new() { new SheetDataRow(_columnNames, _rowHash, _values) };

        public NatureSheetMapperTest()
        {
            _reporterMock = new Mock<ISpreadsheetImportReporter>();
            _mapper = new NatureSheetMapper(_reporterMock.Object);
        }

        [Fact]
        public void Map_WithMinimalValidValues_ShouldMap()
        {
            // Arrange
            const string name = "Nature Name";
            const int atk = 1;
            const int spa = -1;
            const int def = 0;
            const int spd = 1;
            const int spe = -1;

            _values = new List<object>
            {
                name,
                atk,
                spa,
                def,
                spd,
                spe
            };

            var expected = new Nature
            {
                IdHash = _rowHash.IdHash,
                Hash = _rowHash.Hash,
                ImportSheetId = _rowHash.ImportSheetId,
                Name = name,
                Attack = atk,
                SpecialAttack = spa,
                Defense = def,
                SpecialDefense = spd,
                Speed = spe
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
            const string name1 = "Nature Name 1";
            const int atk1 = -1;
            const int spa1 = 0;
            const int def1 = 1;
            const int spd1 = -1;
            const int spe1 = 0;

            const string name2 = "Nature Name 2";
            const int atk2 = 1;
            const int spa2 = -1;
            const int def2 = 0;
            const int spd2 = 1;
            const int spe2 = -1;

            List<object> values1 = new()
            {
                name1,
                atk1,
                spa1,
                def1,
                spd1,
                spe1
            };

            List<object> values2 = new()
            {
                name2,
                atk2,
                spa2,
                def2,
                spd2,
                spe2
            };

            var data = new List<SheetDataRow>
            {
                new(_columnNames, _rowHash, values1),
                new(_columnNames, _rowHash, values2)
            };

            var expected = new List<Nature>
            {
                new()
                {
                    IdHash = _rowHash.IdHash,
                    Hash = _rowHash.Hash,
                    ImportSheetId = _rowHash.ImportSheetId,
                    Name = name1,
                    Attack = atk1,
                    SpecialAttack = spa1,
                    Defense = def1,
                    SpecialDefense = spd1,
                    Speed = spe1
                },
                new()
                {
                    IdHash = _rowHash.IdHash,
                    Hash = _rowHash.Hash,
                    ImportSheetId = _rowHash.ImportSheetId,
                    Name = name2,
                    Attack = atk2,
                    SpecialAttack = spa2,
                    Defense = def2,
                    SpecialDefense = spd2,
                    Speed = spe2
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
                    nameof(Nature),
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
        [InlineData("", 1, 1, 1, 1, 1)]
        [InlineData("a", "notInt", 1, 1, 1, 1)]
        [InlineData("a", 1, "notInt", 1, 1, 1)]
        [InlineData("a", 1, 1, "notInt", 1, 1)]
        [InlineData("a", 1, 1, 1, "notInt", 1)]
        [InlineData("a", 1, 1, 1, 1, "notInt")]
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
                    nameof(Nature),
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
                "Spd",
                "Atk",
                "Name",
                "Spa",
                "Def",
                "Spe"
            };

            const string name = "Nature Name";
            const int atk = 1;
            const int spa = -1;
            const int def = 0;
            const int spd = 1;
            const int spe = -1;

            _values = new List<object>
            {
                spd,
                atk,
                name,
                spa,
                def,
                spe
            };

            var expected = new Nature
            {
                IdHash = _rowHash.IdHash,
                Hash = _rowHash.Hash,
                ImportSheetId = _rowHash.ImportSheetId,
                Name = name,
                Attack = atk,
                SpecialAttack = spa,
                Defense = def,
                SpecialDefense = spd,
                Speed = spe
            };

            // Act
            var actual = _mapper.Map(Data).ToList();

            // Assert
            actual.Count.Should().Be(1);
            actual[0].Should().BeEquivalentTo(expected);
        }
    }
}