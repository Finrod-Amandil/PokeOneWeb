using System.Collections.Generic;
using PokeOneWeb.Services.GoogleSpreadsheet.Import.Impl;

namespace PokeOneWeb.Services.GoogleSpreadsheet.Import
{
    public interface IHashListComparator
    {
        HashListComparisonResult CompareHashLists(List<RowHash> sheetHashes, List<RowHash> dbHashes);
    }
}
