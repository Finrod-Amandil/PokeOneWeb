using Google.Apis.Sheets.v4.Data;
using Microsoft.Extensions.Logging;

namespace PokeOneWeb.Services.GoogleSpreadsheet.Import.Impl.MainData.Builds
{
    public class BuildReader : SpreadsheetEntityReader<BuildDto>
    {
        public BuildReader(ILogger<BuildDto> logger) : base(logger) { }

        protected override BuildDto ReadRow(RowData rowData)
        {
            if (rowData is null || rowData.Values.Count < 1)
            {
                throw new InvalidRowDataException("Row data does not contain sufficient values.");
            }

            var value = new BuildDto
            {
                PokemonVarietyName = rowData.Values[0].EffectiveValue.StringValue
            };

            if (value.PokemonVarietyName is null)
            {
                throw new InvalidRowDataException($"Tried to read Build, but required field {nameof(value.PokemonVarietyName)} was empty.");
            }

            if (rowData.Values.Count > 1)
            {
                value.BuildName = rowData.Values[1]?.EffectiveValue?.StringValue;
            }

            if (rowData.Values.Count > 2)
            {
                value.BuildDescription = rowData.Values[2]?.EffectiveValue?.StringValue;
            }

            if (rowData.Values.Count > 3)
            {
                value.Move1 = rowData.Values[3]?.EffectiveValue?.StringValue;
            }

            if (rowData.Values.Count > 4)
            {
                value.Move2 = rowData.Values[4]?.EffectiveValue?.StringValue;
            }

            if (rowData.Values.Count > 5)
            {
                value.Move3 = rowData.Values[5]?.EffectiveValue?.StringValue;
            }

            if (rowData.Values.Count > 6)
            {
                value.Move4 = rowData.Values[6]?.EffectiveValue?.StringValue;
            }

            if (rowData.Values.Count > 7)
            {
                value.Item = rowData.Values[7]?.EffectiveValue?.StringValue;
            }

            if (rowData.Values.Count > 8)
            {
                value.Nature = rowData.Values[8]?.EffectiveValue?.StringValue;
            }

            if (rowData.Values.Count > 9)
            {
                value.Ability = rowData.Values[9]?.EffectiveValue?.StringValue;
            }

            if (rowData.Values.Count > 10)
            {
                value.EvDistribution = rowData.Values[10]?.EffectiveValue?.StringValue;
            }

            return value;
        }
    }
}