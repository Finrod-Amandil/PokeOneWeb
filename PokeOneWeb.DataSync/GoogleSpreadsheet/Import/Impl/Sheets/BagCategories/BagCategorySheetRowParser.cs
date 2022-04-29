using System;
using System.Collections.Generic;

namespace PokeOneWeb.DataSync.GoogleSpreadsheet.Import.Impl.Sheets.BagCategories
{
    public class BagCategorySheetRowParser : SheetRowParser<BagCategorySheetDto>
    {
        protected override int RequiredValueCount => 2;

        protected override List<Action<BagCategorySheetDto, object>> MappingDelegates => new()
        {
            (dto, value) => dto.Name = ParseAsNonEmptyString(value),
            (dto, value) => dto.SortIndex = ParseAsInt(value)
        };
    }
}