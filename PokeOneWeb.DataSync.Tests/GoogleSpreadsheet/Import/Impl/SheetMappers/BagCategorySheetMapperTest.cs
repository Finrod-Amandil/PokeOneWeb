﻿using System;
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
    public class BagCategorySheetMapperTest
    {
        private readonly Mock<ISpreadsheetImportReporter> _reporterMock;
        private readonly BagCategorySheetMapper _mapper;

        private readonly RowHash _rowHash = new() { IdHash = "Id Hash", Hash = "Hash" };
        private List<string> _columnNames = new() { "Name", "SortIndex" };
        private List<object> _values;

        private List<SheetDataRow> Data => new() { new SheetDataRow(_columnNames, _rowHash, _values) };

        public BagCategorySheetMapperTest()
        {
            _reporterMock = new Mock<ISpreadsheetImportReporter>();
            _mapper = new BagCategorySheetMapper(_reporterMock.Object);
        }

        [Fact]
        public void Map_WithMinimalValidValues_ShouldMap()
        {
            // Arrange
            const string name = "BagCategory Name";
            const int sortIndex = 5;

            _values = new List<object>
            {
                name,
                sortIndex
            };

            var expected = new BagCategory
            {
                IdHash = _rowHash.IdHash,
                Hash = _rowHash.Hash,
                Name = name,
                SortIndex = sortIndex
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
            const string name1 = "BagCategory Name 1";
            const int sortIndex1 = 5;

            const string name2 = "BagCategory Name 2";
            const int sortIndex2 = 6;

            List<object> values1 = new()
            {
                name1,
                sortIndex1
            };

            List<object> values2 = new()
            {
                name2,
                sortIndex2
            };

            var data = new List<SheetDataRow>
            {
                new(_columnNames, _rowHash, values1),
                new(_columnNames, _rowHash, values2)
            };

            var expected1 = new BagCategory
            {
                IdHash = _rowHash.IdHash,
                Hash = _rowHash.Hash,
                Name = name1,
                SortIndex = sortIndex1
            };

            var expected2 = new BagCategory
            {
                IdHash = _rowHash.IdHash,
                Hash = _rowHash.Hash,
                Name = name2,
                SortIndex = sortIndex2
            };

            // Act
            var actual = _mapper.Map(data).ToList();

            // Assert
            actual.Count.Should().Be(2);
            actual.Single(x => x.Name.Equals(name1)).Should().BeEquivalentTo(expected1);
            actual.Single(x => x.Name.Equals(name2)).Should().BeEquivalentTo(expected2);
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
                    nameof(BagCategory),
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
        [InlineData("", 5)]
        [InlineData("a", "notInt")]
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
                    nameof(BagCategory),
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
                "SortIndex",
                "Name"
            };

            const string name = "BagCategory Name";
            const int sortIndex = 5;

            _values = new List<object>
            {
                sortIndex,
                name
            };

            var expected = new BagCategory()
            {
                IdHash = _rowHash.IdHash,
                Hash = _rowHash.Hash,
                Name = name,
                SortIndex = sortIndex
            };

            // Act
            var actual = _mapper.Map(Data).ToList();

            // Assert
            actual.Count.Should().Be(1);
            actual[0].Should().BeEquivalentTo(expected);
        }
    }
}