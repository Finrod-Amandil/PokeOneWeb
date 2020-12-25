using Google.Apis.Sheets.v4.Data;
using Microsoft.Extensions.Logging;

namespace PokeOneWeb.Services.GoogleSpreadsheet.Import.Impl.MasterData.SpawnTypes
{
    public class SpawnTypeReader : SpreadsheetEntityReader<SpawnTypeDto>
    {
        public SpawnTypeReader(ILogger<SpawnTypeReader> logger) : base(logger) { }

        protected override SpawnTypeDto ReadRow(RowData rowData)
        {
            if (rowData is null || rowData.Values.Count < 3)
            {
                throw new InvalidRowDataException("Row data does not contain sufficient values.");
            }

            var value = new SpawnTypeDto
            {
                Name = rowData.Values[0]?.EffectiveValue?.StringValue,
                IsSyncable = rowData.Values[1]?.EffectiveValue?.BoolValue ?? false,
                IsInfinite = rowData.Values[2]?.EffectiveValue?.BoolValue ?? false
            };

            if (value.Name is null)
            {
                throw new InvalidRowDataException($"Tried to read Spawn Type, but required field {nameof(value.Name)} was empty.");
            }

            return value;
        }
    }
}
