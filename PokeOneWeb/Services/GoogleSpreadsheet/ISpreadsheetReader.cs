using Google.Apis.Sheets.v4.Data;
using System.Collections.Generic;

namespace PokeOneWeb.Services.GoogleSpreadsheet
{
    public interface ISpreadsheetReader<out T> where T : ISpreadsheetDto
    {
        IEnumerable<T> Read(Spreadsheet spreadsheet, string sheetPrefix);
    }
}
