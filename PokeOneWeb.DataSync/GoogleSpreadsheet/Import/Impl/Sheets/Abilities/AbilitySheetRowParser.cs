using System;
using System.Collections.Generic;

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
            (dto, value) => dto.AtkBoost = ParseAsDecimal(value, 1M),
            (dto, value) => dto.SpaBoost = ParseAsDecimal(value, 1M),
            (dto, value) => dto.DefBoost = ParseAsDecimal(value, 1M),
            (dto, value) => dto.SpdBoost = ParseAsDecimal(value, 1M),
            (dto, value) => dto.SpeBoost = ParseAsDecimal(value, 1M),
            (dto, value) => dto.HpBoost = ParseAsDecimal(value, 1M),
            (dto, value) => dto.BoostConditions = ParseAsString(value),
        };
    }
}
