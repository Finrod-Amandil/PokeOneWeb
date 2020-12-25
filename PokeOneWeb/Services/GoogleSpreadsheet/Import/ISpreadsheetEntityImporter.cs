using Google.Apis.Sheets.v4.Data;

namespace PokeOneWeb.Services.GoogleSpreadsheet.Import
{
    public interface ISpreadsheetEntityImporter<S, T> where S : ISpreadsheetEntityDto where T : class
    {
        void ImportFromSpreadsheet(Spreadsheet spreadsheet);
    }
}