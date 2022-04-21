using System.Collections.Generic;
using PokeOneWeb.Data.Entities.Interfaces;
using PokeOneWeb.DataSync.GoogleSpreadsheet.DataTypes;

namespace PokeOneWeb.DataSync.GoogleSpreadsheet.Import
{
    public interface ISheetMapper<out TEntity> where TEntity : class, IHashedEntity
    {
        IEnumerable<TEntity> Map(IEnumerable<SheetDataRow> sheetDataRows);
    }
}