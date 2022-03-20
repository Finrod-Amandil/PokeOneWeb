namespace PokeOneWeb.DataSync.GoogleSpreadsheet.Import.Impl.Sheets.Regions
{
    public class RegionSheetRowParser : SheetRowParser<RegionSheetDto>
    {
        protected override int RequiredValueCount => 2;

        protected override List<Action<RegionSheetDto, object>> MappingDelegates => new()
        {
            (dto, value) => dto.Name = ParseAsNonEmptyString(value),
            (dto, value) => dto.ResourceName = ParseAsNonEmptyString(value),
            (dto, value) => dto.Color = ParseAsString(value),
            (dto, value) => dto.IsEventRegion = ParseAsBoolean(value, false),
            (dto, value) => dto.EventName = ParseAsString(value),
        };
    }
}
