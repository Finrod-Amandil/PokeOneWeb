using Google.Apis.Sheets.v4.Data;
using Microsoft.Extensions.Logging;

namespace PokeOneWeb.Services.GoogleSpreadsheet.Import.Impl.MainData.HuntingConfigurations
{
    public class HuntingConfigurationReader : SpreadsheetEntityReader<HuntingConfigurationDto>
    {
        public HuntingConfigurationReader(ILogger<HuntingConfigurationReader> logger) : base(logger) { }

        protected override HuntingConfigurationDto ReadRow(RowData rowData)
        {
            if (rowData is null || rowData.Values.Count < 3)
            {
                throw new InvalidRowDataException("Row data does not contain sufficient values.");
            }

            var value = new HuntingConfigurationDto
            {
                PokemonVarietyName = rowData.Values[0]?.EffectiveValue?.StringValue,
                Nature = rowData.Values[1]?.EffectiveValue?.StringValue,
                Ability = rowData.Values[2]?.EffectiveValue?.StringValue,
            };

            if (value.PokemonVarietyName is null)
            {
                throw new InvalidRowDataException($"Tried to read Hunting Configuration, but required field {nameof(value.PokemonVarietyName)} was empty.");
            }

            if (value.Nature is null)
            {
                throw new InvalidRowDataException($"Tried to read Hunting Configuration, but required field {nameof(value.Nature)} was empty.");
            }

            if (value.Ability is null)
            {
                throw new InvalidRowDataException($"Tried to read Hunting Configuration, but required field {nameof(value.Ability)} was empty.");
            }

            return value;
        }
    }
}
