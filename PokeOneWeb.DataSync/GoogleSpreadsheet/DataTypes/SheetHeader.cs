using System.Collections.Generic;
using PokeOneWeb.DataSync.GoogleSpreadsheet.Exceptions;

namespace PokeOneWeb.DataSync.GoogleSpreadsheet.DataTypes
{
    public class SheetHeader
    {
        private readonly IList<string> _columnNames;

        public SheetHeader(IList<string> columnNames)
        {
            _columnNames = columnNames;
        }

        public int GetColumnIndexForColumnName(string columnName)
        {
            var index = _columnNames.IndexOf(columnName);
            return index != -1 ? index : throw new InvalidColumnNameException(columnName);
        }
    }
}