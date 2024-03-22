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
    public class MoveSheetMapperTest
    {
        private readonly Mock<ISpreadsheetImportReporter> _reporterMock;
        private readonly MoveSheetMapper _mapper;

        private readonly RowHash _rowHash = new() { IdHash = "Id Hash", Hash = "Hash" };

        private List<string> _columnNames = new()
        {
            "Name",
            "DoInclude",
            "ResourceName",
            "DamageClass",
            "Type",
            "Power",
            "Accuracy",
            "PowerPoints",
            "Priority",
            "Effect"
        };

        private List<object> _values;

        private List<SheetDataRow> Data => new() { new SheetDataRow(_columnNames, _rowHash, _values) };

        public MoveSheetMapperTest()
        {
            _reporterMock = new Mock<ISpreadsheetImportReporter>();
            _mapper = new MoveSheetMapper(_reporterMock.Object);
        }

        [Fact]
        public void Map_WithMinimalValidValues_ShouldMap()
        {
            // Arrange
            const string name = "Move Name";
            const bool doInclude = false;
            const string resourceName = "Resource Name";
            const string damageClass = "Damage Class";
            const string type = "Type";
            const int power = 100;
            const int accuracy = 90;
            const int powerPoints = 30;

            _values = new List<object>
            {
                name,
                doInclude,
                resourceName,
                damageClass,
                type,
                power,
                accuracy,
                powerPoints
            };

            var expected = new Move
            {
                IdHash = _rowHash.IdHash,
                Hash = _rowHash.Hash,
                Name = name,
                DoInclude = doInclude,
                ResourceName = resourceName,
                DamageClassName = damageClass,
                ElementalTypeName = type,
                AttackPower = power,
                Accuracy = accuracy,
                PowerPoints = powerPoints,
                Priority = 0,
                Effect = string.Empty
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
            const string name = "Move Name";
            const bool doInclude = false;
            const string resourceName = "Resource Name";
            const string damageClass = "Damage Class";
            const string type = "Type";
            const int power = 100;
            const int accuracy = 90;
            const int powerPoints = 30;
            const int priority = -3;
            const string effect = "Move Effect";

            _values = new List<object>
            {
                name,
                doInclude,
                resourceName,
                damageClass,
                type,
                power,
                accuracy,
                powerPoints,
                priority,
                effect
            };

            var expected = new Move
            {
                IdHash = _rowHash.IdHash,
                Hash = _rowHash.Hash,
                Name = name,
                DoInclude = doInclude,
                ResourceName = resourceName,
                DamageClassName = damageClass,
                ElementalTypeName = type,
                AttackPower = power,
                Accuracy = accuracy,
                PowerPoints = powerPoints,
                Priority = priority,
                Effect = effect
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
            const string name1 = "Move Name 1";
            const bool doInclude1 = false;
            const string resourceName1 = "Resource Name 1";
            const string damageClass1 = "Damage Class 1";
            const string type1 = "Type 1";
            const int power1 = 100;
            const int accuracy1 = 90;
            const int powerPoints1 = 30;

            const string name2 = "Move Name 2";
            const bool doInclude2 = true;
            const string resourceName2 = "Resource Name 2";
            const string damageClass2 = "Damage Class 2";
            const string type2 = "Type 2";
            const int power2 = 0;
            const int accuracy2 = 75;
            const int powerPoints2 = 15;

            var values1 = new List<object>
            {
                name1,
                doInclude1,
                resourceName1,
                damageClass1,
                type1,
                power1,
                accuracy1,
                powerPoints1
            };

            var values2 = new List<object>
            {
                name2,
                doInclude2,
                resourceName2,
                damageClass2,
                type2,
                power2,
                accuracy2,
                powerPoints2
            };

            var data = new List<SheetDataRow>
            {
                new(_columnNames, _rowHash, values1),
                new(_columnNames, _rowHash, values2)
            };

            var expected = new List<Move>
            {
                new()
                {
                    IdHash = _rowHash.IdHash,
                    Hash = _rowHash.Hash,
                    Name = name1,
                    DoInclude = doInclude1,
                    ResourceName = resourceName1,
                    DamageClassName = damageClass1,
                    ElementalTypeName = type1,
                    AttackPower = power1,
                    Accuracy = accuracy1,
                    PowerPoints = powerPoints1,
                    Priority = 0,
                    Effect = string.Empty
                },
                new()
                {
                    IdHash = _rowHash.IdHash,
                    Hash = _rowHash.Hash,
                    Name = name2,
                    DoInclude = doInclude2,
                    ResourceName = resourceName2,
                    DamageClassName = damageClass2,
                    ElementalTypeName = type2,
                    AttackPower = power2,
                    Accuracy = accuracy2,
                    PowerPoints = powerPoints2,
                    Priority = 0,
                    Effect = string.Empty
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
                x => x.ReportError(nameof(Move), _rowHash.IdHash, It.IsAny<Exception>()),
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
        [InlineData("", false, "b", "c", "d", 1, 1, 1, 1, "")]
        [InlineData("a", "notBool", "b", "c", "d", 1, 1, 1, 1, "")]
        [InlineData("a", false, "", "c", "d", 1, 1, 1, 1, "")]
        [InlineData("a", false, "b", "", "d", 1, 1, 1, 1, "")]
        [InlineData("a", false, "b", "c", "", 1, 1, 1, 1, "")]
        [InlineData("a", false, "b", "c", "d", "notInt", 1, 1, 1, "")]
        [InlineData("a", false, "b", "c", "d", 1, "notInt", 1, 1, "")]
        [InlineData("a", false, "b", "c", "d", 1, 1, "notInt", 1, "")]
        [InlineData("a", false, "b", "c", "d", 1, 1, 1, "notInt", "")]
        [InlineData("a", false, "b", "c", "d", 1, 1, 1, 1, 1000)]
        public void Map_WithUnparsableValue_ShouldReportError(params object[] valuesAsArray)
        {
            // Arrange
            _values = valuesAsArray.ToList();

            // Act
            var actual = _mapper.Map(Data).ToList();

            // Assert
            actual.Count.Should().Be(0);
            _reporterMock.Verify(
                x => x.ReportError(nameof(Move), _rowHash.IdHash, It.IsAny<ParseException>()),
                Times.AtLeastOnce);
        }

        [Fact]
        public void Map_WithDifferentColumnOrder_ShouldParse()
        {
            // Arrange
            _columnNames = new List<string>
            {
                "Accuracy",
                "Power",
                "Effect",
                "ResourceName",
                "Name",
                "Priority",
                "DoInclude",
                "DamageClass",
                "Type",
                "PowerPoints"
            };

            const string name = "Move Name";
            const bool doInclude = false;
            const string resourceName = "Resource Name";
            const string damageClass = "Damage Class";
            const string type = "Type";
            const int power = 100;
            const int accuracy = 90;
            const int powerPoints = 30;
            const int priority = -3;
            const string effect = "Move Effect";

            _values = new List<object>
            {
                accuracy,
                power,
                effect,
                resourceName,
                name,
                priority,
                doInclude,
                damageClass,
                type,
                powerPoints
            };

            var expected = new Move
            {
                IdHash = _rowHash.IdHash,
                Hash = _rowHash.Hash,
                Name = name,
                DoInclude = doInclude,
                ResourceName = resourceName,
                DamageClassName = damageClass,
                ElementalTypeName = type,
                AttackPower = power,
                Accuracy = accuracy,
                PowerPoints = powerPoints,
                Priority = priority,
                Effect = effect
            };

            // Act
            var actual = _mapper.Map(Data).ToList();

            // Assert
            actual.Count.Should().Be(1);
            actual[0].Should().BeEquivalentTo(expected);
        }
    }
}