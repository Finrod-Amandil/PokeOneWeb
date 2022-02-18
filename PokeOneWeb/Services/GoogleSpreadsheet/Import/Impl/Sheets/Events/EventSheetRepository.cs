using Microsoft.EntityFrameworkCore;
using PokeOneWeb.Data;
using PokeOneWeb.Data.Entities;

namespace PokeOneWeb.Services.GoogleSpreadsheet.Import.Impl.Sheets.Events
{
    public class EventSheetRepository : SheetRepository<EventSheetDto, Event>
    {
        public EventSheetRepository(
            ApplicationDbContext dbContext,
            ISheetRowParser<EventSheetDto> parser,
            ISpreadsheetEntityMapper<EventSheetDto, Event> mapper,
            ISpreadsheetImportReporter reporter) : base(dbContext, parser, mapper, reporter) { }

        protected override DbSet<Event> DbSet => DbContext.Events;

        protected override Entity Entity => Entity.Event;
    }
}
