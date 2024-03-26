using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PokeOneWeb.Data.Entities.Interfaces;
using PokeOneWeb.Data.Repositories;
using PokeOneWeb.DataSync.GoogleSpreadsheet.DataTypes;
using PokeOneWeb.DataSync.Import.Interfaces;

namespace PokeOneWeb.DataSync.Import
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

            // Get notified if an entity fails to be inserted to the database.
            _repository.UpdateOrInsertExceptionOccurred += (_, args) =>
                _reporter.ReportError(args.EntityType.Name, args.Exception);
        }

        public async Task ImportSheet(string spreadsheetId, string sheetName)
        {
            _reporter.StartImport(sheetName);

            // Compare hash list of sheet and DB to find rows that need to be deleted, inserted, updated
            var sheet = _importSheetRepository.FindBySpreadsheetIdAndSheetName(spreadsheetId, sheetName);
            var sheetData = await _dataLoader.LoadSheetRows(sheet);
            var sheetHashes = sheetData.Select(row => row.RowHash).ToList();

            var dbHashes = _repository.GetHashesForSheet(sheet);
            var hashListComparisonResult = _hashListComparator.CompareHashLists(sheetHashes, dbHashes);

            sheetData = FilterDuplicates(sheetData, hashListComparisonResult.DuplicateIdHashes);

            var deletedCount = Delete(hashListComparisonResult.RowsToDelete);
            var insertedCount = Insert(hashListComparisonResult.RowsToInsert, sheetData);
            var updatedCount = Update(hashListComparisonResult.RowsToUpdate, sheetData);

            _reporter.StopImport(sheetName, insertedCount, updatedCount, deletedCount);
        }

        private List<SheetDataRow> FilterDuplicates(List<SheetDataRow> rows, List<string> duplicateIdHashes)
        {
            if (!duplicateIdHashes.Any())
            {
                return rows;
            }

            var seenDuplicates = new HashSet<string>();

            var filteredList = new List<SheetDataRow>();
            foreach (var row in rows)
            {
                if (!seenDuplicates.Contains(row.IdHash))
                {
                    filteredList.Add(row);
                }

                if (duplicateIdHashes.Contains(row.IdHash))
                {
                    _reporter.ReportError($"Found entry with duplicate hash: {row}");
                    seenDuplicates.Add(row.IdHash);
                }
            }

            return filteredList;
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
            var ids = new HashSet<string>(rowsToInsert);
            var dataToInsert = sheetData.Where(x => ids.Contains(x.IdHash));

            var insertedCount = 0;
            if (dataToInsert.Any())
            {
                var entities = _mapper.Map(dataToInsert).ToList();
                insertedCount = _repository.Insert(entities);
            }

            return insertedCount;
        }

        private int Update(List<string> rowsToUpdate, List<SheetDataRow> sheetData)
        {
            var ids = new HashSet<string>(rowsToUpdate);
            var dataToInsert = sheetData.Where(x => ids.Contains(x.IdHash));

            var updatedCount = 0;
            if (dataToInsert.Any())
            {
                var entities = _mapper.Map(dataToInsert).ToList();
                updatedCount = _repository.UpdateByIdHashes(entities);
            }

            return updatedCount;
        }
    }
}