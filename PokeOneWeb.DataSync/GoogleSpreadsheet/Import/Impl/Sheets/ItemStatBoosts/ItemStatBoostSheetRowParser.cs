using System.Collections.Generic;

namespace PokeOneWeb.DataSync.GoogleSpreadsheet.Import.Impl.Sheets.ItemStatBoosts
{
    public class ItemStatBoostSheetRowParser : SheetRowParser<ItemStatBoostSheetDto>
    {
        protected override int RequiredValueCount => 7;

        protected override List<Action<ItemStatBoostSheetDto, object>> MappingDelegates => new()
        {
            (dto, value) => dto.ItemName = ParseAsNonEmptyString(value),
            (dto, value) => dto.AtkBoost = ParseAsDecimal(value),
            (dto, value) => dto.SpaBoost = ParseAsDecimal(value),
            (dto, value) => dto.DefBoost = ParseAsDecimal(value),
            (dto, value) => dto.SpdBoost = ParseAsDecimal(value),
            (dto, value) => dto.SpeBoost = ParseAsDecimal(value),
            (dto, value) => dto.HpBoost = ParseAsDecimal(value),
            (dto, value) => dto.RequiredPokemonName = ParseAsString(value)
        };
    }
}
