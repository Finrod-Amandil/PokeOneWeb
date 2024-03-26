using System.Collections.Generic;
using PokeOneWeb.Data;
using PokeOneWeb.DataSync.Import.DataTypes;

namespace PokeOneWeb.DataSync.Import.Interfaces
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