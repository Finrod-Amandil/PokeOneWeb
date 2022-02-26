using PokeOneWeb.DataSync.GoogleSpreadsheet.Import.Impl;

namespace PokeOneWeb.DataSync.GoogleSpreadsheet.Import
{
    public interface IHashListComparator
    {
        HashListComparisonResult CompareHashLists(List<RowHash> sheetHashes, List<RowHash> dbHashesOrdered);
    }
}
