using System.Collections.Generic;

namespace PokeOneWeb.Services.GoogleSpreadsheet.Import.Impl.Sheets.Items
{
    public class ItemSheetRowParser : ISheetRowParser<ItemSheetDto>
    {
        public ItemSheetDto ReadRow(List<object> values)
        {
            if (values is null || values.Count < 6)
            {
                throw new InvalidRowDataException("Row data does not contain sufficient values.");
            }

            var value = new ItemSheetDto
            {
                Name = values[0] as string,
                IsAvailable = bool.TryParse(values[1].ToString(), out var parsedIsAvailable) && parsedIsAvailable,
                DoInclude = bool.TryParse(values[2].ToString(), out var parsedDoInclude) && parsedDoInclude,
                ResourceName = values[3] as string,
                SortIndex = int.TryParse(values[4].ToString(), out var parsedSortIndex) ? parsedSortIndex : 0,
                BagCategoryName = values[5] as string
            };

            if (value.Name is null)
            {
                throw new InvalidRowDataException($"Tried to read Item, but required field {nameof(value.Name)} was empty.");
            }

            if (value.ResourceName is null)
            {
                throw new InvalidRowDataException($"Tried to read Item, but required field {nameof(value.ResourceName)} was empty.");
            }

            if (value.BagCategoryName is null)
            {
                throw new InvalidRowDataException($"Tried to read Item, but required field {nameof(value.BagCategoryName)} was empty.");
            }

            if (values.Count > 6)
            {
                value.PokeApiName = values[6] as string;
            }

            if (values.Count > 7)
            {
                value.PokeOneItemId = int.TryParse(values[7].ToString(), out var parsed) ? parsed : 0;
            }

            if (values.Count > 8)
            {
                value.Description = values[8] as string;
            }

            if (values.Count > 9)
            {
                value.Effect = values[9] as string;
            }

            if (values.Count > 10)
            {
                value.SpriteName = values[10] as string;
            }

            return value;
        }
    }
}
