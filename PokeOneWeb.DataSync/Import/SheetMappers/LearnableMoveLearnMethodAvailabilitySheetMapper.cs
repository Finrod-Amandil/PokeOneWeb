using System;
using System.Collections.Generic;
using PokeOneWeb.Data.Entities;
using PokeOneWeb.DataSync.Import.Interfaces;
using PokeOneWeb.Shared.Extensions;

namespace PokeOneWeb.DataSync.Import.SheetMappers
{
    public class LearnableMoveLearnMethodAvailabilitySheetMapper : SheetMapper<LearnableMoveLearnMethodAvailability>
    {
        public LearnableMoveLearnMethodAvailabilitySheetMapper(ISpreadsheetImportReporter reporter) : base(reporter)
        {
        }

        protected override Dictionary<string, Action<LearnableMoveLearnMethodAvailability, object>> ValueToEntityMappings => new()
        {
            { "Name", (e, v) => e.Name = v.ParseAsNonEmptyString() },
            { "IsAvailable", (e, v) => e.IsAvailable = v.ParseAsBoolean() },
            { "Description", (e, v) => e.Description = v.ParseAsNonEmptyString() },
        };
    }
}
