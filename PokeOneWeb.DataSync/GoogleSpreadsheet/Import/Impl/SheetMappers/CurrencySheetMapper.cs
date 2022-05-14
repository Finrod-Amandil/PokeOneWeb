using System;
using System.Collections.Generic;
using PokeOneWeb.Data.Entities;
using PokeOneWeb.Shared.Extensions;

namespace PokeOneWeb.DataSync.GoogleSpreadsheet.Import.Impl.SheetMappers
{
    public class CurrencySheetMapper : SheetMapper<Currency>
    {
        public CurrencySheetMapper(ISpreadsheetImportReporter reporter) : base(reporter)
        {
        }

        protected override Dictionary<string, Action<Currency, object>> ValueToEntityMappings => new()
        {
            { "Item", (e, v) => e.ItemName = v.ParseAsNonEmptyString() }
        };
    }
}