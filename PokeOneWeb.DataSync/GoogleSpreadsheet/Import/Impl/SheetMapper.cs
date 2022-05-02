using System;
using System.Collections.Generic;
using PokeOneWeb.Data.Entities.Interfaces;
using PokeOneWeb.DataSync.GoogleSpreadsheet.DataTypes;
using PokeOneWeb.DataSync.GoogleSpreadsheet.Exceptions;
using PokeOneWeb.Shared.Exceptions;

namespace PokeOneWeb.DataSync.GoogleSpreadsheet.Import.Impl
{
    public abstract class SheetMapper<TEntity> : ISheetMapper<TEntity> where TEntity : class, IHashedEntity, new()
    {
        protected readonly ISpreadsheetImportReporter Reporter;

        protected SheetMapper(ISpreadsheetImportReporter reporter)
        {
            Reporter = reporter;
        }

        public IEnumerable<TEntity> Map(IEnumerable<SheetDataRow> sheetDataRows)
        {
            if (sheetDataRows is null)
            {
                throw new ArgumentNullException(nameof(sheetDataRows));
            }

            foreach (var row in sheetDataRows)
            {
                var canMap = TryMapEntity(row, out var mappedEntity);

                if (!canMap)
                {
                    continue;
                }

                yield return mappedEntity;
            }
        }

        protected abstract Dictionary<string, Action<TEntity, object>> ValueToEntityMappings { get; }

        private bool TryMapEntity(SheetDataRow row, out TEntity entity)
        {
            entity = new TEntity
            {
                IdHash = row.IdHash,
                Hash = row.Hash,
                ImportSheetId = row.ImportSheetId
            };

            var canMap = true;
            foreach (var (columnName, mapValueOntoEntity) in ValueToEntityMappings)
            {
                try
                {
                    mapValueOntoEntity(entity, row[columnName]);
                }
                catch (Exception e) when (e is InvalidColumnNameException or InvalidRowDataException or ParseException)
                {
                    Reporter.ReportError(typeof(TEntity).Name, row.IdHash, e);
                    canMap = false;
                }
            }

            return canMap;
        }
    }
}