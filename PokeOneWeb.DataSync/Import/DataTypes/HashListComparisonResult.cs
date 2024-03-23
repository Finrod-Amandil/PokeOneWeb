using System.Collections.Generic;

namespace PokeOneWeb.DataSync.Import.DataTypes
{
    public class HashListComparisonResult
    {
        public List<string> RowsToInsert { get; set; } = new();

        public List<string> RowsToDelete { get; set; } = new();

        public List<string> RowsToUpdate { get; set; } = new();

        public List<string> DuplicateIdHashes { get; set; } = new();
    }
}