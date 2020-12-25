using Google.Apis.Sheets.v4.Data;
using Microsoft.Extensions.Logging;

namespace PokeOneWeb.Services.GoogleSpreadsheet.Import.Impl.MasterData.Evolutions
{
    public class EvolutionReader : SpreadsheetEntityReader<EvolutionDto>
    {
        public EvolutionReader(ILogger<EvolutionReader> logger) : base(logger) { }

        protected override EvolutionDto ReadRow(RowData rowData)
        {
            if (rowData is null || rowData.Values.Count < 10)
            {
                throw new InvalidRowDataException("Row data does not contain sufficient values.");
            }

            var value = new EvolutionDto
            {
                BasePokemonSpeciesPokedexNumber = (int?) rowData.Values[0]?.EffectiveValue?.NumberValue ?? 0,
                BasePokemonSpeciesName = rowData.Values[1]?.EffectiveValue?.StringValue,
                BasePokemonVarietyName = rowData.Values[2]?.EffectiveValue?.StringValue,
                BaseStage = (int?) rowData.Values[3]?.EffectiveValue?.NumberValue ?? 0,
                EvolvedPokemonVarietyName = rowData.Values[4]?.EffectiveValue?.StringValue,
                EvolvedStage = (int?) rowData.Values[5]?.EffectiveValue?.NumberValue ?? 0,
                EvolutionTrigger = rowData.Values[6]?.EffectiveValue?.StringValue,
                IsReversible = rowData.Values[7]?.EffectiveValue?.BoolValue ?? false,
                IsAvailable = rowData.Values[8]?.EffectiveValue?.BoolValue ?? false,
                DoInclude = rowData.Values[9]?.EffectiveValue?.BoolValue ?? false,
            };

            if (value.BasePokemonSpeciesName is null)
            {
                throw new InvalidRowDataException($"Tried to read Evolution, but required field {nameof(value.BasePokemonSpeciesName)} was empty.");
            }

            if (value.BasePokemonVarietyName is null)
            {
                throw new InvalidRowDataException($"Tried to read Evolution, but required field {nameof(value.BasePokemonVarietyName)} was empty.");
            }

            if (value.EvolvedPokemonVarietyName is null)
            {
                throw new InvalidRowDataException($"Tried to read Evolution, but required field {nameof(value.EvolvedPokemonVarietyName)} was empty.");
            }

            return value;
        }
    }
}
