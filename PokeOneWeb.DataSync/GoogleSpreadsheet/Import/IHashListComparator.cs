using System.Collections.Generic;
using PokeOneWeb.Data;
using PokeOneWeb.DataSync.GoogleSpreadsheet.Import.Impl.DataTypes;

namespace PokeOneWeb.DataSync.GoogleSpreadsheet.Import
{
    public interface IHashListComparator
    {
        /// <summary>
        /// Compares two lists of hashes and determines which records (represented by their hashes) need
        /// to be added, updated or deleted.
        /// </summary>
        HashListComparisonResult CompareHashLists(List<RowHash> sheetHashes, List<RowHash> dbHashesOrdered);
    }
}