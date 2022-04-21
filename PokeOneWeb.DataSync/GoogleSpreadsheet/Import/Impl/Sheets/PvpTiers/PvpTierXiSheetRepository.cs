using Microsoft.EntityFrameworkCore;
using PokeOneWeb.Data;
using PokeOneWeb.Data.Entities;

namespace PokeOneWeb.DataSync.GoogleSpreadsheet.Import.Impl.Sheets.PvpTiers
{
    public class PvpTierXiSheetRepository : XSheetRepository<PvpTierSheetDto, PvpTier>
    {
        public PvpTierXiSheetRepository(
            ApplicationDbContext dbContext,
            XISheetRowParser<PvpTierSheetDto> parser,
            XISpreadsheetEntityMapper<PvpTierSheetDto, PvpTier> mapper,
            ISpreadsheetImportReporter reporter) : base(dbContext, parser, mapper, reporter)
        {
        }

        protected override DbSet<PvpTier> DbSet => DbContext.PvpTiers;

        protected override Entity Entity => Entity.PvpTier;
    }
}