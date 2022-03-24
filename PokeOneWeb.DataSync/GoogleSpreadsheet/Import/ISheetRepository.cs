using PokeOneWeb.Data.Entities;
using PokeOneWeb.DataSync.GoogleSpreadsheet.Import.Impl;
using System.Collections.Generic;

namespace PokeOneWeb.DataSync.GoogleSpreadsheet.Import
{
    public interface ISheetRepository
    {
        /// <summary>
        /// Gets the hashes of the currently available records in the database for a given sheet.
        /// </summary>
        public List<RowHash> ReadDbHashes(ImportSheet sheet);

        /// <summary>
        /// Deletes the records that correspond to the given hashes from the database.
        /// </summary>
        public int Delete(List<RowHash> hashes);

        /// <summary>
        /// Inserts the given data into the database.
        /// </summary>
        public int Insert(Dictionary<RowHash, List<object>> rowData);

        /// <summary>
        /// Updates all fields of the existing records (represented by their hashes) in the database.
        /// </summary>
        public int Update(Dictionary<RowHash, List<object>> rowData);
    }
}
