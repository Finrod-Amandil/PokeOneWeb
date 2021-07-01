using System;
using Google.Apis.Sheets.v4.Data;
using Microsoft.Extensions.Logging;

namespace PokeOneWeb.Services.GoogleSpreadsheet.Import.Impl.MainData.Regions
{
    public class RegionReader : SpreadsheetEntityReader<RegionDto>
    {
        public RegionReader(ILogger<RegionReader> logger) : base(logger) { }

        protected override RegionDto ReadRow(RowData rowData)
        {
            if (rowData is null || rowData.Values.Count < 2)
            {
                throw new InvalidRowDataException("Row data does not contain sufficient values.");
            }

            var value = new RegionDto
            {
                RegionName = rowData.Values[0]?.EffectiveValue?.StringValue,
                Color = rowData.Values[1]?.EffectiveValue?.StringValue
            };

            if (value.RegionName is null)
            {
                throw new InvalidRowDataException($"Tried to read Region, but required field {nameof(value.RegionName)} was empty.");
            }

            if (rowData.Values.Count > 2)
            {
                value.IsEventRegion = rowData.Values[2]?.EffectiveValue?.BoolValue ?? false;
            }

            if (rowData.Values.Count > 3)
            {
                value.EventName = rowData.Values[3]?.EffectiveValue?.StringValue;
            }

            if (rowData.Values.Count > 4)
            {
                var dateTimeString = rowData.Values[4]?.EffectiveValue?.StringValue;
                var canParse = DateTime.TryParse(dateTimeString, out var eventStart);

                if (!canParse && !string.IsNullOrWhiteSpace(dateTimeString))
                {
                    throw new InvalidRowDataException($"Failed to parse date {dateTimeString}.");
                }

                value.EventStart = eventStart;
            }

            if (rowData.Values.Count > 5)
            {
                var dateTimeString = rowData.Values[5]?.EffectiveValue?.StringValue;
                var canParse = DateTime.TryParse(dateTimeString, out var eventEnd);

                if (!canParse && !string.IsNullOrWhiteSpace(dateTimeString))
                {
                    throw new InvalidRowDataException($"Failed to parse date {dateTimeString}.");
                }

                value.EventEnd = eventEnd;
            }

            return value;
        }
    }
}
