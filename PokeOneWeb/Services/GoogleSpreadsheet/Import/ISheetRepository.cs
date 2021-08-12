using PokeOneWeb.Services.GoogleSpreadsheet.Import.Impl;
using System.Collections.Generic;
using PokeOneWeb.Data.Entities;

namespace PokeOneWeb.Services.GoogleSpreadsheet.Import
{
    public interface ISheetRepository
    {
        public List<RowHash> ReadDbHashes(ImportSheet sheet);

        public int Delete(List<RowHash> hashes);

        public int Insert(Dictionary<RowHash, List<object>> rowData);

        public int Update(Dictionary<RowHash, List<object>> rowData);
    }
}
