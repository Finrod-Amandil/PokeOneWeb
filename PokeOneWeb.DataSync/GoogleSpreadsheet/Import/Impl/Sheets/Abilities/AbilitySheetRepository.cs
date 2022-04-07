using Microsoft.EntityFrameworkCore;
using PokeOneWeb.Data;
using PokeOneWeb.Data.Entities;

namespace PokeOneWeb.DataSync.GoogleSpreadsheet.Import.Impl.Sheets.Abilities
{
    public class AbilitySheetRepository : SheetRepository<AbilitySheetDto, Ability>
    {
        public AbilitySheetRepository(
            ApplicationDbContext dbContext,
            ISheetRowParser<AbilitySheetDto> parser,
            ISpreadsheetEntityMapper<AbilitySheetDto, Ability> mapper,
            ISpreadsheetImportReporter reporter) : base(dbContext, parser, mapper, reporter)
        {
        }

        protected override DbSet<Ability> DbSet => DbContext.Abilities;

        protected override Entity Entity => Entity.Ability;
    }
}