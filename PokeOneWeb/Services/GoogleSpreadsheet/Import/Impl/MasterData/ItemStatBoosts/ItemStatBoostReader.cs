using Google.Apis.Sheets.v4.Data;
using Microsoft.Extensions.Logging;

namespace PokeOneWeb.Services.GoogleSpreadsheet.Import.Impl.MasterData.ItemStatBoosts
{
    public class ItemStatBoostReader : SpreadsheetEntityReader<ItemStatBoostDto>
    {
        public ItemStatBoostReader(ILogger<ItemStatBoostReader> logger) : base(logger) { }

        protected override ItemStatBoostDto ReadRow(RowData rowData)
        {
            if (rowData is null || rowData.Values.Count < 7)
            {
                throw new InvalidRowDataException("Row data does not contain sufficient values.");
            }

            var value = new ItemStatBoostDto
            {
                ItemName = rowData.Values[0]?.EffectiveValue?.StringValue,
                AtkBoost = (decimal?)rowData.Values[1]?.EffectiveValue?.NumberValue ?? 0,
                SpaBoost = (decimal?)rowData.Values[2]?.EffectiveValue?.NumberValue ?? 0,
                DefBoost = (decimal?)rowData.Values[3]?.EffectiveValue?.NumberValue ?? 0,
                SpdBoost = (decimal?)rowData.Values[4]?.EffectiveValue?.NumberValue ?? 0,
                SpeBoost = (decimal?)rowData.Values[5]?.EffectiveValue?.NumberValue ?? 0,
                HpBoost = (decimal?)rowData.Values[6]?.EffectiveValue?.NumberValue ?? 0
            };

            if (value.ItemName is null)
            {
                throw new InvalidRowDataException($"Tried to read Item stat boost, but required field {nameof(value.ItemName)} was empty.");
            }

            if (rowData.Values.Count > 7)
            {
                value.RequiredPokemonName = rowData.Values[7]?.EffectiveValue?.StringValue;
            }

            return value;
        }
    }
}
