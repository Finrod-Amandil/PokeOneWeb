namespace PokeOneWeb.DataSync.GoogleSpreadsheet.Import.Impl.Sheets.ItemStatBoosts
{
    public class ItemStatBoostSheetRowParser : ISheetRowParser<ItemStatBoostSheetDto>
    {
        public ItemStatBoostSheetDto ReadRow(List<object> values)
        {
            if (values is null || values.Count < 7)
            {
                throw new InvalidRowDataException("Row data does not contain sufficient values.");
            }

            var value = new ItemStatBoostSheetDto
            {
                ItemName = values[0] as string,
                AtkBoost = decimal.TryParse(values[1].ToString(), out var parsedAtk) ? parsedAtk : 1M,
                SpaBoost = decimal.TryParse(values[2].ToString(), out var parsedSpa) ? parsedSpa : 1M,
                DefBoost = decimal.TryParse(values[3].ToString(), out var parsedDef) ? parsedDef : 1M,
                SpdBoost = decimal.TryParse(values[4].ToString(), out var parsedSpd) ? parsedSpd : 1M,
                SpeBoost = decimal.TryParse(values[5].ToString(), out var parsedSpe) ? parsedSpe : 1M,
                HpBoost = decimal.TryParse(values[6].ToString(), out var parsedHp) ? parsedHp : 1M
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
