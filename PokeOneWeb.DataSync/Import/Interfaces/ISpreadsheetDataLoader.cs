using System.Collections.Generic;
using System.Threading.Tasks;
using PokeOneWeb.Data.Entities;
using PokeOneWeb.DataSync.GoogleSpreadsheet.DataTypes;

namespace PokeOneWeb.DataSync.Import.Interfaces
{
    public interface ISpreadsheetDataLoader
    {
        /// <summary>
        /// Loads the list of sheets to be imported from the given spreadsheet.
        /// </summary>
        Task<List<Sheet>> LoadSheetList(string sheetListSpreadsheetId, string sheetListSheetName);

        /// <summary>
        /// Loads the data of those rows corresponding to the given selected id hashes. The original,
        /// ordered list of all id hashes is required in order to associate the hashes with the row
        /// indexes.
        /// </summary>
        Task<List<SheetDataRow>> LoadSheetRows(ImportSheet sheet);
    }
}