using PokeOneWeb.Data.Entities;

namespace PokeOneWeb.Data.Repositories
{
    public interface IImportSheetRepository : IRepository<ImportSheet>
    {
        public ImportSheet FindBySpreadsheetIdAndSheetName(string spreadsheetId, string sheetName);
    }
}