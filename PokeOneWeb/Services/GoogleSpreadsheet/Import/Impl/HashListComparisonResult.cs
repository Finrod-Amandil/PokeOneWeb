using System.Collections.Generic;

namespace PokeOneWeb.Services.GoogleSpreadsheet.Import.Impl
{
    public class HashListComparisonResult
    {
        public List<RowHash> RowsToInsert { get; set; } = new();
        public List<RowHash> RowsToDelete { get; set; } = new();
        public List<RowHash> RowsToUpdate { get; set; } = new();
    }
}
