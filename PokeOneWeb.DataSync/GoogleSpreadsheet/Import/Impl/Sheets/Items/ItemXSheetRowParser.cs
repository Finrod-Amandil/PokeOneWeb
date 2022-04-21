using System;
using System.Collections.Generic;

namespace PokeOneWeb.DataSync.GoogleSpreadsheet.Import.Impl.Sheets.Items
{
    public class ItemXSheetRowParser : XSheetRowParser<ItemSheetDto>
    {
        protected override int RequiredValueCount => 6;

        protected override List<Action<ItemSheetDto, object>> MappingDelegates => new()
        {
            (dto, value) => dto.Name = ParseAsNonEmptyString(value),
            (dto, value) => dto.IsAvailable = ParseAsBoolean(value),
            (dto, value) => dto.DoInclude = ParseAsBoolean(value),
            (dto, value) => dto.ResourceName = ParseAsNonEmptyString(value),
            (dto, value) => dto.SortIndex = ParseAsInt(value),
            (dto, value) => dto.BagCategoryName = ParseAsNonEmptyString(value),
            (dto, value) => dto.PokeOneItemId = ParseAsInt(value, 0),
            (dto, value) => dto.Description = ParseAsString(value),
            (dto, value) => dto.Effect = ParseAsString(value),
            (dto, value) => dto.SpriteName = ParseAsString(value)
        };
    }
}