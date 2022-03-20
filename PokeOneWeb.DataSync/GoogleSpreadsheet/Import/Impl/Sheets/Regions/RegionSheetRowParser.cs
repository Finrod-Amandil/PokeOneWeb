using System.Collections.Generic;

namespace PokeOneWeb.DataSync.GoogleSpreadsheet.Import.Impl.Sheets.Regions
{
    public class RegionSheetRowParser : ISheetRowParser<RegionSheetDto>
    {
        public RegionSheetDto ReadRow(List<object> values)
        {
            if (values is null || values.Count < 2)
            {
                throw new InvalidRowDataException("Row data does not contain sufficient values.");
            }

            var value = new RegionSheetDto
            {
                Name = values[0] as string,
                ResourceName = values[1] as string
            };

            if (value.Name is null)
            {
                throw new InvalidRowDataException($"Tried to read Region, but required field {nameof(value.Name)} was empty.");
            }
            if (value.ResourceName is null)
            {
                throw new InvalidRowDataException($"Tried to read Region, but required field {nameof(value.ResourceName)} was empty.");
            }

            if (values.Count > 2)
            {
                value.Color = values[2] as string;
            }

            if (values.Count > 3)
            {
                value.IsEventRegion = bool.TryParse(values[3].ToString(), out var parsedIsEventRegion) && parsedIsEventRegion;
            }

            if (values.Count > 4)
            {
                value.EventName = values[4] as string;
            }

            return value;
        }
    }
}
