using System;
using System.Collections.Generic;

namespace PokeOneWeb.DataSync.GoogleSpreadsheet.Import.Impl.Sheets.SeasonTimesOfDay
{
    public class SeasonTimeOfDaySheetRowParser : SheetRowParser<SeasonTimeOfDaySheetDto>
    {
        protected override int RequiredValueCount => 4;

        protected override List<Action<SeasonTimeOfDaySheetDto, object>> MappingDelegates => new()
        {
            (dto, value) => dto.SeasonName = ParseAsNonEmptyString(value),
            (dto, value) => dto.TimeOfDayName = ParseAsNonEmptyString(value),
            (dto, value) => dto.StartHour = ParseAsInt(value),
            (dto, value) => dto.EndHour = ParseAsInt(value),
        };
    }
}
