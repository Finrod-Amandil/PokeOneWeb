﻿using FluentAssertions;
using PokeOneWeb.DataSync.GoogleSpreadsheet.Import.Impl;
using PokeOneWeb.DataSync.GoogleSpreadsheet.Import.Impl.Sheets.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace PokeOneWeb.DataSync.Tests.GoogleSpreadsheet.Import.Impl.Sheets.Events
{
    public class EventSheetRowParserTest
    {
        [Fact]
        public void ReadRow_WithMinimalValidValues_ShouldParse()
        {
            // Arrange
            var parser = new EventSheetRowParser();
            var name = "Event Name";
            var values = new List<object> { name };

            var expected = new EventSheetDto { Name = name };

            // Act
            var result = parser.ReadRow(values);

            // Assert
            result.Should().BeEquivalentTo(expected);
        }

        [Fact]
        public void ReadRow_WithAllValidValues_ShouldParse()
        {
            // Arrange
            var parser = new EventSheetRowParser();

            var name = "Event Name";
            var startDate = "01.01.2020";
            var endDate = "01.02.2020";

            var values = new List<object>
            {
                name, startDate, endDate
            };

            var expected = new EventSheetDto
            {
                Name = name,
                StartDate = new DateTime(2020, 1, 1),
                EndDate = new DateTime(2020, 2, 1)
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
            var parser = new EventSheetRowParser();
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
            var parser = new EventSheetRowParser();
            var values = new List<object>
            {
                "0", "01.01.2020", "01.02.2020", "excessive value" 
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
            var parser = new EventSheetRowParser();
            List<object> values = null;

            // Act
            var actual = () => parser.ReadRow(values);

            // Assert
            actual.Should().Throw<InvalidRowDataException>();
        }

        [Theory]
        [InlineData("")] // Name must be non-empty
        [InlineData("0", "x")] // Start Date must be DateTime
        [InlineData("0", "01.01.2020", "x")] // End Date must be DateTime
        public void ReadRow_WithUnparsableValue_ShouldThrow(params object[] valuesAsArray)
        {
            // Arrange
            var parser = new EventSheetRowParser();
            var values = valuesAsArray.ToList();

            // Act
            var actual = () => parser.ReadRow(values);

            // Assert
            actual.Should().Throw<InvalidRowDataException>();
        }

        [Fact]
        public void ReadRow_WithWrongDateTimeFormat_ShouldThrow()
        {
            // Arrange
            var parser = new EventSheetRowParser();

            var name = "Event Name";
            var startDate = "2020-01-01";

            var values = new List<object>
            {
                name, startDate
            };

            // Act
            var actual = () => parser.ReadRow(values);

            // Assert
            actual.Should().Throw<InvalidRowDataException>();
        }
    }
}