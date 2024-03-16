﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using PokeOneWeb.DataSync.GoogleSpreadsheet.Configuration;
using PokeOneWeb.DataSync.GoogleSpreadsheet.Import.Impl.Reporting;
using PokeOneWeb.DataSync.Utils;

namespace PokeOneWeb.DataSync.GoogleSpreadsheet.Import.Impl
{
    public class GoogleSpreadsheetImportService : IGoogleSpreadsheetImportService
    {
        private readonly IOptions<GoogleSpreadsheetsSettings> _settings;
        private readonly ISpreadsheetDataLoader _dataLoader;
        private readonly ISpreadsheetImportReporter _reporter;
        private readonly IDictionary<string, ISheetImporter> _sheetImporters;

        public GoogleSpreadsheetImportService(
            IOptions<GoogleSpreadsheetsSettings> settings,
            IServiceProvider serviceProvider,
            ISpreadsheetDataLoader dataLoader,
            ISpreadsheetImportReporter reporter)
        {
            _settings = settings;
            _dataLoader = dataLoader;
            _reporter = reporter;
            _sheetImporters = ReflectionUtils.LoadSheetImporters(serviceProvider);
        }

        public async Task<SpreadsheetImportReport> ImportSpreadsheetData()
        {
            _reporter.NewSession();
            _reporter.StartImport();

            var sheets = await _dataLoader.LoadSheetList(
                _settings.Value.Import.SheetsListSpreadsheetId,
                _settings.Value.Import.SheetsListSheetName);

            foreach (var sheet in sheets)
            {
                var sheetImporter = FindSheetImporterForSheet(sheet.SheetName);
                await sheetImporter.ImportSheet(sheet.SpreadsheetId, sheet.SheetName);
            }

            _reporter.StopImport();
            return _reporter.GetReport();
        }

        private ISheetImporter FindSheetImporterForSheet(string sheetName)
        {
            // Sheet name can either be an exact match, or have a suffix, i.e. placed_items_kanto (matches placed_items)
            var keys = _sheetImporters.Keys.Where(sheetName.StartsWith).ToList();

            return keys.Count == 1 ? _sheetImporters[keys[0]] :
                throw new Exception($"No suitable single importer could be found for sheet with sheet name {sheetName}.");
        }
    }
}