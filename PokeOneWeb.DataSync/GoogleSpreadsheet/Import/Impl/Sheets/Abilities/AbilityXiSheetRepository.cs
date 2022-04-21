using Microsoft.EntityFrameworkCore;
using PokeOneWeb.Data;
using PokeOneWeb.Data.Entities;

namespace PokeOneWeb.DataSync.GoogleSpreadsheet.Import.Impl.Sheets.Abilities
{
    public class AbilityXiSheetRepository : XSheetRepository<AbilitySheetDto, Ability>
    {
        public AbilityXiSheetRepository(
            ApplicationDbContext dbContext,
            XISheetRowParser<AbilitySheetDto> parser,
            XISpreadsheetEntityMapper<AbilitySheetDto, Ability> mapper,
            ISpreadsheetImportReporter reporter) : base(dbContext, parser, mapper, reporter)
        {
        }

        protected override DbSet<Ability> DbSet => DbContext.Abilities;

        protected override Entity Entity => Entity.Ability;
    }
}