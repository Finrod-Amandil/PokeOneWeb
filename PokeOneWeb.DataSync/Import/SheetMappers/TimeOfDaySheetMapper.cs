using System;
using System.Collections.Generic;
using PokeOneWeb.Data.Entities;
using PokeOneWeb.DataSync.Import.Interfaces;
using PokeOneWeb.Shared.Extensions;

namespace PokeOneWeb.DataSync.Import.SheetMappers
{
    public class TimeOfDaySheetMapper : SheetMapper<TimeOfDay>
    {
        public TimeOfDaySheetMapper(ISpreadsheetImportReporter reporter) : base(reporter)
        {
        }

        protected override Dictionary<string, Action<TimeOfDay, object>> ValueToEntityMappings => new()
        {
            { "SortIndex", (e, v) => e.SortIndex = v.ParseAsInt() },
            { "Name", (e, v) => e.Name = v.ParseAsNonEmptyString() },
            { "Abbreviation", (e, v) => e.Abbreviation = v.ParseAsNonEmptyString() },
            { "Color", (e, v) => e.Color = v.ParseAsString() },
        };
    }
}