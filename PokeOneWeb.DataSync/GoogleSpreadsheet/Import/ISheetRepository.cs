using PokeOneWeb.Data.Entities;
using PokeOneWeb.DataSync.GoogleSpreadsheet.Import.Impl;

namespace PokeOneWeb.DataSync.GoogleSpreadsheet.Import
{
    public interface ISheetRepository
    {
        public List<RowHash> ReadDbHashes(ImportSheet sheet);

        public int Delete(List<RowHash> hashes);

        public int Insert(Dictionary<RowHash, List<object>> rowData);

        public int Update(Dictionary<RowHash, List<object>> rowData);
    }
}
