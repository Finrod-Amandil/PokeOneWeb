using System;
using System.Collections.Generic;

namespace PokeOneWeb.DataSync.GoogleSpreadsheet.Import.Impl.Sheets.MoveTutors
{
    public class MoveTutorSheetRowParser : SheetRowParser<MoveTutorSheetDto>
    {
        protected override int RequiredValueCount => 2;

        protected override List<Action<MoveTutorSheetDto, object>> MappingDelegates => new()
        {
            (dto, value) => dto.Name = ParseAsNonEmptyString(value),
            (dto, value) => dto.LocationName = ParseAsNonEmptyString(value),
            (dto, value) => dto.PlacementDescription = ParseAsString(value),
        };
    }
}