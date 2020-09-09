using Google.Apis.Sheets.v4.Data;

namespace PokeOneWeb.Services.GoogleSpreadsheet
{
    public interface ISpreadsheetLoader
    {
        Spreadsheet LoadSpreadsheet(string spreadsheetId);
    }
}
