using System.Collections.Generic;

namespace PokeOneWeb.DataSync.GoogleSpreadsheet.Import.Impl.Sheets.ElementalTypes
{
    public class ElementalTypeSheetRowParser : SheetRowParser<ElementalTypeSheetDto>
    {
        protected override int RequiredValueCount => 1;

        protected override List<Action<ElementalTypeSheetDto, object>> MappingDelegates => new()
        {
            (dto, value) => dto.Name = ParseAsNonEmptyString(value)
        };
    }
}
