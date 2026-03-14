using System;
using System.Collections.Generic;
using PokeOneWeb.Data.Entities;
using PokeOneWeb.DataSync.Import.Interfaces;
using PokeOneWeb.Shared.Extensions;

namespace PokeOneWeb.DataSync.Import.SheetMappers
{
    public class ChangeLogSheetMapper : SheetMapper<ChangeLog>
    {
        public ChangeLogSheetMapper(ISpreadsheetImportReporter reporter) : base(reporter)
        {
        }

        protected override Dictionary<string, Action<ChangeLog, object>> ValueToEntityMappings => new()
        {
            { "ChangeLogId", (e, v) => e.ChangeLogId = v.ParseAsInt() },
            { "Date", (e, v) => e.Date = v.ParseAsDate() },
            { "Category", (e, v) => e.Category = v.ParseAsString() },
            { "Description", (e, v) => e.Description = v.ParseAsString() },
        };
    }
}
