using Microsoft.EntityFrameworkCore;
using PokeOneWeb.Data;
using PokeOneWeb.Data.Entities;

namespace PokeOneWeb.Services.GoogleSpreadsheet.Import.Impl.Sheets.Availabilities
{
    public class AvailabilitySheetRepository : SheetRepository<AvailabilitySheetDto, PokemonAvailability>
    {
        public AvailabilitySheetRepository(
            ApplicationDbContext dbContext,
            ISheetRowParser<AvailabilitySheetDto> parser,
            ISpreadsheetEntityMapper<AvailabilitySheetDto, PokemonAvailability> mapper,
            ISpreadsheetImportReporter reporter) : base(dbContext, parser, mapper, reporter)
        { }

        protected override DbSet<PokemonAvailability> DbSet => DbContext.PokemonAvailabilities;

        protected override Entity Entity => Entity.PokemonAvailability;
    }
}
