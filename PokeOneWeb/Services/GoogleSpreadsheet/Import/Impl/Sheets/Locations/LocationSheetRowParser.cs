using System.Collections.Generic;

namespace PokeOneWeb.Services.GoogleSpreadsheet.Import.Impl.Sheets.Locations
{
    public class LocationSheetRowParser : ISheetRowParser<LocationSheetDto>
    {
        public LocationSheetDto ReadRow(List<object> values)
        {
            if (values is null || values.Count < 6)
            {
                throw new InvalidRowDataException("Row data does not contain sufficient values.");
            }

            var value = new LocationSheetDto
            {
                RegionName = values[0] as string,
                LocationGroupName = values[1] as string,
                ResourceName = values[2] as string,
                LocationName = values[3] as string,
                SortIndex = int.TryParse(values[4].ToString(), out var parsedSortIndex) ? parsedSortIndex : 0,
                IsDiscoverable = bool.TryParse(values[5].ToString(), out var parsedIsDiscoverable) && parsedIsDiscoverable
            };

            if (value.RegionName is null)
            {
                throw new InvalidRowDataException($"Tried to read Location, but required field {nameof(value.RegionName)} was empty.");
            }

            if (value.LocationGroupName is null)
            {
                throw new InvalidRowDataException($"Tried to read Location, but required field {nameof(value.LocationGroupName)} was empty.");
            }

            if (value.ResourceName is null)
            {
                throw new InvalidRowDataException($"Tried to read Location, but required field {nameof(value.ResourceName)} was empty.");
            }

            if (value.LocationName is null)
            {
                throw new InvalidRowDataException($"Tried to read Location, but required field {nameof(value.LocationName)} was empty.");
            }

            if (values.Count > 6)
            {
                value.Notes = values[6] as string;
            }

            return value;
        }
    }
}
