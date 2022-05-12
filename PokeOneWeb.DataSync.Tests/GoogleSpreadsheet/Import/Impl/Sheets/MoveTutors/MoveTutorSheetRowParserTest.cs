﻿using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using PokeOneWeb.DataSync.GoogleSpreadsheet.Import.Impl;
using PokeOneWeb.DataSync.GoogleSpreadsheet.Import.Impl.Sheets.MoveTutors;
using Xunit;

namespace PokeOneWeb.DataSync.Tests.GoogleSpreadsheet.Import.Impl.Sheets.MoveTutors
{
    public class MoveTutorSheetRowParserTest
    {
        [Fact]
        public void ReadRow_WithMinimalValidValues_ShouldParse()
        {
            // Arrange
            var parser = new MoveTutorSheetRowParser();

            var name = "Move Tutor Name";
            var locationName = "Location Name";

            var values = new List<object>
            {
                name,
                locationName
            };

            var expected = new MoveTutorSheetDto
            {
                Name = name,
                LocationName = locationName
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
            var parser = new MoveTutorSheetRowParser();

            var name = "Move Tutor Name";
            var locationName = "Location Name";
            var placementDescription = "Placement Description";

            var values = new List<object>
            {
                name,
                locationName,
                placementDescription
            };

            var expected = new MoveTutorSheetDto
            {
                Name = name,
                LocationName = locationName,
                PlacementDescription = placementDescription
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
            var parser = new MoveTutorSheetRowParser();
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
            var parser = new MoveTutorSheetRowParser();
            var values = new List<object>()
            {
                "0", "0", string.Empty, "excessive value"
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
            var parser = new MoveTutorSheetRowParser();
            List<object> values = null;

            // Act
            var actual = () => parser.ReadRow(values);

            // Assert
            actual.Should().Throw<InvalidRowDataException>();
        }

        [Theory]
        [InlineData("")] // Name must be non-empty
        [InlineData("0", "")] // LocationName must be non-empty
        [InlineData("0", "0", 0)] // PlacementDescription must be string
        public void ReadRow_WithUnparsableValue_ShouldThrow(params object[] valuesAsArray)
        {
            // Arrange
            var parser = new MoveTutorSheetRowParser();
            var values = valuesAsArray.ToList();

            // Act
            var actual = () => parser.ReadRow(values);

            // Assert
            actual.Should().Throw<InvalidRowDataException>();
        }
    }
}