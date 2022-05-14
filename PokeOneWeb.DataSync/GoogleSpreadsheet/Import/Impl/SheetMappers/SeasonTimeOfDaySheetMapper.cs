using System;
using System.Collections.Generic;
using PokeOneWeb.Data.Entities;
using PokeOneWeb.Shared.Extensions;

namespace PokeOneWeb.DataSync.GoogleSpreadsheet.Import.Impl.SheetMappers
{
    public class SeasonTimeOfDaySheetMapper : SheetMapper<SeasonTimeOfDay>
    {
        public SeasonTimeOfDaySheetMapper(ISpreadsheetImportReporter reporter) : base(reporter)
        {
        }

        protected override Dictionary<string, Action<SeasonTimeOfDay, object>> ValueToEntityMappings => new()
        {
            { "Season", (e, v) => e.SeasonName = v.ParseAsNonEmptyString() },
            { "Time", (e, v) => e.TimeOfDayName = v.ParseAsNonEmptyString() },
            { "StartHour", (e, v) => e.StartHour = v.ParseAsInt() },
            { "EndHour", (e, v) => e.EndHour = v.ParseAsInt() },
        };
    }
}