using System.Collections.Generic;
using PokeOneWeb.Data;

namespace PokeOneWeb.DataSync.GoogleSpreadsheet.DataTypes
{
    public class SheetDataRow
    {
        private readonly RowHash _hash;
        private readonly IList<object> _values;
        private readonly SheetHeader _header;

        public string IdHash => _hash.IdHash;

        public string Hash => _hash.Hash;

        public int ValueCount => _values.Count;

        public object this[string columnName]
        {
            get
            {
                var index = _header.GetColumnIndexForColumnName(columnName);

                return index < _values.Count ? _values[index] : null;
            }
        }

        public SheetDataRow(SheetHeader header, RowHash hash, IList<object> values)
        {
            _header = header;
            _hash = hash;
            _values = values;
        }
    }
}