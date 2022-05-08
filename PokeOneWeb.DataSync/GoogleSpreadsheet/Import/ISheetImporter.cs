using System.Threading.Tasks;

namespace PokeOneWeb.DataSync.GoogleSpreadsheet.Import
{
    public interface ISheetImporter
    {
        Task ImportSheet(string spreadsheetId, string sheetName);
    }
}