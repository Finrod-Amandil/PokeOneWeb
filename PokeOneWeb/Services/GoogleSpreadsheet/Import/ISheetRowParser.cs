using System.Collections.Generic;

namespace PokeOneWeb.Services.GoogleSpreadsheet.Import
{
    public interface ISheetRowParser<out T> where T : ISpreadsheetEntityDto
    {
        T ReadRow(List<object> values);
    }
}
