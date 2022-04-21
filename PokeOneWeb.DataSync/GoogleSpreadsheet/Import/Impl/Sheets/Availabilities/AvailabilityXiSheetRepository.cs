using Microsoft.EntityFrameworkCore;
using PokeOneWeb.Data;
using PokeOneWeb.Data.Entities;

namespace PokeOneWeb.DataSync.GoogleSpreadsheet.Import.Impl.Sheets.Availabilities
{
    public class AvailabilityXiSheetRepository : XSheetRepository<AvailabilitySheetDto, PokemonAvailability>
    {
        public AvailabilityXiSheetRepository(
            ApplicationDbContext dbContext,
            XISheetRowParser<AvailabilitySheetDto> parser,
            XISpreadsheetEntityMapper<AvailabilitySheetDto, PokemonAvailability> mapper,
            ISpreadsheetImportReporter reporter) : base(dbContext, parser, mapper, reporter)
        {
        }

        protected override DbSet<PokemonAvailability> DbSet => DbContext.PokemonAvailabilities;

        protected override Entity Entity => Entity.PokemonAvailability;
    }
}