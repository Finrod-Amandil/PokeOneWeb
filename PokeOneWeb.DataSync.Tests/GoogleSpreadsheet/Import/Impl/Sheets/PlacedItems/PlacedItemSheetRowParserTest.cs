﻿using FluentAssertions;
using PokeOneWeb.DataSync.GoogleSpreadsheet.Import.Impl;
using PokeOneWeb.DataSync.GoogleSpreadsheet.Import.Impl.Sheets.PlacedItems;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace PokeOneWeb.DataSync.Tests.GoogleSpreadsheet.Import.Impl.Sheets.PlacedItems
{
    public class PlacedItemSheetRowParserTest
    {
        [Fact]
        public void ReadRow_WithMinimalValidValues_ShouldParse()
        {
            // Arrange
            var parser = new PlacedItemSheetRowParser();

            var locationName = "Location Name";
            var quantity = 5;
            var itemName = "Item Name";
            var sortIndex = 1;
            var index = 2;

            var values = new List<object>
            {
                locationName,
                quantity,
                itemName,
                sortIndex,
                index
            };

            var expected = new PlacedItemSheetDto()
            {
                LocationName = locationName,
                Quantity = quantity,
                ItemName = itemName,
                SortIndex = sortIndex,
                Index = index
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
            var parser = new PlacedItemSheetRowParser();

            var locationName = "Location Name";
            var quantity = 5;
            var itemName = "Item Name";
            var sortIndex = 1;
            var index = 2;
            var placementDescription = "Placement Description";
            var isHidden = true;
            var isConfirmed = false;
            var requirements = "Requirements";
            var screenshotName = "screenshot.png";
            var notes = "Notes";

            var values = new List<object>
            {
                locationName,
                quantity,
                itemName,
                sortIndex,
                index,
                placementDescription,
                isHidden,
                isConfirmed,
                requirements,
                screenshotName,
                notes
            };

            var expected = new PlacedItemSheetDto()
            {
                LocationName = locationName,
                Quantity = quantity,
                ItemName = itemName,
                SortIndex = sortIndex,
                Index = index,
                PlacementDescription = placementDescription,
                IsHidden = isHidden,
                IsConfirmed = isConfirmed,
                Requirements = requirements,
                ScreenshotName = screenshotName,
                Notes = notes
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
            var parser = new PlacedItemSheetRowParser();
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
            var parser = new PlacedItemSheetRowParser();
            var values = new List<object>()
            {
                "0", 0, "0", 0, 0, "", false, false, "", "", "", "excessive value"
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
            var parser = new PlacedItemSheetRowParser();
            List<object> values = null;

            // Act
            var actual = () => parser.ReadRow(values);

            // Assert
            actual.Should().Throw<InvalidRowDataException>();
        }

        [Theory]
        [InlineData("")] // LocationName must be non-empty
        [InlineData("0", "notInt")] // Quantity must be int
        [InlineData("0", 0, "")] // ItemName must be non-empty
        [InlineData("0", 0, "0", "notInt")] // SortIndex must be int
        [InlineData("0", 0, "0", 0, "notInt")] // Index must be int
        [InlineData("0", 0, "0", 0, 0, 0)] // PlacementDescription must be string
        [InlineData("0", 0, "0", 0, 0, "", "notBoolean")] // IsHidden must be boolean
        [InlineData("0", 0, "0", 0, 0, "", false, "notBoolean")] // IsConfirmed must be boolean
        [InlineData("0", 0, "0", 0, 0, "", false, false, 0)] // Requirements must be string
        [InlineData("0", 0, "0", 0, 0, "", false, false, "", 0)] // ScreenshotName must be string
        [InlineData("0", 0, "0", 0, 0, "", false, false, "", "", 0)] // Notes must be string
        public void ReadRow_WithUnparsableValue_ShouldThrow(params object[] valuesAsArray)
        {
            // Arrange
            var parser = new PlacedItemSheetRowParser();
            var values = valuesAsArray.ToList();

            // Act
            var actual = () => parser.ReadRow(values);

            // Assert
            actual.Should().Throw<InvalidRowDataException>();
        }

        [Fact]
        public void ReadRow_WithMissingOptionalNonStringValues_ShouldParse()
        {
            // Arrange
            var parser = new PlacedItemSheetRowParser();

            var locationName = "Location Name";
            var quantity = 5;
            var itemName = "Item Name";
            var sortIndex = 1;
            var index = 2;
            var placementDescription = "Placement Description";
            var isHidden = "";
            var isConfirmed = "";

            var values = new List<object>
            {
                locationName,
                quantity,
                itemName,
                sortIndex,
                index,
                placementDescription,
                isHidden,
                isConfirmed,
            };

            var defaultIsHidden = false;
            var defaultIsConfirmed = true;

            var expected = new PlacedItemSheetDto()
            {
                LocationName = locationName,
                Quantity = quantity,
                ItemName = itemName,
                SortIndex = sortIndex,
                Index = index,
                PlacementDescription = placementDescription,
                IsHidden = defaultIsHidden,
                IsConfirmed = defaultIsConfirmed,
            };

            // Act
            var result = parser.ReadRow(values);

            // Assert
            result.Should().BeEquivalentTo(expected);
        }
    }
}