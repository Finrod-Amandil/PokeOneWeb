using System;
using System.Collections.Generic;

namespace PokeOneWeb.DataSync.GoogleSpreadsheet.Import.Impl.Sheets.TimesOfDay
{
    public class TimeOfDayXSheetRowParser : XSheetRowParser<TimeOfDaySheetDto>
    {
        protected override int RequiredValueCount => 3;

        protected override List<Action<TimeOfDaySheetDto, object>> MappingDelegates => new()
        {
            (dto, value) => dto.SortIndex = ParseAsInt(value),
            (dto, value) => dto.Name = ParseAsNonEmptyString(value),
            (dto, value) => dto.Abbreviation = ParseAsNonEmptyString(value),
            (dto, value) => dto.Color = ParseAsString(value)
        };
    }
}