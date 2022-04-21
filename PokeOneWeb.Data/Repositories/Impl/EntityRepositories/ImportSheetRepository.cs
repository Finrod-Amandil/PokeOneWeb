using System.Linq;
using PokeOneWeb.Data.Entities;

namespace PokeOneWeb.Data.Repositories.Impl.EntityRepositories
{
    public class ImportSheetRepository : Repository<ImportSheet>, IImportSheetRepository
    {
        public ImportSheetRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }

        public ImportSheet FindBySpreadsheetIdAndSheetName(string spreadsheetId, string sheetName)
        {
            var sheet = DbContext.ImportSheets
                .SingleOrDefault(s =>
                    s.SpreadsheetId.Equals(spreadsheetId) &&
                    s.SheetName.Equals(sheetName));

            if (sheet is null)
            {
                sheet = new ImportSheet
                {
                    SpreadsheetId = spreadsheetId,
                    SheetName = sheetName,
                    SheetHash = "No successful import yet."
                };

                Insert(sheet);
            }

            return sheet;
        }
    }
}