using System;
using System.Collections.Generic;

namespace PokeOneWeb.DataSync.GoogleSpreadsheet.Import.Impl.Sheets.Natures
{
    public class NatureSheetRowParser : SheetRowParser<NatureSheetDto>
    {
        protected override int RequiredValueCount => 6;

        protected override List<Action<NatureSheetDto, object>> MappingDelegates => new()
        {
            (dto, value) => dto.Name = ParseAsNonEmptyString(value),
            (dto, value) => dto.Attack = ParseAsInt(value),
            (dto, value) => dto.SpecialAttack = ParseAsInt(value),
            (dto, value) => dto.Defense = ParseAsInt(value),
            (dto, value) => dto.SpecialDefense = ParseAsInt(value),
            (dto, value) => dto.Speed = ParseAsInt(value)
        };
    }
}
