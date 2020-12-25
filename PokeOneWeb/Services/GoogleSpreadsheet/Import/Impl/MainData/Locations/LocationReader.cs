using Google.Apis.Sheets.v4.Data;
using Microsoft.Extensions.Logging;

namespace PokeOneWeb.Services.GoogleSpreadsheet.Import.Impl.MainData.Locations
{
    public class LocationReader : SpreadsheetEntityReader<LocationDto>
    {
        public LocationReader(ILogger<LocationReader> logger) : base(logger) { }

        protected override LocationDto ReadRow(RowData rowData)
        {
            if (rowData is null || rowData.Values.Count < 6)
            {
                throw new InvalidRowDataException("Row data does not contain sufficient values.");
            }

            var value = new LocationDto
            {
                RegionName = rowData.Values[0].EffectiveValue.StringValue,
                LocationGroupName = rowData.Values[1].EffectiveValue.StringValue,
                ResourceName = rowData.Values[2].EffectiveValue.StringValue,
                LocationName = rowData.Values[3].EffectiveValue.StringValue,
                SortIndex = (int)(rowData.Values[4].EffectiveValue.NumberValue ?? 0D),
                IsDiscoverable = rowData.Values[5].EffectiveValue.BoolValue ?? false,
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

            if (rowData.Values.Count > 6)
            {
                value.Notes = rowData.Values[6]?.EffectiveValue?.StringValue;
            }

            return value;
        }
    }
}
