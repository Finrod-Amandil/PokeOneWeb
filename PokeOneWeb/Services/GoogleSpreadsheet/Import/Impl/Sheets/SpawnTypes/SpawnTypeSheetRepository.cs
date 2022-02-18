using Microsoft.EntityFrameworkCore;
using PokeOneWeb.Data;
using PokeOneWeb.Data.Entities;

namespace PokeOneWeb.Services.GoogleSpreadsheet.Import.Impl.Sheets.SpawnTypes
{
    public class SpawnTypeSheetRepository : SheetRepository<SpawnTypeSheetDto, SpawnType>
    {
        public SpawnTypeSheetRepository(
            ApplicationDbContext dbContext,
            ISheetRowParser<SpawnTypeSheetDto> parser,
            ISpreadsheetEntityMapper<SpawnTypeSheetDto, SpawnType> mapper,
            ISpreadsheetImportReporter reporter) : base(dbContext, parser, mapper, reporter) { }

        protected override DbSet<SpawnType> DbSet => DbContext.SpawnTypes;

        protected override Entity Entity => Entity.SpawnType;
    }
}
