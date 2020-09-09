using Google.Apis.Sheets.v4.Data;
using Microsoft.Extensions.Logging;

namespace PokeOneWeb.Services.GoogleSpreadsheet.Impl.PlacedItem
{
    public class PlacedItemReader : SpreadsheetReader<PlacedItemDto>
    {
        public PlacedItemReader(ILogger<PlacedItemReader> logger) : base(logger) { }

        protected override PlacedItemDto ReadRow(RowData rowData)
        {
            if (rowData is null || rowData.Values.Count < 6)
            {
                throw new InvalidRowDataException("Row data does not contain sufficient values.");
            }

            var value = new PlacedItemDto
            {
                LocationName = rowData.Values[0]?.EffectiveValue?.StringValue,
                Quantity = (int)(rowData.Values[1]?.EffectiveValue?.NumberValue ?? 1D),
                ItemName = rowData.Values[2]?.EffectiveValue?.StringValue,
                PlacementDescription = rowData.Values[3]?.EffectiveValue?.StringValue,
                IsHidden = rowData.Values[4]?.EffectiveValue?.BoolValue ?? false,
                IsConfirmed = rowData.Values[5]?.EffectiveValue?.BoolValue ?? false
            };

            if (value.LocationName is null)
            {
                throw new InvalidRowDataException($"Tried to read PlacedItem, but required field {nameof(value.LocationName)} was empty.");
            }

            if (value.ItemName is null)
            {
                throw new InvalidRowDataException($"Tried to read PlacedItem, but required field {nameof(value.ItemName)} was empty.");
            }

            if (rowData.Values.Count > 6)
            {
                value.Requirements = rowData.Values[6]?.EffectiveValue?.StringValue;
            }

            if (rowData.Values.Count > 7)
            {
                value.Notes = rowData.Values[7]?.EffectiveValue?.StringValue;
            }

            return value;
        }
    }
}
