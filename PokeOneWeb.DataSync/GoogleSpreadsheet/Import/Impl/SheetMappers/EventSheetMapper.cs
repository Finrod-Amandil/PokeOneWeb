using System;
using System.Collections.Generic;
using PokeOneWeb.Data.Entities;
using PokeOneWeb.Shared.Extensions;

namespace PokeOneWeb.DataSync.GoogleSpreadsheet.Import.Impl.SheetMappers
{
    public class EventSheetMapper : SheetMapper<Event>
    {
        public EventSheetMapper(ISpreadsheetImportReporter reporter) : base(reporter)
        {
        }

        protected override Dictionary<string, Action<Event, object>> ValueToEntityMappings => new()
        {
            { "EventName", (e, v) => e.Name = v.ParseAsNonEmptyString() },
            { "StartDate", (e, v) => e.StartDate = v.ParseAsOptionalDate() },
            { "EndDate", (e, v) => e.EndDate = v.ParseAsOptionalDate() },
        };
    }
}