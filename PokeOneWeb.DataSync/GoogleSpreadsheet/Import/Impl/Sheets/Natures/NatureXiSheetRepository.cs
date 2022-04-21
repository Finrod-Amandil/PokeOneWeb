using Microsoft.EntityFrameworkCore;
using PokeOneWeb.Data;
using PokeOneWeb.Data.Entities;

namespace PokeOneWeb.DataSync.GoogleSpreadsheet.Import.Impl.Sheets.Natures
{
    public class NatureXiSheetRepository : XSheetRepository<NatureSheetDto, Nature>
    {
        public NatureXiSheetRepository(
            ApplicationDbContext dbContext,
            XISheetRowParser<NatureSheetDto> parser,
            XISpreadsheetEntityMapper<NatureSheetDto, Nature> mapper,
            ISpreadsheetImportReporter reporter) : base(dbContext, parser, mapper, reporter)
        {
        }

        protected override DbSet<Nature> DbSet => DbContext.Natures;

        protected override Entity Entity => Entity.Nature;
    }
}