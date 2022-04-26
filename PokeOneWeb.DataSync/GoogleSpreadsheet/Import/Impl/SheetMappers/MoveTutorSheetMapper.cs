using System;
using System.Collections.Generic;
using PokeOneWeb.Data.Entities;
using PokeOneWeb.Shared.Extensions;

namespace PokeOneWeb.DataSync.GoogleSpreadsheet.Import.Impl.SheetMappers
{
    public class MoveTutorSheetMapper : SheetMapper<MoveTutor>
    {
        public MoveTutorSheetMapper(ISpreadsheetImportReporter reporter) : base(reporter)
        {
        }

        protected override Dictionary<string, Action<MoveTutor, object>> ValueToEntityMappings => new()
        {
            { "Name", (e, v) => e.Name = v.ParseAsNonEmptyString() },
            { "Location", (e, v) => e.LocationName = v.ParseAsNonEmptyString() },
            { "PlacementDescription", (e, v) => e.PlacementDescription = v.ParseAsString() },
        };
    }
}