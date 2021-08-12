using System.Collections.Generic;

namespace PokeOneWeb.Services.GoogleSpreadsheet.Import.Impl.ItemStatBoosts
{
    public class ItemStatBoostSheetRowParser : ISheetRowParser<ItemStatBoostDto>
    {
        public ItemStatBoostDto ReadRow(List<object> values)
        {
            if (values is null || values.Count < 7)
            {
                throw new InvalidRowDataException("Row data does not contain sufficient values.");
            }

            var value = new ItemStatBoostDto
            {
                ItemName = values[0] as string,
                AtkBoost = decimal.TryParse(values[1].ToString(), out var parsedAtk) ? parsedAtk : 0M,
                SpaBoost = decimal.TryParse(values[2].ToString(), out var parsedSpa) ? parsedSpa : 0M,
                DefBoost = decimal.TryParse(values[3].ToString(), out var parsedDef) ? parsedDef : 0M,
                SpdBoost = decimal.TryParse(values[4].ToString(), out var parsedSpd) ? parsedSpd : 0M,
                SpeBoost = decimal.TryParse(values[5].ToString(), out var parsedSpe) ? parsedSpe : 0M,
                HpBoost = decimal.TryParse(values[6].ToString(), out var parsedHp) ? parsedHp : 0M
            };

            if (value.ItemName is null)
            {
                throw new InvalidRowDataException($"Tried to read Item stat boost, but required field {nameof(value.ItemName)} was empty.");
            }

            if (values.Count > 7)
            {
                value.RequiredPokemonName = values[7] as string;
            }

            return value;
        }
    }
}
