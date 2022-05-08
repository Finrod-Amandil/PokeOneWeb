using PokeOneWeb.Data.Entities;

namespace PokeOneWeb.Data.Repositories
{
    /// <summary>
    /// Specialized repository for interacting with ImportSheets.
    /// </summary>
    public interface IImportSheetRepository : IRepository<ImportSheet>
    {
        /// <summary>
        /// Loads the import spreadsheet that matches the given spreadsheet ID and sheet name.
        /// If no such sheet exists in the data store yet, a new entry is created.
        /// </summary>
        public ImportSheet FindBySpreadsheetIdAndSheetName(string spreadsheetId, string sheetName);
    }
}