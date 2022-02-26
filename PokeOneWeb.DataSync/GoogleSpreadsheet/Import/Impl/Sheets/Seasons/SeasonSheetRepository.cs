using Microsoft.EntityFrameworkCore;
using PokeOneWeb.Data;
using PokeOneWeb.Data.Entities;

namespace PokeOneWeb.DataSync.GoogleSpreadsheet.Import.Impl.Sheets.Seasons
{
    public class SeasonSheetRepository : SheetRepository<SeasonSheetDto, Season>
    {
        public SeasonSheetRepository(
            ApplicationDbContext dbContext,
            ISheetRowParser<SeasonSheetDto> parser,
            ISpreadsheetEntityMapper<SeasonSheetDto, Season> mapper,
            ISpreadsheetImportReporter reporter) : base(dbContext, parser, mapper, reporter) { }

        protected override DbSet<Season> DbSet => DbContext.Seasons;

        protected override Entity Entity => Entity.Season;
    }
}
