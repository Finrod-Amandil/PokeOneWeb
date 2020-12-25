using Google.Apis.Sheets.v4.Data;
using Microsoft.Extensions.Logging;

namespace PokeOneWeb.Services.GoogleSpreadsheet.Import.Impl.MasterData.Items
{
    public class ItemReader : SpreadsheetEntityReader<ItemDto>
    {
        public ItemReader(ILogger<ItemReader> logger) : base(logger) { }

        protected override ItemDto ReadRow(RowData rowData)
        {
            if (rowData is null || rowData.Values.Count < 7)
            {
                throw new InvalidRowDataException("Row data does not contain sufficient values.");
            }

            var value = new ItemDto
            {
                ItemName = rowData.Values[0]?.EffectiveValue?.StringValue,
                IsAvailable = rowData.Values[1]?.EffectiveValue?.BoolValue ?? false,
                DoInclude = rowData.Values[2]?.EffectiveValue?.BoolValue ?? false,
                ResourceName = rowData.Values[3]?.EffectiveValue?.StringValue,
                SortIndex = (int?) rowData.Values[4]?.EffectiveValue?.NumberValue ?? 0,
                BagCategoryName = rowData.Values[5]?.EffectiveValue?.StringValue,
                BagCategorySortIndex = (int?) rowData.Values[6]?.EffectiveValue?.NumberValue ?? 0
            };

            if (value.ItemName is null)
            {
                throw new InvalidRowDataException($"Tried to read Item, but required field {nameof(value.ItemName)} was empty.");
            }

            if (value.ResourceName is null)
            {
                throw new InvalidRowDataException($"Tried to read Item, but required field {nameof(value.ResourceName)} was empty.");
            }

            if (value.BagCategoryName is null)
            {
                throw new InvalidRowDataException($"Tried to read Item, but required field {nameof(value.BagCategoryName)} was empty.");
            }

            if (rowData.Values.Count > 7)
            {
                value.PokeApiName = rowData.Values[7]?.EffectiveValue?.StringValue;
            }

            if (rowData.Values.Count > 8)
            {
                value.PokeOneItemId = (int?)rowData.Values[8]?.EffectiveValue?.NumberValue ?? 0;
            }

            if (rowData.Values.Count > 9)
            {
                value.Description = rowData.Values[9]?.EffectiveValue?.StringValue;
            }

            if (rowData.Values.Count > 10)
            {
                value.Effect = rowData.Values[10]?.EffectiveValue?.StringValue;
            }

            return value;
        }
    }
}
