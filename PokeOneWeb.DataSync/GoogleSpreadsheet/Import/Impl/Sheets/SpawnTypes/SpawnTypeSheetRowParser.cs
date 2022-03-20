namespace PokeOneWeb.DataSync.GoogleSpreadsheet.Import.Impl.Sheets.SpawnTypes
{
    public class SpawnTypeSheetRowParser : SheetRowParser<SpawnTypeSheetDto>
    {
        protected override int RequiredValueCount => 5;

        protected override List<Action<SpawnTypeSheetDto, object>> MappingDelegates => new()
        {
            (dto, value) => dto.Name = ParseAsNonEmptyString(value),
            (dto, value) => dto.SortIndex = ParseAsInt(value),
            (dto, value) => dto.IsSyncable = ParseAsBoolean(value),
            (dto, value) => dto.IsInfinite = ParseAsBoolean(value),
            (dto, value) => dto.Color = ParseAsNonEmptyString(value)
        };
    }
}
