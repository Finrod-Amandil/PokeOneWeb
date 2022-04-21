using System.Collections.Generic;

namespace PokeOneWeb.DataSync.GoogleSpreadsheet.Import.Impl.DataTypes
{
    public class HashListComparisonResult
    {
        public List<string> RowsToInsert { get; set; } = new();

        public List<string> RowsToDelete { get; set; } = new();

        public List<string> RowsToUpdate { get; set; } = new();
    }
}