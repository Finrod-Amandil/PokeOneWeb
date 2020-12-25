using Google.Apis.Sheets.v4.Data;
using Microsoft.Extensions.Logging;

namespace PokeOneWeb.Services.GoogleSpreadsheet.Import.Impl.MainData.Spawns
{
    public class SpawnReader : SpreadsheetEntityReader<SpawnDto>
    {
        public SpawnReader(ILogger<SpawnReader> logger) : base(logger) { }

        protected override SpawnDto ReadRow(RowData rowData)
        {
            if (rowData is null || rowData.Values.Count < 5)
            {
                throw new InvalidRowDataException("Row data does not contain sufficient values.");
            }

            var value = new SpawnDto
            {
                LocationName = rowData.Values[0].EffectiveValue?.StringValue,
                PokemonForm = rowData.Values[1].EffectiveValue?.StringValue,
                Season = rowData.Values[2]?.EffectiveValue?.StringValue ?? "Any",
                TimeOfDay = rowData.Values[3]?.EffectiveValue?.StringValue ?? "Any",
                SpawnType = rowData.Values[4]?.EffectiveValue?.StringValue,
            };

            if (value.LocationName is null)
            {
                throw new InvalidRowDataException($"Tried to read Spawn, but required field {nameof(value.LocationName)} was empty.");
            }

            if (value.PokemonForm is null)
            {
                throw new InvalidRowDataException($"Tried to read Spawn, but required field {nameof(value.PokemonForm)} was empty.");
            }

            if (value.SpawnType is null)
            {
                throw new InvalidRowDataException($"Tried to read Spawn, but required field {nameof(value.SpawnType)} was empty.");
            }

            if (rowData.Values.Count > 5)
            {
                value.SpawnCommonality = rowData.Values[5]?.EffectiveValue?.StringValue;
            }

            if (rowData.Values.Count > 6)
            {
                value.SpawnProbability = rowData.Values[6]?.EffectiveValue?.StringValue;
            }

            if (rowData.Values.Count > 7)
            {
                value.EncounterCount = (int?)rowData.Values[7]?.EffectiveValue?.NumberValue;
            }

            if (rowData.Values.Count > 8)
            {
                value.IsConfirmed = rowData.Values[8]?.EffectiveValue?.BoolValue ?? false;
            }

            if (rowData.Values.Count > 9)
            {
                value.Notes = rowData.Values[9]?.EffectiveValue?.StringValue;
            }

            return value;
        }
    }
}
