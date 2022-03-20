using System;
using System.Collections.Generic;

namespace PokeOneWeb.DataSync.GoogleSpreadsheet.Import.Impl.Sheets.Availabilities
{
    public class AvailabilitySheetRowParser : SheetRowParser<AvailabilitySheetDto>
    {
        protected override int RequiredValueCount => 2;

        protected override List<Action<AvailabilitySheetDto, object>> MappingDelegates => new()
        {
            (dto, value) => dto.Name = ParseAsNonEmptyString(value),
            (dto, value) => dto.Description = ParseAsNonEmptyString(value)
        };
    }
}
