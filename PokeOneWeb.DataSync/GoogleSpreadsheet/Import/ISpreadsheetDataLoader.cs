using System.Collections.Generic;
using System.Threading.Tasks;
using PokeOneWeb.Data;
using PokeOneWeb.Data.Entities;
using PokeOneWeb.DataSync.GoogleSpreadsheet.DataTypes;

namespace PokeOneWeb.DataSync.GoogleSpreadsheet.Import
{
    public interface ISpreadsheetDataLoader
    {
        Task<List<Sheet>> LoadSheetList(string sheetListSpreadsheetId, string sheetListSheetName);

        Task<string> LoadSheetHash(string spreadsheetId, string sheetName);

        Task<List<RowHash>> LoadHashes(string spreadsheetId, string sheetName, int sheetId);

        Task<List<SheetDataRow>> LoadDataRows(ImportSheet sheet, List<string> selectedIdHashes, List<string> allIdHashes);
    }
}