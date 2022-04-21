using Microsoft.EntityFrameworkCore;
using PokeOneWeb.Data;
using PokeOneWeb.Data.Entities;

namespace PokeOneWeb.DataSync.GoogleSpreadsheet.Import.Impl.Sheets.Events
{
    public class EventXiSheetRepository : XSheetRepository<EventSheetDto, Event>
    {
        public EventXiSheetRepository(
            ApplicationDbContext dbContext,
            XISheetRowParser<EventSheetDto> parser,
            XISpreadsheetEntityMapper<EventSheetDto, Event> mapper,
            ISpreadsheetImportReporter reporter) : base(dbContext, parser, mapper, reporter)
        {
        }

        protected override DbSet<Event> DbSet => DbContext.Events;

        protected override Entity Entity => Entity.Event;
    }
}