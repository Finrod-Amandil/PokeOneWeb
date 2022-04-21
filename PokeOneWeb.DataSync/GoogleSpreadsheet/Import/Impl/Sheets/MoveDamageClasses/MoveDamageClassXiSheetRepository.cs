using Microsoft.EntityFrameworkCore;
using PokeOneWeb.Data;
using PokeOneWeb.Data.Entities;

namespace PokeOneWeb.DataSync.GoogleSpreadsheet.Import.Impl.Sheets.MoveDamageClasses
{
    public class MoveDamageClassXiSheetRepository : XSheetRepository<MoveDamageClassSheetDto, MoveDamageClass>
    {
        public MoveDamageClassXiSheetRepository(
            ApplicationDbContext dbContext,
            XISheetRowParser<MoveDamageClassSheetDto> parser,
            XISpreadsheetEntityMapper<MoveDamageClassSheetDto, MoveDamageClass> mapper,
            ISpreadsheetImportReporter reporter) : base(dbContext, parser, mapper, reporter)
        {
        }

        protected override DbSet<MoveDamageClass> DbSet => DbContext.MoveDamageClasses;
        protected override Entity Entity => Entity.MoveDamageClass;
    }
}