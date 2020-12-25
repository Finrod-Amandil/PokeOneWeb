using Google.Apis.Sheets.v4.Data;
using Microsoft.Extensions.Logging;

namespace PokeOneWeb.Services.GoogleSpreadsheet.Import.Impl.MasterData.ElementalTypes
{
    public class ElementalTypeReader : SpreadsheetEntityReader<ElementalTypeDto>
    {
        public ElementalTypeReader(ILogger<ElementalTypeReader> logger) : base(logger) { }

        protected override ElementalTypeDto ReadRow(RowData rowData)
        {
            if (rowData is null || rowData.Values.Count < 1)
            {
                throw new InvalidRowDataException("Row data does not contain sufficient values.");
            }

            var value = new ElementalTypeDto
            {
                Name = rowData.Values[0]?.EffectiveValue?.StringValue
            };

            if (value.Name is null)
            {
                throw new InvalidRowDataException($"Tried to read ElementalType, but required field {nameof(value.Name)} was empty.");
            }

            if (rowData.Values.Count > 1)
            {
                value.PokeApiName = rowData.Values[1]?.EffectiveValue?.StringValue;
            }

            return value;
        }
    }
}
