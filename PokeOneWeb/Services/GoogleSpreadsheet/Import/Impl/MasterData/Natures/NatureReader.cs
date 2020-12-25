using Google.Apis.Sheets.v4.Data;
using Microsoft.Extensions.Logging;

namespace PokeOneWeb.Services.GoogleSpreadsheet.Import.Impl.MasterData.Natures
{
    public class NatureReader : SpreadsheetEntityReader<NatureDto>
    {
        public NatureReader(ILogger<NatureReader> logger) : base(logger) { }

        protected override NatureDto ReadRow(RowData rowData)
        {
            if (rowData is null || rowData.Values.Count < 6)
            {
                throw new InvalidRowDataException("Row data does not contain sufficient values.");
            }

            var value = new NatureDto
            {
                Name = rowData.Values[0]?.EffectiveValue?.StringValue,
                Attack = (int?) rowData.Values[1]?.EffectiveValue?.NumberValue ?? 0,
                SpecialAttack = (int?) rowData.Values[2]?.EffectiveValue?.NumberValue ?? 0,
                Defense = (int?) rowData.Values[3]?.EffectiveValue?.NumberValue ?? 0,
                SpecialDefense = (int?) rowData.Values[4]?.EffectiveValue?.NumberValue ?? 0,
                Speed = (int?) rowData.Values[5]?.EffectiveValue?.NumberValue ?? 0
            };

            if (value.Name is null)
            {
                throw new InvalidRowDataException($"Tried to read Nature, but required field {nameof(value.Name)} was empty.");
            }

            return value;
        }
    }
}
