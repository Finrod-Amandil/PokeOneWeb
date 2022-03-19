namespace PokeOneWeb.DataSync.GoogleSpreadsheet.Import.Impl.Sheets.Locations
{
    public class LocationSheetRowParser : SheetRowParser<LocationSheetDto>
    {
        protected override int RequiredValueCount => 6;

        protected override List<Action<LocationSheetDto, object>> MappingDelegates => new()
        {
            (dto, value) => dto.RegionName = ParseAsNonEmptyString(value),
            (dto, value) => dto.LocationGroupName = ParseAsNonEmptyString(value),
            (dto, value) => dto.ResourceName = ParseAsNonEmptyString(value),
            (dto, value) => dto.LocationName = ParseAsNonEmptyString(value),
            (dto, value) => dto.SortIndex = ParseAsInt(value),
            (dto, value) => dto.IsDiscoverable = ParseAsBoolean(value),
            (dto, value) => dto.Notes = ParseAsString(value),
        };
    }
}
