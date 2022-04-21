using Microsoft.EntityFrameworkCore;
using PokeOneWeb.Data;
using PokeOneWeb.Data.Entities;

namespace PokeOneWeb.DataSync.GoogleSpreadsheet.Import.Impl.Sheets.Seasons
{
    public class SeasonXiSheetRepository : XSheetRepository<SeasonSheetDto, Season>
    {
        public SeasonXiSheetRepository(
            ApplicationDbContext dbContext,
            XISheetRowParser<SeasonSheetDto> parser,
            XISpreadsheetEntityMapper<SeasonSheetDto, Season> mapper,
            ISpreadsheetImportReporter reporter) : base(dbContext, parser, mapper, reporter)
        {
        }

        protected override DbSet<Season> DbSet => DbContext.Seasons;

        protected override Entity Entity => Entity.Season;
    }
}