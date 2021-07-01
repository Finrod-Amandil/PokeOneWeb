using Google.Apis.Sheets.v4.Data;
using Microsoft.Extensions.Logging;

namespace PokeOneWeb.Services.GoogleSpreadsheet.Import.Impl.MasterData.Times
{
    public class TimeReader : SpreadsheetEntityReader<TimeDto>
    {
        public TimeReader(ILogger<TimeReader> logger) : base(logger) { }

        protected override TimeDto ReadRow(RowData rowData)
        {
            if (rowData is null || rowData.Values.Count < 10)
            {
                throw new InvalidRowDataException("Row data does not contain sufficient values.");
            }

            var value = new TimeDto
            {
                SeasonSortIndex = (int?)rowData.Values[0]?.EffectiveValue?.NumberValue ?? 0,
                SeasonName = rowData.Values[1]?.EffectiveValue?.StringValue,
                SeasonAbbreviation = rowData.Values[2]?.EffectiveValue?.StringValue,
                SeasonColor = rowData.Values[3]?.EffectiveValue?.StringValue,
                TimeSortIndex = (int?)rowData.Values[4]?.EffectiveValue?.NumberValue ?? 0,
                TimeName = rowData.Values[5]?.EffectiveValue?.StringValue,
                TimeAbbreviation = rowData.Values[6]?.EffectiveValue?.StringValue,
                TimeColor = rowData.Values[7]?.EffectiveValue?.StringValue,
                StartHour = (int?) rowData.Values[8]?.EffectiveValue?.NumberValue ?? 0,
                EndHour = (int?) rowData.Values[9]?.EffectiveValue?.NumberValue ?? 0
            };

            if (value.SeasonName is null)
            {
                throw new InvalidRowDataException($"Tried to read Time, but required field {nameof(value.SeasonName)} was empty.");
            }

            if (value.SeasonAbbreviation is null)
            {
                throw new InvalidRowDataException($"Tried to read Time, but required field {nameof(value.SeasonAbbreviation)} was empty.");
            }

            if (value.TimeName is null)
            {
                throw new InvalidRowDataException($"Tried to read Time, but required field {nameof(value.TimeName)} was empty.");
            }

            if (value.TimeAbbreviation is null)
            {
                throw new InvalidRowDataException($"Tried to read Time, but required field {nameof(value.TimeAbbreviation)} was empty.");
            }

            return value;
        }
    }
}
