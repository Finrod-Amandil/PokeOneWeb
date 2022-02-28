namespace PokeOneWeb.DataSync.GoogleSpreadsheet.Import
{
    public interface ISheetNameHelper
    {
        /// <summary>
        /// Maps the string name of a sheet to the corresponding repository.
        /// </summary>
        ISheetRepository GetSheetRepositoryForSheetName(string sheetName);
    }
}
