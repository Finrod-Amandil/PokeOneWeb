namespace PokeOneWeb.DataSync.GoogleSpreadsheet.Import
{
    public interface ISheetNameHelper
    {
        ISheetRepository GetSheetRepositoryForSheetName(string sheetName);
    }
}
