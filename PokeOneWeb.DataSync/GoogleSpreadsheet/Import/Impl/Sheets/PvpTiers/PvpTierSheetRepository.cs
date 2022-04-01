using Microsoft.EntityFrameworkCore;
using PokeOneWeb.Data;
using PokeOneWeb.Data.Entities;

namespace PokeOneWeb.DataSync.GoogleSpreadsheet.Import.Impl.Sheets.PvpTiers
{
    public class PvpTierSheetRepository : SheetRepository<PvpTierSheetDto, PvpTier>
    {
        public PvpTierSheetRepository(
            ApplicationDbContext dbContext,
            ISheetRowParser<PvpTierSheetDto> parser,
            ISpreadsheetEntityMapper<PvpTierSheetDto, PvpTier> mapper,
            ISpreadsheetImportReporter reporter) : base(dbContext, parser, mapper, reporter)
        {
        }

        protected override DbSet<PvpTier> DbSet => DbContext.PvpTiers;

        protected override Entity Entity => Entity.PvpTier;
    }
}