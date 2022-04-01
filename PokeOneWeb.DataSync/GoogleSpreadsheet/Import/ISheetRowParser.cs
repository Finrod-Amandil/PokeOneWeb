using System.Collections.Generic;

namespace PokeOneWeb.DataSync.GoogleSpreadsheet.Import
{
    public interface ISheetRowParser<out T> where T : ISpreadsheetEntityDto
    {
        /// <summary>
        /// Parses the raw values of a row of a spreadsheet into the corresponding DTO.
        /// </summary>
        T ReadRow(List<object> values);
    }
}