using Microsoft.EntityFrameworkCore;
using PokeOneWeb.Data;
using PokeOneWeb.Data.Entities;

namespace PokeOneWeb.DataSync.GoogleSpreadsheet.Import.Impl.Sheets.ElementalTypes
{
    public class ElementalTypeXiSheetRepository : XSheetRepository<ElementalTypeSheetDto, ElementalType>
    {
        public ElementalTypeXiSheetRepository(
            ApplicationDbContext dbContext,
            XISheetRowParser<ElementalTypeSheetDto> parser,
            XISpreadsheetEntityMapper<ElementalTypeSheetDto, ElementalType> mapper,
            ISpreadsheetImportReporter reporter) : base(dbContext, parser, mapper, reporter)
        {
        }

        protected override DbSet<ElementalType> DbSet => DbContext.ElementalTypes;

        protected override Entity Entity => Entity.ElementalType;
    }
}