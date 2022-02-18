using Microsoft.EntityFrameworkCore;
using PokeOneWeb.Data;
using PokeOneWeb.Data.Entities;

namespace PokeOneWeb.Services.GoogleSpreadsheet.Import.Impl.Sheets.TimesOfDay
{
    public class TimeOfDaySheetRepository : SheetRepository<TimeOfDaySheetDto, TimeOfDay>
    {
        public TimeOfDaySheetRepository(
            ApplicationDbContext dbContext,
            ISheetRowParser<TimeOfDaySheetDto> parser,
            ISpreadsheetEntityMapper<TimeOfDaySheetDto, TimeOfDay> mapper,
            ISpreadsheetImportReporter reporter) : base(dbContext, parser, mapper, reporter) { }

        protected override DbSet<TimeOfDay> DbSet => DbContext.TimesOfDay;

        protected override Entity Entity => Entity.TimeOfDay;
    }
}
