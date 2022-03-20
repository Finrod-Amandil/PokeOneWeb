namespace PokeOneWeb.DataSync.GoogleSpreadsheet.Import.Impl.Sheets.PlacedItems
{
    public class PlacedItemSheetRowParser : SheetRowParser<PlacedItemSheetDto>
    {
        protected override int RequiredValueCount => 5;

        protected override List<Action<PlacedItemSheetDto, object>> MappingDelegates => new()
        {
            (dto, value) => dto.LocationName = ParseAsNonEmptyString(value),
            (dto, value) => dto.Quantity = ParseAsInt(value),
            (dto, value) => dto.ItemName = ParseAsNonEmptyString(value),
            (dto, value) => dto.SortIndex = ParseAsInt(value),
            (dto, value) => dto.Index = ParseAsInt(value),
            (dto, value) => dto.PlacementDescription = ParseAsString(value),
            (dto, value) => dto.IsHidden = ParseAsBoolean(value),
            (dto, value) => dto.IsConfirmed = ParseAsBoolean(value),
            (dto, value) => dto.Requirements = ParseAsString(value),
            (dto, value) => dto.ScreenshotName = ParseAsString(value),
            (dto, value) => dto.Notes = ParseAsString(value),
        };
    }
}
