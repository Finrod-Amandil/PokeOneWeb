using System.Collections.Generic;

namespace PokeOneWeb.DataSync.GoogleSpreadsheet.Import.Impl.Sheets.Regions
{
    public class RegionSheetRowParser : ISheetRowParser<RegionSheetDto>
    {
        public RegionSheetDto ReadRow(List<object> values)
        {
            if (values is null || values.Count < 1)
            {
                throw new InvalidRowDataException("Row data does not contain sufficient values.");
            }

            var value = new RegionSheetDto
            {
                Name = values[0] as string
            };

            if (value.Name is null)
            {
                throw new InvalidRowDataException($"Tried to read Region, but required field {nameof(value.Name)} was empty.");
            }

            if (values.Count > 1)
            {
                value.Color = values[1] as string;
            }

            if (values.Count > 2)
            {
                value.IsEventRegion = bool.TryParse(values[2].ToString(), out var parsedIsEventRegion) && parsedIsEventRegion;
            }

            if (values.Count > 3)
            {
                value.EventName = values[3] as string;
            }

            return value;
        }
    }
}
