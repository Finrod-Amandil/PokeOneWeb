using Microsoft.EntityFrameworkCore;
using PokeOneWeb.Data;
using PokeOneWeb.Data.Entities;

namespace PokeOneWeb.Services.GoogleSpreadsheet.Import.Impl.Sheets.ElementalTypes
{
    public class ElementalTypeSheetRepository : SheetRepository<ElementalTypeSheetDto, ElementalType>
    {
        public ElementalTypeSheetRepository(
            ApplicationDbContext dbContext,
            ISheetRowParser<ElementalTypeSheetDto> parser,
            ISpreadsheetEntityMapper<ElementalTypeSheetDto, ElementalType> mapper,
            ISpreadsheetImportReporter reporter) : base(dbContext, parser, mapper, reporter) { }

        protected override DbSet<ElementalType> DbSet => DbContext.ElementalTypes;

        protected override Entity Entity => Entity.ElementalType;
    }
}
