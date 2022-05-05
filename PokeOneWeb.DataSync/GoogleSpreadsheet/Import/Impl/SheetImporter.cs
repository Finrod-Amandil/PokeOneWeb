using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PokeOneWeb.Data.Entities;
using PokeOneWeb.Data.Entities.Interfaces;
using PokeOneWeb.Data.Repositories;

namespace PokeOneWeb.DataSync.GoogleSpreadsheet.Import.Impl
{
    public class SheetImporter<TEntity> : ISheetImporter where TEntity : class, IHashedEntity
    {
        private readonly ISpreadsheetDataLoader _dataLoader;
        private readonly IImportSheetRepository _importSheetRepository;
        private readonly IHashedEntityRepository<TEntity> _repository;
        private readonly ISheetMapper<TEntity> _mapper;
        private readonly IHashListComparator _hashListComparator;
        private readonly ISpreadsheetImportReporter _reporter;

        public SheetImporter(
            ISpreadsheetDataLoader dataLoader,
            IImportSheetRepository importSheetRepository,
            IHashedEntityRepository<TEntity> repository,
            ISheetMapper<TEntity> mapper,
            IHashListComparator hashListComparator,
            ISpreadsheetImportReporter reporter)
        {
            _dataLoader = dataLoader;
            _importSheetRepository = importSheetRepository;
            _repository = repository;
            _mapper = mapper;
            _hashListComparator = hashListComparator;
            _reporter = reporter;

            // Report if an entity fails to be inserted to the database.
            _repository.UpdateOrInsertExceptionOccurred += (_, args) =>
                _reporter.ReportError(args.EntityType.Name, args.Exception);
        }

        public async Task ImportSheet(string spreadsheetId, string sheetName)
        {
            _reporter.StartImport(sheetName);

            var sheet = _importSheetRepository.FindBySpreadsheetIdAndSheetName(spreadsheetId, sheetName);
            var sheetHash = await _dataLoader.LoadSheetHash(sheet.SpreadsheetId, sheet.SheetName);

            if (!HasSheetChanged(sheet, sheetHash))
            {
                _reporter.StopImport(sheetName, 0, 0, 0);
                return;
            }

            // Compare hash list of sheet and DB to find rows that need to be deleted, inserted, updated
            var sheetHashes = await _dataLoader.LoadHashes(sheet.SpreadsheetId, sheet.SheetName, sheet.Id);
            var dbHashes = _repository.GetHashesForSheet(sheet).ToList();
            var hashListComparisonResult = _hashListComparator.CompareHashLists(sheetHashes, dbHashes);

            var sheetIdHashes = sheetHashes.Select(rh => rh.IdHash).ToList();

            var deletedCount = Delete(hashListComparisonResult.RowsToDelete);
            var insertedCount = await Insert(hashListComparisonResult.RowsToInsert, sheetIdHashes, sheet);
            var updatedCount = await Update(hashListComparisonResult.RowsToUpdate, sheetIdHashes, sheet);

            // Update sheet hash
            sheet.SheetHash = sheetHash;
            _importSheetRepository.Update(sheet);

            _reporter.StopImport(sheetName, insertedCount, updatedCount, deletedCount);
        }

        private static bool HasSheetChanged(ImportSheet sheet, string sheetHash)
        {
            return !string.Equals(sheet.SheetHash, sheetHash, StringComparison.Ordinal);
        }

        private int Delete(List<string> rowsToDelete)
        {
            if (rowsToDelete.Any())
            {
                var deletedCount = _repository.DeleteByIdHashes(rowsToDelete);

                return deletedCount;
            }

            return 0;
        }

        private async Task<int> Insert(List<string> rowsToInsert, List<string> sheetIdHashes, ImportSheet sheet)
        {
            if (rowsToInsert.Any())
            {
                var sheetData = await _dataLoader.LoadDataRows(sheet, rowsToInsert, sheetIdHashes);
                var entities = _mapper.Map(sheetData).ToList();
                var insertedCount = _repository.Insert(entities);

                return insertedCount;
            }

            return 0;
        }

        private async Task<int> Update(List<string> rowsToUpdate, List<string> sheetIdHashes, ImportSheet sheet)
        {
            if (rowsToUpdate.Any())
            {
                var sheetData = await _dataLoader.LoadDataRows(sheet, rowsToUpdate, sheetIdHashes);
                var entities = _mapper.Map(sheetData).ToList();
                var updatedCount = _repository.UpdateByIdHashes(entities);

                return updatedCount;
            }

            return 0;
        }
    }
}