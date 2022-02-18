using Microsoft.EntityFrameworkCore;
using PokeOneWeb.Data;
using PokeOneWeb.Data.Entities;

namespace PokeOneWeb.Services.GoogleSpreadsheet.Import.Impl.Sheets.BagCategories
{
    public class BagCategorySheetRepository : SheetRepository<BagCategorySheetDto, BagCategory>
    {
        public BagCategorySheetRepository(
            ApplicationDbContext dbContext,
            ISheetRowParser<BagCategorySheetDto> parser,
            ISpreadsheetEntityMapper<BagCategorySheetDto, BagCategory> mapper,
            ISpreadsheetImportReporter reporter) : base(dbContext, parser, mapper, reporter) { }

        protected override DbSet<BagCategory> DbSet => DbContext.BagCategories;

        protected override Entity Entity => Entity.BagCategory;
    }
}
