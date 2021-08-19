using System.Collections.Generic;

namespace PokeOneWeb.Services.GoogleSpreadsheet.Import.Impl.SpawnTypes
{
    public class SpawnTypeSheetRowParser : ISheetRowParser<SpawnTypeSheetDto>
    {
        public SpawnTypeSheetDto ReadRow(List<object> values)
        {
            if (values is null || values.Count < 5)
            {
                throw new InvalidRowDataException("Row data does not contain sufficient values.");
            }

            var value = new SpawnTypeSheetDto
            {
                Name = values[0] as string,
                SortIndex = int.TryParse(values[1].ToString(), out var parsedSortIndex) ? parsedSortIndex : 0,
                IsSyncable = bool.TryParse(values[2].ToString(), out var parsedIsSyncable) && parsedIsSyncable,
                IsInfinite = bool.TryParse(values[3].ToString(), out var parsedIsInfinite) && parsedIsInfinite,
                Color = values[4] as string
            };

            if (value.Name is null)
            {
                throw new InvalidRowDataException($"Tried to read Spawn Type, but required field {nameof(value.Name)} was empty.");
            }

            return value;
        }
    }
}
