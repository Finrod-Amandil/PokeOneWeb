using System;
using System.Collections.Generic;
using PokeOneWeb.Data.Entities;
using PokeOneWeb.DataSync.Import.Interfaces;
using PokeOneWeb.Shared.Extensions;

namespace PokeOneWeb.DataSync.Import.SheetMappers
{
    public class AvailabilitySheetMapper : SheetMapper<PokemonAvailability>
    {
        public AvailabilitySheetMapper(ISpreadsheetImportReporter reporter) : base(reporter)
        {
        }

        protected override Dictionary<string, Action<PokemonAvailability, object>> ValueToEntityMappings => new()
        {
            { "Name", (e, v) => e.Name = v.ParseAsNonEmptyString() },
            { "Description", (e, v) => e.Description = v.ParseAsNonEmptyString() },
        };
    }
}