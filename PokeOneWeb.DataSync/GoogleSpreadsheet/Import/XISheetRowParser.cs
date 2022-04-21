using System.Collections.Generic;

namespace PokeOneWeb.DataSync.GoogleSpreadsheet.Import
{
    public interface XISheetRowParser<out T> where T : XISpreadsheetEntityDto
    {
        /// <summary>
        /// Parses the raw values of a row of a spreadsheet into the corresponding DTO.
        /// </summary>
        T ReadRow(List<object> values);
    }
}