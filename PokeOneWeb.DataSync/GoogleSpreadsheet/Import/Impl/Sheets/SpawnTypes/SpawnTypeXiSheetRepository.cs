using Microsoft.EntityFrameworkCore;
using PokeOneWeb.Data;
using PokeOneWeb.Data.Entities;

namespace PokeOneWeb.DataSync.GoogleSpreadsheet.Import.Impl.Sheets.SpawnTypes
{
    public class SpawnTypeXiSheetRepository : XSheetRepository<SpawnTypeSheetDto, SpawnType>
    {
        public SpawnTypeXiSheetRepository(
            ApplicationDbContext dbContext,
            XISheetRowParser<SpawnTypeSheetDto> parser,
            XISpreadsheetEntityMapper<SpawnTypeSheetDto, SpawnType> mapper,
            ISpreadsheetImportReporter reporter) : base(dbContext, parser, mapper, reporter)
        {
        }

        protected override DbSet<SpawnType> DbSet => DbContext.SpawnTypes;

        protected override Entity Entity => Entity.SpawnType;
    }
}