using Microsoft.EntityFrameworkCore;
using PokeOneWeb.Data;
using PokeOneWeb.Data.Entities;

namespace PokeOneWeb.DataSync.GoogleSpreadsheet.Import.Impl.Sheets.TimesOfDay
{
    public class TimeOfDayXiSheetRepository : XSheetRepository<TimeOfDaySheetDto, TimeOfDay>
    {
        public TimeOfDayXiSheetRepository(
            ApplicationDbContext dbContext,
            XISheetRowParser<TimeOfDaySheetDto> parser,
            XISpreadsheetEntityMapper<TimeOfDaySheetDto, TimeOfDay> mapper,
            ISpreadsheetImportReporter reporter) : base(dbContext, parser, mapper, reporter)
        {
        }

        protected override DbSet<TimeOfDay> DbSet => DbContext.TimesOfDay;

        protected override Entity Entity => Entity.TimeOfDay;
    }
}