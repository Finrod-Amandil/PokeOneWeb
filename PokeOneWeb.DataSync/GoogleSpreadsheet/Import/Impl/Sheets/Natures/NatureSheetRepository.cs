using Microsoft.EntityFrameworkCore;
using PokeOneWeb.Data;
using PokeOneWeb.Data.Entities;

namespace PokeOneWeb.DataSync.GoogleSpreadsheet.Import.Impl.Sheets.Natures
{
    public class NatureSheetRepository : SheetRepository<NatureSheetDto, Nature>
    {
        public NatureSheetRepository(
            ApplicationDbContext dbContext,
            ISheetRowParser<NatureSheetDto> parser,
            ISpreadsheetEntityMapper<NatureSheetDto, Nature> mapper,
            ISpreadsheetImportReporter reporter) : base(dbContext, parser, mapper, reporter)
        {
        }

        protected override DbSet<Nature> DbSet => DbContext.Natures;

        protected override Entity Entity => Entity.Nature;
    }
}