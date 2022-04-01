using PokeOneWeb.Data;
using PokeOneWeb.Data.Entities;

namespace PokeOneWeb.DataSync.GoogleSpreadsheet.Import.Impl.Sheets.Events
{
    public class EventMapper : SpreadsheetEntityMapper<EventSheetDto, Event>
    {
        public EventMapper(ISpreadsheetImportReporter reporter) : base(reporter)
        {
        }

        protected override Entity Entity => Entity.Event;

        protected override bool IsValid(EventSheetDto dto)
        {
            return !string.IsNullOrWhiteSpace(dto.Name);
        }

        protected override string GetUniqueName(EventSheetDto dto)
        {
            return dto.Name;
        }

        protected override Event MapEntity(EventSheetDto dto, RowHash rowHash, Event eventEntity = null)
        {
            eventEntity ??= new Event();

            eventEntity.IdHash = rowHash.IdHash;
            eventEntity.Hash = rowHash.ContentHash;
            eventEntity.ImportSheetId = rowHash.ImportSheetId;
            eventEntity.Name = dto.Name;
            eventEntity.StartDate = dto.StartDate;
            eventEntity.EndDate = dto.EndDate;

            return eventEntity;
        }
    }
}