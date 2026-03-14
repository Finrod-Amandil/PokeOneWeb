using System.Collections.Generic;
using PokeOneWeb.Data.Entities.Interfaces;
using PokeOneWeb.DataSync.GoogleSpreadsheet.DataTypes;

namespace PokeOneWeb.DataSync.Import.Interfaces
{
    public interface ISheetMapper<out TEntity> where TEntity : class, IHashedEntity
    {
        /// <summary>
        /// Maps the raw data rows from a google sheet to the data store entities.
        /// </summary>
        IEnumerable<TEntity> Map(IEnumerable<SheetDataRow> sheetDataRows);
    }
}