using System.Collections.Generic;

namespace PokeOneWeb.DataSync.GoogleSpreadsheet.Import.Impl.Sheets.HuntingConfigurations
{
    public class HuntingConfigurationSheetRowParser : SheetRowParser<HuntingConfigurationSheetDto>
    {
        protected override int RequiredValueCount => 3;

        protected override List<Action<HuntingConfigurationSheetDto, object>> MappingDelegates => new()
        {
            (dto, value) => dto.PokemonVarietyName = ParseAsNonEmptyString(value),
            (dto, value) => dto.Nature = ParseAsNonEmptyString(value),
            (dto, value) => dto.Ability = ParseAsNonEmptyString(value),
        };
    }
}