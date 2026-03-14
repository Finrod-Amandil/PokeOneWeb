using System;
using System.Collections.Generic;
using PokeOneWeb.Data.Entities;
using PokeOneWeb.DataSync.Import.Interfaces;
using PokeOneWeb.Shared.Extensions;

namespace PokeOneWeb.DataSync.Import.SheetMappers
{
    public class HuntingConfigurationSheetMapper : SheetMapper<HuntingConfiguration>
    {
        public HuntingConfigurationSheetMapper(ISpreadsheetImportReporter reporter) : base(reporter)
        {
        }

        protected override Dictionary<string, Action<HuntingConfiguration, object>> ValueToEntityMappings => new()
        {
            { "PokemonVariety", (e, v) => e.PokemonVarietyName = v.ParseAsNonEmptyString() },
            { "Nature", (e, v) => e.NatureName = v.ParseAsNonEmptyString() },
            { "Ability", (e, v) => e.AbilityName = v.ParseAsNonEmptyString() },
        };
    }
}