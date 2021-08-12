using System.Collections.Generic;

namespace PokeOneWeb.Services.GoogleSpreadsheet.Import.Impl.Evolutions
{
    public class EvolutionSheetRowParser : ISheetRowParser<EvolutionDto>
    {
        public EvolutionDto ReadRow(List<object> values)
        {
            if (values is null || values.Count < 10)
            {
                throw new InvalidRowDataException("Row data does not contain sufficient values.");
            }

            var value = new EvolutionDto
            {
                BasePokemonSpeciesPokedexNumber = int.TryParse(values[0].ToString(), out var parsedPokedexNumber) ? parsedPokedexNumber : 0,
                BasePokemonSpeciesName = values[1] as string,
                BasePokemonVarietyName = values[2] as string,
                BaseStage = int.TryParse(values[3].ToString(), out var parsedBaseStage) ? parsedBaseStage : 0,
                EvolvedPokemonVarietyName = values[4] as string,
                EvolvedStage = int.TryParse(values[5].ToString(), out var parsedEvolvedStage) ? parsedEvolvedStage : 0,
                EvolutionTrigger = values[6] as string,
                IsReversible = bool.TryParse(values[7].ToString(), out var parsedIsReversible) && parsedIsReversible,
                IsAvailable = bool.TryParse(values[8].ToString(), out var parsedIsAvailable) && parsedIsAvailable,
                DoInclude = bool.TryParse(values[9].ToString(), out var parsedDoInclude) && parsedDoInclude
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
