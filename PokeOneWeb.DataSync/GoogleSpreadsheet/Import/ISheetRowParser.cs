namespace PokeOneWeb.DataSync.GoogleSpreadsheet.Import
{
    public interface ISheetRowParser<out T> where T : ISpreadsheetEntityDto
    {
        T ReadRow(List<object> values);
    }
}
