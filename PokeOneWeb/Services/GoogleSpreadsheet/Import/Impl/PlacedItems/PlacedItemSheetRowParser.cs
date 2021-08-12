using System.Collections.Generic;

namespace PokeOneWeb.Services.GoogleSpreadsheet.Import.Impl.PlacedItems
{
    public class PlacedItemSheetRowParser : ISheetRowParser<PlacedItemDto>
    {
        public PlacedItemDto ReadRow(List<object> values)
        {
            if (values is null || values.Count < 5)
            {
                throw new InvalidRowDataException("Row data does not contain sufficient values.");
            }

            var value = new PlacedItemDto
            {
                LocationName = values[0] as string,
                Quantity = int.TryParse(values[1].ToString(), out var parsedQuantity) ? parsedQuantity : 1,
                ItemName = values[2] as string,
                SortIndex = int.TryParse(values[3].ToString(), out var parsedSortIndex) ? parsedSortIndex : 0,
                Index = int.TryParse(values[4].ToString(), out var parsedIndex) ? parsedIndex : 0,
            };

            if (value.LocationName is null)
            {
                throw new InvalidRowDataException($"Tried to read PlacedItem, but required field {nameof(value.LocationName)} was empty.");
            }

            if (value.ItemName is null)
            {
                throw new InvalidRowDataException($"Tried to read PlacedItem, but required field {nameof(value.ItemName)} was empty.");
            }

            if (value.Index == 0)
            {
                throw new InvalidRowDataException($"Tried to read PlacedItem, but required field {nameof(value.Index)} was zero.");
            }

            if (values.Count > 5)
            {
                value.PlacementDescription = values[5] as string;
            }

            if (values.Count > 6)
            {
                value.IsHidden = bool.TryParse(values[6].ToString(), out var parsed) && parsed;
            }

            if (values.Count > 7)
            {
                value.IsConfirmed = bool.TryParse(values[7].ToString(), out var parsed) && parsed;
            }

            if (values.Count > 8)
            {
                value.Requirements = values[8] as string;
            }

            if (values.Count > 9)
            {
                value.ScreenshotName = values[9] as string;
            }

            if (values.Count > 10)
            {
                value.Notes = values[10] as string;
            }

            return value;
        }
    }
}
