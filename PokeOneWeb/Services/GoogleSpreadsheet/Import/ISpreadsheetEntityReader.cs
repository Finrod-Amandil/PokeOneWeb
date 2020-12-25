using System.Collections.Generic;
using Google.Apis.Sheets.v4.Data;

namespace PokeOneWeb.Services.GoogleSpreadsheet.Import
{
    public interface ISpreadsheetEntityReader<out T> where T : ISpreadsheetEntityDto
    {
        IEnumerable<T> Read(Spreadsheet spreadsheet, string sheetPrefix);
    }
}
