using System.Collections.Generic;
using PokeOneWeb.Data;
using PokeOneWeb.Data.Entities;

namespace PokeOneWeb.Services.GoogleSpreadsheet.Import.Impl.MasterData.PvpTiers
{
    public class PvpTierImporter : SpreadsheetEntityImporter<PvpTierDto, PvpTier>
    {
        private readonly ApplicationDbContext _dbContext;

        public PvpTierImporter(
            ISpreadsheetEntityReader<PvpTierDto> reader,
            ISpreadsheetEntityMapper<PvpTierDto, PvpTier> mapper,
            ApplicationDbContext dbContext) : base(reader, mapper)
        {
            _dbContext = dbContext;
        }

        protected override string GetSheetPrefix()
        {
            return Constants.SHEET_PREFIX_PVP_TIERS;
        }

        protected override void WriteToDatabase(IEnumerable<PvpTier> entities)
        {
            _dbContext.PvpTiers.AddRange(entities);
            _dbContext.SaveChanges();
        }
    }
}
