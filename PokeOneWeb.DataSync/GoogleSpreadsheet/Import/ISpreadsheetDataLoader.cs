using System.Collections.Generic;
using System.Threading.Tasks;
using PokeOneWeb.Data;
using PokeOneWeb.Data.Entities;
using PokeOneWeb.DataSync.GoogleSpreadsheet.DataTypes;

namespace PokeOneWeb.DataSync.GoogleSpreadsheet.Import
{
    public interface ISpreadsheetDataLoader
    {
        /// <summary>
        /// Loads the list of sheets to be imported from the given spreadsheet.
        /// </summary>
        Task<List<Sheet>> LoadSheetList(string sheetListSpreadsheetId, string sheetListSheetName);

        /// <summary>
        /// Loads the sheet hash, hashing all the data in the sheet, from the given
        /// spreadsheet.
        /// </summary>
        Task<string> LoadSheetHash(string spreadsheetId, string sheetName);

        /// <summary>
        /// Loads all ID hashes and content hashes of the given sheet.
        /// </summary>
        Task<List<RowHash>> LoadHashes(string spreadsheetId, string sheetName, int sheetId);

        /// <summary>
        /// Loads the data of those rows corresponding to the given selected id hashes. The original,
        /// ordered list of all id hashes is required in order to associate the hashes with the row
        /// indexes.
        /// </summary>
        Task<List<SheetDataRow>> LoadDataRows(ImportSheet sheet, List<string> selectedIdHashes, List<string> allIdHashes);
    }
}