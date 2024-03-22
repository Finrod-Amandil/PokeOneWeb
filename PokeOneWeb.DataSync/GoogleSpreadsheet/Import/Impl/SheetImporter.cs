using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PokeOneWeb.Data.Entities.Interfaces;
using PokeOneWeb.Data.Repositories;
using PokeOneWeb.DataSync.GoogleSpreadsheet.DataTypes;

namespace PokeOneWeb.DataSync.GoogleSpreadsheet.Import.Impl
{
    public class SheetImporter<TEntity> : ISheetImporter where TEntity : class, IHashedEntity
    {
        private readonly ISpreadsheetDataLoader _dataLoader;
        private readonly IHashedEntityRepository<TEntity> _repository;
        private readonly ISheetMapper<TEntity> _mapper;
        private readonly IHashListComparator _hashListComparator;
        private readonly ISpreadsheetImportReporter _reporter;

        public SheetImporter(
            ISpreadsheetDataLoader dataLoader,
            IHashedEntityRepository<TEntity> repository,
            ISheetMapper<TEntity> mapper,
            IHashListComparator hashListComparator,
            ISpreadsheetImportReporter reporter)
        {
            _dataLoader = dataLoader;
            _repository = repository;
            _mapper = mapper;
            _hashListComparator = hashListComparator;
            _reporter = reporter;

            // Get notified if an entity fails to be inserted to the database.
            _repository.UpdateOrInsertExceptionOccurred += (_, args) =>
                _reporter.ReportError(args.EntityType.Name, args.Exception);
        }

        public async Task ImportSheet(string spreadsheetId, string sheetName)
        {
            _reporter.StartImport(sheetName);

            // Compare hash list of sheet and DB to find rows that need to be deleted, inserted, updated

            // TODO Load all data and calculate hashes
            var sheetData = await _dataLoader.LoadSheetRows(spreadsheetId, sheetName);
            var sheetHashes = sheetData.Select(row => row.RowHash).ToList();

            var dbHashes = _repository.GetHashes();
            var hashListComparisonResult = _hashListComparator.CompareHashLists(sheetHashes, dbHashes);

            var sheetIdHashes = sheetHashes.Select(rh => rh.IdHash).ToList();

            var deletedCount = Delete(hashListComparisonResult.RowsToDelete);
            var insertedCount = Insert(hashListComparisonResult.RowsToInsert, sheetData);
            var updatedCount = Update(hashListComparisonResult.RowsToUpdate, sheetData);

            _reporter.StopImport(sheetName, insertedCount, updatedCount, deletedCount);
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

        private int Insert(List<string> rowsToInsert, List<SheetDataRow> sheetData)
        {
            if (rowsToInsert.Any())
            {
                var entities = _mapper.Map(sheetData).ToList();
                var insertedCount = _repository.Insert(entities);

                return insertedCount;
            }

            return 0;
        }

        private int Update(List<string> rowsToUpdate, List<SheetDataRow> sheetData)
        {
            if (rowsToUpdate.Any())
            {
                var entities = _mapper.Map(sheetData).ToList();
                var updatedCount = _repository.UpdateByIdHashes(entities);

                return updatedCount;
            }

            return 0;
        }
    }
}