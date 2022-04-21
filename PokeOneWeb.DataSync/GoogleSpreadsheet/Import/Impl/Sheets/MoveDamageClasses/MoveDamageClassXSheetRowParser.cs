using System;
using System.Collections.Generic;

namespace PokeOneWeb.DataSync.GoogleSpreadsheet.Import.Impl.Sheets.MoveDamageClasses
{
    public class MoveDamageClassXSheetRowParser : XSheetRowParser<MoveDamageClassSheetDto>
    {
        protected override int RequiredValueCount => 1;

        protected override List<Action<MoveDamageClassSheetDto, object>> MappingDelegates => new()
        {
            (dto, value) => dto.Name = ParseAsNonEmptyString(value)
        };
    }
}