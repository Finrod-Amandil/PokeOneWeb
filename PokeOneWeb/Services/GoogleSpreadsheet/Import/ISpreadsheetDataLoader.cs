using System.Collections.Generic;
using System.Threading.Tasks;
using PokeOneWeb.Services.GoogleSpreadsheet.Import.Impl;

namespace PokeOneWeb.Services.GoogleSpreadsheet.Import
{
    public interface ISpreadsheetDataLoader
    {
        Task<string> LoadSheetHash(string spreadsheetId, string sheetName);

        Task<List<RowHash>> LoadHashes(string spreadsheetId, string sheetName, int sheetId);

        Task<List<List<object>>> LoadRows(string spreadsheetId, string sheetName, List<int> rows = null);

        Task<List<List<object>>> LoadRange(string spreadsheetId, string sheetName, string range);
    }
}
