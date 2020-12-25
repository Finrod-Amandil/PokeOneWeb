using Google.Apis.Sheets.v4.Data;
using Microsoft.Extensions.Logging;

namespace PokeOneWeb.Services.GoogleSpreadsheet.Import.Impl.MasterData.Currencies
{
    public class CurrencyReader : SpreadsheetEntityReader<CurrencyDto>
    {
        public CurrencyReader(ILogger<CurrencyReader> logger) : base(logger) { }

        protected override CurrencyDto ReadRow(RowData rowData)
        {
            if (rowData is null || rowData.Values.Count < 1)
            {
                throw new InvalidRowDataException("Row data does not contain sufficient values.");
            }

            var value = new CurrencyDto
            {
                ItemName = rowData.Values[0]?.EffectiveValue?.StringValue
            };

            if (value.ItemName is null)
            {
                throw new InvalidRowDataException($"Tried to read Currency, but required field {nameof(value.ItemName)} was empty.");
            }

            return value;
        }
    }
}
