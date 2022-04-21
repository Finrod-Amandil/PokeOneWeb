using System;
using System.Collections.Generic;

namespace PokeOneWeb.DataSync.GoogleSpreadsheet.Import.Impl.Sheets.Evolutions
{
    public class EvolutionXSheetRowParser : XSheetRowParser<EvolutionSheetDto>
    {
        protected override int RequiredValueCount => 10;

        protected override List<Action<EvolutionSheetDto, object>> MappingDelegates => new()
        {
            (dto, value) => dto.BasePokemonSpeciesPokedexNumber = ParseAsInt(value),
            (dto, value) => dto.BasePokemonSpeciesName = ParseAsNonEmptyString(value),
            (dto, value) => dto.BasePokemonVarietyName = ParseAsNonEmptyString(value),
            (dto, value) => dto.BaseStage = ParseAsInt(value),
            (dto, value) => dto.EvolvedPokemonVarietyName = ParseAsNonEmptyString(value),
            (dto, value) => dto.EvolvedStage = ParseAsInt(value),
            (dto, value) => dto.EvolutionTrigger = ParseAsString(value),
            (dto, value) => dto.IsReversible = ParseAsBoolean(value),
            (dto, value) => dto.IsAvailable = ParseAsBoolean(value),
            (dto, value) => dto.DoInclude = ParseAsBoolean(value),
        };
    }
}