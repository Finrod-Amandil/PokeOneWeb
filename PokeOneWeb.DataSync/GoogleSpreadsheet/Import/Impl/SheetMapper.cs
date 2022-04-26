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
                yield return MapEntity(row);
            }
        }

        protected abstract Dictionary<string, Action<TEntity, object>> ValueToEntityMappings { get; }

        private TEntity MapEntity(SheetDataRow row)
        {
            var entity = new TEntity
            {
                IdHash = row.IdHash,
                Hash = row.Hash,
                ImportSheetId = row.ImportSheetId
            };

            foreach (var (columnName, mapValueOntoEntity) in ValueToEntityMappings)
            {
                try
                {
                    mapValueOntoEntity(entity, row[columnName]);
                }
                catch (Exception e) when (e is InvalidRowDataException or ParseException)
                {
                    Reporter.ReportError(typeof(TEntity).Name, row.IdHash, e);
                }
            }

            return entity;
        }
    }
}