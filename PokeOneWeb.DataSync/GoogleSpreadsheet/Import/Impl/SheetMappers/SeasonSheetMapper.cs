using System;
using System.Collections.Generic;
using PokeOneWeb.Data.Entities;
using PokeOneWeb.Shared.Extensions;

namespace PokeOneWeb.DataSync.GoogleSpreadsheet.Import.Impl.SheetMappers
{
    public class SeasonSheetMapper : SheetMapper<Season>
    {
        public SeasonSheetMapper(ISpreadsheetImportReporter reporter) : base(reporter)
        {
        }

        protected override Dictionary<string, Action<Season, object>> ValueToEntityMappings => new()
        {
            { "SortIndex", (e, v) => e.SortIndex = v.ParseAsInt() },
            { "Name", (e, v) => e.Name = v.ParseAsNonEmptyString() },
            { "Abbreviation", (e, v) => e.Abbreviation = v.ParseAsNonEmptyString() },
            { "Color", (e, v) => e.Color = v.ParseAsOptionalString() },
        };
    }
}