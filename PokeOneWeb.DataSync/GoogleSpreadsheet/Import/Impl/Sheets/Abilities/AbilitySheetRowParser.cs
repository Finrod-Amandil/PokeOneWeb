namespace PokeOneWeb.DataSync.GoogleSpreadsheet.Import.Impl.Sheets.Abilities
{
    public class AbilitySheetRowParser : SheetRowParser<AbilitySheetDto>
    {
        protected override int RequiredValueCount => 1;

        protected override List<Action<AbilitySheetDto, object>> MappingDelegates => new()
        {
            (dto, value) => dto.Name = ParseAsNonEmptyString(value),
            (dto, value) => dto.ShortEffect = ParseAsString(value),
            (dto, value) => dto.Effect = ParseAsString(value),
            (dto, value) => dto.AtkBoost = ParseAsDecimal(value),
            (dto, value) => dto.SpaBoost = ParseAsDecimal(value),
            (dto, value) => dto.DefBoost = ParseAsDecimal(value),
            (dto, value) => dto.SpdBoost = ParseAsDecimal(value),
            (dto, value) => dto.SpeBoost = ParseAsDecimal(value),
            (dto, value) => dto.HpBoost = ParseAsDecimal(value),
            (dto, value) => dto.BoostConditions = ParseAsString(value),
        };
    }
}
