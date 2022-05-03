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
                return;
            }

            // Compare hash list of sheet and DB to find rows that need to be deleted, inserted, updated
            var sheetHashes = await _dataLoader.LoadHashes(sheet.SpreadsheetId, sheet.SheetName, sheet.Id);
            var dbHashes = _repository.GetHashesForSheet(sheet).ToList();
            var hashListComparisonResult = _hashListComparator.CompareHashLists(sheetHashes, dbHashes);

            var sheetIdHashes = sheetHashes.Select(rh => rh.IdHash).ToList();

            Delete(hashListComparisonResult.RowsToDelete);
            await Insert(hashListComparisonResult.RowsToInsert, sheetIdHashes, sheet);
            await Update(hashListComparisonResult.RowsToUpdate, sheetIdHashes, sheet);

            // Update sheet hash
            sheet.SheetHash = sheetHash;
            _importSheetRepository.Update(sheet);

            _reporter.StopImport(sheetName);
        }

        private static bool HasSheetChanged(ImportSheet sheet, string sheetHash)
        {
            return !string.Equals(sheet.SheetHash, sheetHash, StringComparison.Ordinal);
        }

        private void Delete(List<string> rowsToDelete)
        {
            if (rowsToDelete.Any())
            {
                _repository.DeleteByIdHashes(rowsToDelete);
            }
        }

        private async Task Insert(List<string> rowsToInsert, List<string> sheetIdHashes, ImportSheet sheet)
        {
            if (rowsToInsert.Any())
            {
                var sheetData = await _dataLoader.LoadDataRows(sheet, rowsToInsert, sheetIdHashes);
                var entities = _mapper.Map(sheetData).ToList();
                _repository.Insert(entities);
            }
        }

        private async Task Update(List<string> rowsToUpdate, List<string> sheetIdHashes, ImportSheet sheet)
        {
            if (rowsToUpdate.Any())
            {
                var sheetData = await _dataLoader.LoadDataRows(sheet, rowsToUpdate, sheetIdHashes);
                var entities = _mapper.Map(sheetData).ToList();
                _repository.UpdateByIdHashes(entities);
            }
        }
    }
}