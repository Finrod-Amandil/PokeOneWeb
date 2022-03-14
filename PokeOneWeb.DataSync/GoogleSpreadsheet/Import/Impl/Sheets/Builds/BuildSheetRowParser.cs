namespace PokeOneWeb.DataSync.GoogleSpreadsheet.Import.Impl.Sheets.Builds
{
    public class BuildSheetRowParser : SheetRowParser<BuildSheetDto>
    {
        protected override int RequiredValueCount => 1;

        protected override List<Action<BuildSheetDto, object>> MappingDelegates => new()
        {
            (dto, value) => dto.PokemonVarietyName = ParseAsNonEmptyString(value),
            (dto, value) => dto.BuildName = ParseAsString(value),
            (dto, value) => dto.BuildDescription = ParseAsString(value),
            (dto, value) => dto.Move1 = ParseAsString(value),
            (dto, value) => dto.Move2 = ParseAsString(value),
            (dto, value) => dto.Move3 = ParseAsString(value),
            (dto, value) => dto.Move4 = ParseAsString(value),
            (dto, value) => dto.Item = ParseAsString(value),
            (dto, value) => dto.Nature = ParseAsString(value),
            (dto, value) => dto.Ability = ParseAsString(value),
            (dto, value) => dto.EvDistribution = ParseAsString(value),
        };
    }
}
