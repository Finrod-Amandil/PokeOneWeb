using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using PokeOneWeb.DataSync.GoogleSpreadsheet.Exceptions;
using PokeOneWeb.DataSync.GoogleSpreadsheet.Import.Impl.Sheets.Moves;
using Xunit;

namespace PokeOneWeb.DataSync.Tests.GoogleSpreadsheet.Import.Impl.Sheets.Moves
{
    public class MoveSheetRowParserTest
    {
        [Fact]
        public void ReadRow_WithMinimalValidValues_ShouldParse()
        {
            // Arrange
            var parser = new MoveXSheetRowParser();

            var name = "Move Name";
            var doInclude = true;
            var resourceName = "Resource Name";
            var damageClassName = "Damage Class Name";
            var typeName = "Type Name";
            var attackPower = 100;
            var accuracy = 100;
            var powerPoints = 10;
            var priority = 1;

            var values = new List<object>
            {
                name,
                doInclude,
                resourceName,
                damageClassName,
                typeName,
                attackPower,
                accuracy,
                powerPoints,
                priority
            };

            var expected = new MoveSheetDto
            {
                Name = name,
                DoInclude = doInclude,
                ResourceName = resourceName,
                DamageClassName = damageClassName,
                TypeName = typeName,
                AttackPower = attackPower,
                Accuracy = accuracy,
                PowerPoints = powerPoints,
                Priority = priority
            };

            // Act
            var result = parser.ReadRow(values);

            // Assert
            result.Should().BeEquivalentTo(expected);
        }

        [Fact]
        public void ReadRow_WithAllValidValues_ShouldParse()
        {
            // Arrange
            var parser = new MoveXSheetRowParser();

            var name = "Move Name";
            var doInclude = true;
            var resourceName = "Resource Name";
            var damageClassName = "Damage Class Name";
            var typeName = "Type Name";
            var attackPower = 100;
            var accuracy = 100;
            var powerPoints = 10;
            var priority = 1;
            var effect = "Effect";

            var values = new List<object>
            {
                name,
                doInclude,
                resourceName,
                damageClassName,
                typeName,
                attackPower,
                accuracy,
                powerPoints,
                priority,
                effect
            };

            var expected = new MoveSheetDto
            {
                Name = name,
                DoInclude = doInclude,
                ResourceName = resourceName,
                DamageClassName = damageClassName,
                TypeName = typeName,
                AttackPower = attackPower,
                Accuracy = accuracy,
                PowerPoints = powerPoints,
                Priority = priority,
                Effect = effect
            };

            // Act
            var result = parser.ReadRow(values);

            // Assert
            result.Should().BeEquivalentTo(expected);
        }

        [Fact]
        public void ReadRow_WithInsufficientValues_ShouldThrow()
        {
            // Arrange
            var parser = new MoveXSheetRowParser();
            var values = new List<object>();

            // Act
            var actual = () => parser.ReadRow(values);

            // Assert
            actual.Should().Throw<InvalidRowDataException>();
        }

        [Fact]
        public void ReadRow_WithTooManyValues_ShouldThrow()
        {
            // Arrange
            var parser = new MoveXSheetRowParser();
            var values = new List<object>()
            {
                "0", false, "0", "0", "0", 0, 0, 0, 0, string.Empty, "excessive value"
            };

            // Act
            var actual = () => parser.ReadRow(values);

            // Assert
            actual.Should().Throw<InvalidRowDataException>();
        }

        [Fact]
        public void ReadRow_WithValuesNull_ShouldThrow()
        {
            // Arrange
            var parser = new MoveXSheetRowParser();
            List<object> values = null;

            // Act
            var actual = () => parser.ReadRow(values);

            // Assert
            actual.Should().Throw<InvalidRowDataException>();
        }

        [Theory]
        [InlineData("")] // Name must be non-empty
        [InlineData("0", "notBoolean")] // DoInclude must be boolean
        [InlineData("0", false, "")] // ResourceName must be non-empty
        [InlineData("0", false, "0", "")] // DamageClassName must be non-empty
        [InlineData("0", false, "0", "0", "")] // TypeName must be non-empty
        [InlineData("0", false, "0", "0", "0", "notInt")] // AttackPower must be int
        [InlineData("0", false, "0", "0", "0", 0, "notInt")] // Accuracy must be int
        [InlineData("0", false, "0", "0", "0", 0, 0, "notInt")] // PowerPoints must be int
        [InlineData("0", false, "0", "0", "0", 0, 0, 0, "notInt")] // Priority must be int
        [InlineData("0", false, "0", "0", "0", 0, 0, 0, 0, 0)] // Effect must be string
        public void ReadRow_WithUnparsableValue_ShouldThrow(params object[] valuesAsArray)
        {
            // Arrange
            var parser = new MoveXSheetRowParser();
            var values = valuesAsArray.ToList();

            // Act
            var actual = () => parser.ReadRow(values);

            // Assert
            actual.Should().Throw<InvalidRowDataException>();
        }
    }
}