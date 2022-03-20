using System;
using System.Collections.Generic;

namespace PokeOneWeb.DataSync.GoogleSpreadsheet.Import.Impl.Sheets.PvpTiers
{
    public class PvpTierSheetRowParser : SheetRowParser<PvpTierSheetDto>
    {
        protected override int RequiredValueCount => 2;

        protected override List<Action<PvpTierSheetDto, object>> MappingDelegates => new()
        {
            (dto, value) => dto.Name = ParseAsNonEmptyString(value),
            (dto, value) => dto.SortIndex = ParseAsInt(value)
        };
    }
}
