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
    public class ElementalTypeRelationSheetMapperTest
    {
        private readonly Mock<ISpreadsheetImportReporter> _reporterMock;
        private readonly ElementalTypeRelationSheetMapper _mapper;

        private readonly RowHash _rowHash = new() { IdHash = "Id Hash", Hash = "Hash", ImportSheetId = 1 };
        private List<string> _columnNames = new() { "AttackingType", "DefendingType", "Effectivity" };
        private List<object> _values;

        private List<SheetDataRow> Data => new() { new SheetDataRow(_columnNames, _rowHash, _values) };

        public ElementalTypeRelationSheetMapperTest()
        {
            _reporterMock = new Mock<ISpreadsheetImportReporter>();
            _mapper = new ElementalTypeRelationSheetMapper(_reporterMock.Object);
        }

        [Fact]
        public void Map_WithMinimalValidValues_ShouldMap()
        {
            // Arrange
            const string attackingTypeName = "Attacking Type Name";
            const string defendingTypeName = "Defending Type Name";
            const decimal effectivity = 2M;

            _values = new List<object>
            {
                attackingTypeName,
                defendingTypeName,
                effectivity
            };

            var expected = new ElementalTypeRelation
            {
                IdHash = _rowHash.IdHash,
                Hash = _rowHash.Hash,
                ImportSheetId = _rowHash.ImportSheetId,
                AttackingTypeName = attackingTypeName,
                DefendingTypeName = defendingTypeName,
                AttackEffectivity = effectivity
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
            const string attackingTypeName1 = "Attacking Type Name 1";
            const string defendingTypeName1 = "Defending Type Name 1";
            const decimal effectivity1 = 2M;

            const string attackingTypeName2 = "Attacking Type Name 2";
            const string defendingTypeName2 = "Defending Type Name 2";
            const decimal effectivity2 = 0.5M;

            List<object> values1 = new()
            {
                attackingTypeName1,
                defendingTypeName1,
                effectivity1
            };

            List<object> values2 = new()
            {
                attackingTypeName2,
                defendingTypeName2,
                effectivity2
            };

            var data = new List<SheetDataRow>
            {
                new(_columnNames, _rowHash, values1),
                new(_columnNames, _rowHash, values2)
            };

            var expected = new List<ElementalTypeRelation>
            {
                new()
                {
                    IdHash = _rowHash.IdHash,
                    Hash = _rowHash.Hash,
                    ImportSheetId = _rowHash.ImportSheetId,
                    AttackingTypeName = attackingTypeName1,
                    DefendingTypeName = defendingTypeName1,
                    AttackEffectivity = effectivity1
                },
                new()
                {
                    IdHash = _rowHash.IdHash,
                    Hash = _rowHash.Hash,
                    ImportSheetId = _rowHash.ImportSheetId,
                    AttackingTypeName = attackingTypeName2,
                    DefendingTypeName = defendingTypeName2,
                    AttackEffectivity = effectivity2
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
                    nameof(ElementalTypeRelation),
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
        [InlineData("", "b", 1)]
        [InlineData("a", "", 1)]
        [InlineData("a", "b", "notDecimal")]
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
                    nameof(ElementalTypeRelation),
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
                "DefendingType",
                "Effectivity",
                "AttackingType"
            };

            const string attackingTypeName = "Attacking Type Name";
            const string defendingTypeName = "Defending Type Name";
            const decimal effectivity = 2M;

            _values = new List<object>
            {
                defendingTypeName,
                effectivity,
                attackingTypeName
            };

            var expected = new ElementalTypeRelation()
            {
                IdHash = _rowHash.IdHash,
                Hash = _rowHash.Hash,
                ImportSheetId = _rowHash.ImportSheetId,
                AttackingTypeName = attackingTypeName,
                DefendingTypeName = defendingTypeName,
                AttackEffectivity = effectivity
            };

            // Act
            var actual = _mapper.Map(Data).ToList();

            // Assert
            actual.Count.Should().Be(1);
            actual[0].Should().BeEquivalentTo(expected);
        }
    }
}