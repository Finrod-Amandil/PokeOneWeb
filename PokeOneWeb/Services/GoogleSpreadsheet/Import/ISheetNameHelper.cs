namespace PokeOneWeb.Services.GoogleSpreadsheet.Import
{
    public interface ISheetNameHelper
    {
        ISheetRepository GetSheetRepositoryForSheetName(string sheetName);
    }
}
