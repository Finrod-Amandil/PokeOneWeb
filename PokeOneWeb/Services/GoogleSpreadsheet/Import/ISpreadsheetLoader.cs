using System.Threading.Tasks;
using Google.Apis.Sheets.v4.Data;

namespace PokeOneWeb.Services.GoogleSpreadsheet.Import
{
    public interface ISpreadsheetLoader
    {
        Task<Spreadsheet> LoadSpreadsheet(string spreadsheetId);
    }
}
