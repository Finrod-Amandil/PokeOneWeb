using Google.Apis.Sheets.v4.Data;
using Microsoft.Extensions.Logging;

namespace PokeOneWeb.Services.GoogleSpreadsheet.Import.Impl.MasterData.Availabilities
{
    public class AvailabilityReader : SpreadsheetEntityReader<AvailabilityDto>
    {
        public AvailabilityReader(ILogger<AvailabilityReader> logger) : base(logger) { }

        protected override AvailabilityDto ReadRow(RowData rowData)
        {
            if (rowData is null || rowData.Values.Count < 2)
            {
                throw new InvalidRowDataException("Row data does not contain sufficient values.");
            }

            var value = new AvailabilityDto
            {
                Name = rowData.Values[0]?.EffectiveValue?.StringValue,
                Description = rowData.Values[1]?.EffectiveValue?.StringValue
            };

            if (value.Name is null)
            {
                throw new InvalidRowDataException($"Tried to read Availability, but required field {nameof(value.Name)} was empty.");
            }

            if (value.Description is null)
            {
                throw new InvalidRowDataException($"Tried to read Availability, but required field {nameof(value.Description)} was empty.");
            }

            return value;
        }
    }
}
