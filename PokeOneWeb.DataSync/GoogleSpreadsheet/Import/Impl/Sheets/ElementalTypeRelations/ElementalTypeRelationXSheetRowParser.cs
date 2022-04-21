using System;
using System.Collections.Generic;

namespace PokeOneWeb.DataSync.GoogleSpreadsheet.Import.Impl.Sheets.ElementalTypeRelations
{
    public class ElementalTypeRelationXSheetRowParser : XSheetRowParser<ElementalTypeRelationSheetDto>
    {
        protected override int RequiredValueCount => 3;

        protected override List<Action<ElementalTypeRelationSheetDto, object>> MappingDelegates => new()
        {
            (dto, value) => dto.AttackingTypeName = ParseAsNonEmptyString(value),
            (dto, value) => dto.DefendingTypeName = ParseAsNonEmptyString(value),
            (dto, value) => dto.Effectivity = ParseAsDecimal(value)
        };
    }
}