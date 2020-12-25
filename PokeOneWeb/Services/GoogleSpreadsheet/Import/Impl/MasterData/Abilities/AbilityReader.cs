using Google.Apis.Sheets.v4.Data;
using Microsoft.Extensions.Logging;

namespace PokeOneWeb.Services.GoogleSpreadsheet.Import.Impl.MasterData.Abilities
{
    public class AbilityReader : SpreadsheetEntityReader<AbilityDto>
    {
        public AbilityReader(ILogger<AbilityReader> logger) : base(logger) { }

        protected override AbilityDto ReadRow(RowData rowData)
        {
            if (rowData is null || rowData.Values.Count < 1)
            {
                throw new InvalidRowDataException("Row data does not contain sufficient values.");
            }

            var value = new AbilityDto
            {
                Name = rowData.Values[0]?.EffectiveValue?.StringValue
            };

            if (value.Name is null)
            {
                throw new InvalidRowDataException($"Tried to read Ability, but required field {nameof(value.Name)} was empty.");
            }

            if (rowData.Values.Count > 1)
            {
                value.PokeApiName = rowData.Values[1]?.EffectiveValue?.StringValue;
            }

            if (rowData.Values.Count > 2)
            {
                value.ShortEffect = rowData.Values[2]?.EffectiveValue?.StringValue;
            }

            if (rowData.Values.Count > 3)
            {
                value.Effect = rowData.Values[3]?.EffectiveValue?.StringValue;
            }

            return value;
        }
    }
}
