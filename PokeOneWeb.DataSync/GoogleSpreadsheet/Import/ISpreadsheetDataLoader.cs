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
        /// Loads the sheet hash, representing the state of an entire sheet.
        /// </summary>
        Task<string> LoadSheetHash(string spreadsheetId, string sheetName);

        /// <summary>
        /// Loads the row-based ID Hashes and Content Hashes of a sheet.
        /// </summary>
        Task<List<RowHash>> LoadHashes(string spreadsheetId, string sheetName, int sheetId);

        Task<List<SheetDataRow>> LoadDataRows(ImportSheet sheet, List<string> selectedIdHashes, List<string> allIdHashes);

        /// <summary>
        /// Loads the content values of a sheet, of all rows, or of specific rows.
        /// </summary>
        /// <param name="rows">The 0-indicated row indexes of which the data should be loaded.
        /// The first row with content has index 0, header rows do not need to be considered.</param>
        Task<List<List<object>>> LoadRows(string spreadsheetId, string sheetName, List<int> rows = null);

        /// <summary>
        /// Loads the values of a range of cells. Specify the range in "A1" notation, i.e. "A1:D3".
        /// No offset for header rows will be added - the specified range will be treated equal as if
        /// specified in the Google Spreadsheet.
        /// </summary>
        Task<List<List<object>>> LoadRange(string spreadsheetId, string sheetName, string range);
    }
}