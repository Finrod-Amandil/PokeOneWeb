using System;
using System.Collections.Generic;
using PokeOneWeb.Data.Entities;
using PokeOneWeb.Shared.Extensions;

namespace PokeOneWeb.DataSync.GoogleSpreadsheet.Import.Impl.SheetMappers
{
    public class EvolutionSheetMapper : SheetMapper<Evolution>
    {
        public EvolutionSheetMapper(ISpreadsheetImportReporter reporter) : base(reporter)
        {
        }

        protected override Dictionary<string, Action<Evolution, object>> ValueToEntityMappings => new()
        {
            { "BasePokemonSpecies", (e, v) => e.BasePokemonSpeciesName = v.ParseAsNonEmptyString() },
            { "BasePokemonVariety", (e, v) => e.BasePokemonVarietyName = v.ParseAsNonEmptyString() },
            { "BaseStage", (e, v) => e.BaseStage = v.ParseAsInt() },
            { "EvolvedPokemonVariety", (e, v) => e.EvolvedPokemonVarietyName = v.ParseAsNonEmptyString() },
            { "EvolvedStage", (e, v) => e.EvolvedStage = v.ParseAsInt() },
            { "EvolutionTrigger", (e, v) => e.BasePokemonSpeciesName = v.ParseAsString() },
            { "IsReversible", (e, v) => e.IsReversible = v.ParseAsBoolean(defaultValue: false) },
            { "IsAvailable", (e, v) => e.IsAvailable = v.ParseAsBoolean(defaultValue: true) },
            { "IsReversible", (e, v) => e.DoInclude = v.ParseAsBoolean(defaultValue: true) },
        };
    }
}