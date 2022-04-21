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
        private readonly ISpreadsheetImportReporter _reporter;

        protected SheetMapper(ISpreadsheetImportReporter reporter)
        {
            _reporter = reporter;
        }

        public IEnumerable<TEntity> Map(IEnumerable<SheetDataRow> sheetDataRows)
        {
            if (sheetDataRows is null)
            {
                throw new ArgumentNullException(nameof(sheetDataRows));
            }

            var entities = new List<TEntity>();

            foreach (var row in sheetDataRows)
            {
                try
                {
                    entities.Add(MapEntity(row));
                }
                catch (Exception e) when (e is InvalidRowDataException or ParseException)
                {
                    _reporter.ReportError(typeof(TEntity).Name, row.IdHash, e);
                }
            }

            return entities;
        }

        protected abstract Dictionary<string, Action<TEntity, object>> ValueToEntityMappings { get; }

        protected abstract int RequiredValueCount { get; }

        private TEntity MapEntity(SheetDataRow row)
        {
            if (row.ValueCount < RequiredValueCount)
            {
                throw new InvalidRowDataException("Row data does not contain sufficient values.");
            }

            var entity = new TEntity();

            foreach (var (columnName, mapping) in ValueToEntityMappings)
            {
                mapping(entity, row[columnName]);
            }

            return entity;
        }
    }
}