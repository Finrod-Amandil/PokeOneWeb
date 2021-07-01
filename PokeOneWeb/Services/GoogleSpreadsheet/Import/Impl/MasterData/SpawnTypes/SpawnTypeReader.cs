using Google.Apis.Sheets.v4.Data;
using Microsoft.Extensions.Logging;

namespace PokeOneWeb.Services.GoogleSpreadsheet.Import.Impl.MasterData.SpawnTypes
{
    public class SpawnTypeReader : SpreadsheetEntityReader<SpawnTypeDto>
    {
        public SpawnTypeReader(ILogger<SpawnTypeReader> logger) : base(logger) { }

        protected override SpawnTypeDto ReadRow(RowData rowData)
        {
            if (rowData is null || rowData.Values.Count < 5)
            {
                throw new InvalidRowDataException("Row data does not contain sufficient values.");
            }

            var value = new SpawnTypeDto
            {
                Name = rowData.Values[0]?.EffectiveValue?.StringValue,
                SortIndex = (int?)rowData.Values[1]?.EffectiveValue?.NumberValue ?? 0,
                IsSyncable = rowData.Values[2]?.EffectiveValue?.BoolValue ?? false,
                IsInfinite = rowData.Values[3]?.EffectiveValue?.BoolValue ?? false,
                Color = rowData.Values[4]?.EffectiveValue?.StringValue
            };

            if (value.Name is null)
            {
                throw new InvalidRowDataException($"Tried to read Spawn Type, but required field {nameof(value.Name)} was empty.");
            }

            return value;
        }
    }
}
