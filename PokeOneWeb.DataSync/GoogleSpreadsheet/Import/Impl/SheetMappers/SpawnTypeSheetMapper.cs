using System;
using System.Collections.Generic;
using PokeOneWeb.Data.Entities;
using PokeOneWeb.Shared.Extensions;

namespace PokeOneWeb.DataSync.GoogleSpreadsheet.Import.Impl.SheetMappers
{
    public class SpawnTypeSheetMapper : SheetMapper<SpawnType>
    {
        public SpawnTypeSheetMapper(ISpreadsheetImportReporter reporter) : base(reporter)
        {
        }

        protected override Dictionary<string, Action<SpawnType, object>> ValueToEntityMappings => new()
        {
            { "Name", (e, v) => e.Name = v.ParseAsNonEmptyString() },
            { "SortIndex", (e, v) => e.SortIndex = v.ParseAsInt() },
            { "IsSyncable", (e, v) => e.IsSyncable = v.ParseAsBoolean() },
            { "IsInfinite", (e, v) => e.IsInfinite = v.ParseAsBoolean() },
            { "Color", (e, v) => e.Color = v.ParseAsNonEmptyString() },
        };
    }
}