using Google.Apis.Sheets.v4.Data;
using Microsoft.Extensions.Logging;

namespace PokeOneWeb.Services.GoogleSpreadsheet.Import.Impl.MasterData.PvpTiers
{
    public class PvpTierReader : SpreadsheetEntityReader<PvpTierDto>
    {
        public PvpTierReader(ILogger<PvpTierReader> logger) : base(logger) { }

        protected override PvpTierDto ReadRow(RowData rowData)
        {
            if (rowData is null || rowData.Values.Count < 2)
            {
                throw new InvalidRowDataException("Row data does not contain sufficient values.");
            }

            var value = new PvpTierDto
            {
                Name = rowData.Values[0]?.EffectiveValue?.StringValue,
                SortIndex = (int?) rowData.Values[1]?.EffectiveValue?.NumberValue ?? 0
            };

            if (value.Name is null)
            {
                throw new InvalidRowDataException($"Tried to read PvpTier, but required field {nameof(value.Name)} was empty.");
            }

            return value;
        }
    }
}
