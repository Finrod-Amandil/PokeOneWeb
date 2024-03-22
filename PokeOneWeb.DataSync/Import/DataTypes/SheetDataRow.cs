using System;
using System.Collections.Generic;
using PokeOneWeb.Data;

namespace PokeOneWeb.DataSync.GoogleSpreadsheet.DataTypes
{
    /// <summary>
    /// Auxiliary class representing a single row of data from a google sheet, allowing
    /// lookups by column name.
    /// </summary>
    public class SheetDataRow
    {
        private readonly RowHash _hash;
        private readonly Dictionary<string, object> _values = new();

        public string IdHash => _hash.IdHash;

        public string Hash => _hash.Hash;

        public RowHash RowHash => _hash;

        /// <summary>
        /// Finds the value of this row and the given column. Returns
        /// null if no value was found for this column name.
        /// </summary>
        public object this[string columnName] =>
            _values.TryGetValue(columnName, out var value)
                ? value
                : null;

        public SheetDataRow(IList<string> columnNames, RowHash hash, IList<object> values)
        {
            if (columnNames is null)
            {
                throw new ArgumentNullException(nameof(columnNames));
            }

            if (values is null)
            {
                throw new ArgumentNullException(nameof(values));
            }

            _hash = hash ?? throw new ArgumentNullException(nameof(hash));

            if (columnNames.Count < values.Count)
            {
                throw new ArgumentException("Fewer column names were given than values.");
            }

            for (var index = 0; index < values.Count; index++)
            {
                var columnName = columnNames[index];
                if (string.IsNullOrWhiteSpace(columnName))
                {
                    throw new ArgumentException($"An invalid column name was given for column {index}");
                }

                if (_values.ContainsKey(columnName))
                {
                    throw new ArgumentException($"Duplicate column name: {columnName}");
                }

                _values.Add(columnNames[index], values[index]);
            }
        }
    }
}