﻿namespace PokeOneWeb.DataSync.GoogleSpreadsheet.Import.Impl.Sheets.Events
{
    public class EventSheetRowParser : SheetRowParser<EventSheetDto>
    {
        protected override int RequiredValueCount => 1;

        protected override List<Action<EventSheetDto, object>> MappingDelegates => new()
        {
            (dto, value) => dto.Name = ParseAsNonEmptyString(value),
            (dto, value) => dto.StartDate = ParseAsDate(value),
            (dto, value) => dto.EndDate = ParseAsDate(value)
        };
    }
}
