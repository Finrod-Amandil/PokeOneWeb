using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using PokeOneWeb.Configuration;
using PokeOneWeb.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using PokeOneWeb.Data.Entities;
using PokeOneWeb.Services.GoogleSpreadsheet.Import.Impl.Reporting;

namespace PokeOneWeb.Services.GoogleSpreadsheet.Import.Impl
{
    public class GoogleSpreadsheetImportService : IGoogleSpreadsheetImportService
    {
        private readonly ILogger<GoogleSpreadsheetImportService> _logger;
        private readonly IOptions<GoogleSpreadsheetsSettings> _settings;
        private readonly ApplicationDbContext _dbContext;
        private readonly ISpreadsheetDataLoader _dataLoader;
        private readonly ISheetNameHelper _sheetNameHelper;
        private readonly ISpreadsheetImportReporter _reporter;
        private readonly IHashListComparator _hashListComparator;

        public GoogleSpreadsheetImportService(
            ILogger<GoogleSpreadsheetImportService> logger,
            IOptions<GoogleSpreadsheetsSettings> settings,
            ApplicationDbContext dbContext,
            ISpreadsheetDataLoader dataLoader,
            ISheetNameHelper sheetNameHelper,
            ISpreadsheetImportReporter reporter,
            IHashListComparator hashListComparator)
        {
            _logger = logger;
            _settings = settings;
            _dbContext = dbContext;
            _dataLoader = dataLoader;
            _sheetNameHelper = sheetNameHelper;
            _reporter = reporter;
            _hashListComparator = hashListComparator;
        }

        public async Task<SpreadsheetImportReport> ImportSpreadsheetData()
        {
            _reporter.NewSession();
            _reporter.StartImport();

            var sheetsData = await _dataLoader.LoadRange(
                _settings.Value.Import.SheetsListSpreadsheetId,
                _settings.Value.Import.SheetsListSheetName,
                "B2:C");

            foreach (var sheetData in sheetsData)
            {
                var spreadsheetId = sheetData[0].ToString();
                var sheetName = sheetData[1].ToString();

                _reporter.StartImport(sheetName);

                var sheet = GetSheet(spreadsheetId, sheetName);

                await ImportSheet(sheet);

                _dbContext.ChangeTracker.Clear();
                GC.Collect();

                _reporter.StopImport(sheetName);
            }

            _reporter.StopImport();
            return _reporter.GetReport();
        }

        private async Task ImportSheet(ImportSheet sheet)
        {
            var sheetHash = await _dataLoader.LoadSheetHash(sheet.SpreadsheetId, sheet.SheetName);
            if (!HasSheetChanged(sheet, sheetHash))
            {
                _logger.LogInformation($"No changes found in sheet {sheet.SheetName}.");
                return;
            }

            var repository = _sheetNameHelper.GetSheetRepositoryForSheetName(sheet.SheetName);
            var sheetHashes = await _dataLoader.LoadHashes(sheet.SpreadsheetId, sheet.SheetName, sheet.Id);
            var dbHashes = repository.ReadDbHashes(sheet);

            // Compare hash list of sheet and DB to find rows that need to be deleted, inserted, updated
            var hashListComparisonResult = _hashListComparator.CompareHashLists(sheetHashes, dbHashes);

            var sheetIdHashes = sheetHashes.Select(rh => rh.IdHash).ToList();

            // Delete
            if (hashListComparisonResult.RowsToDelete.Any())
            {
                var deletedCount = repository.Delete(hashListComparisonResult.RowsToDelete);
                _logger.LogInformation($"Deleted {deletedCount} entries for sheet {sheet.SheetName}.");
            }

            // Insert
            if (hashListComparisonResult.RowsToInsert.Any())
            {
                var insertRowIndexes = GetRowIndexesForIdHashes(hashListComparisonResult.RowsToInsert.Select(rh => rh.IdHash), sheetIdHashes);

                var dataToInsert = await _dataLoader.LoadRows(sheet.SpreadsheetId, sheet.SheetName, insertRowIndexes);
                var dataToInsertForHashes = hashListComparisonResult.RowsToInsert
                    .Zip(dataToInsert, (hash, values) => new { hash, values })
                    .ToDictionary(x => x.hash, x => x.values);

                var insertedCount = repository.Insert(dataToInsertForHashes);
                _logger.LogInformation($"Inserted {insertedCount} entries for sheet {sheet.SheetName}.");
            }
            
            // Update
            if (hashListComparisonResult.RowsToUpdate.Any())
            {
                var updateRowIndexes = GetRowIndexesForIdHashes(hashListComparisonResult.RowsToUpdate.Select(rh => rh.IdHash), sheetIdHashes);

                var dataToUpdate = await _dataLoader.LoadRows(sheet.SpreadsheetId, sheet.SheetName, updateRowIndexes);
                var dataToUpdateForHashes = hashListComparisonResult.RowsToUpdate
                    .Zip(dataToUpdate, (hash, values) => new { hash, values })
                    .ToDictionary(x => x.hash, x => x.values);

                var updatedCount = repository.Update(dataToUpdateForHashes);
                _logger.LogInformation($"Updated {updatedCount} entries for sheet {sheet.SheetName}.");
            }

            UpdateSheetHash(sheet.SpreadsheetId, sheet.SheetName, sheetHash);
        }

        private bool HasSheetChanged(ImportSheet sheet, string sheetHash)
        {
            return !string.Equals(sheet.SheetHash, sheetHash, StringComparison.Ordinal);
        }

        private ImportSheet GetSheet(string spreadsheetId, string sheetName)
        {
            var sheet = _dbContext.ImportSheets
                .SingleOrDefault(s =>
                    s.SpreadsheetId.Equals(spreadsheetId) &&
                    s.SheetName.Equals(sheetName));

            if (sheet is null)
            {
                sheet = new ImportSheet
                {
                    SpreadsheetId = spreadsheetId,
                    SheetName = sheetName,
                    SheetHash = "No successful import yet."
                };

                _dbContext.ImportSheets.Add(sheet);

                _dbContext.SaveChanges();
            }

            return sheet;
        }

        private void UpdateSheetHash(string spreadsheetId, string sheetName, string sheetHash)
        {
            var dbSheet = _dbContext.ImportSheets
                .SingleOrDefault(s =>
                    s.SpreadsheetId.Equals(spreadsheetId) &&
                    s.SheetName.Equals(sheetName));

            if (dbSheet is null)
            {
                throw new Exception();
            }
            dbSheet.SheetHash = sheetHash;

            _dbContext.SaveChanges();
        }

        private static List<int> GetRowIndexesForIdHashes(IEnumerable<string> selectedHashes, IList<string> allHashes)
        {
            var result = selectedHashes.Select(allHashes.IndexOf).OrderBy(i => i).ToList();

            return result;
        }
    }
}
