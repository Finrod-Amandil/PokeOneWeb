using System;
using System.Collections.Generic;
using PokeOneWeb.Data.Entities;
using PokeOneWeb.DataSync.Import.Interfaces;
using PokeOneWeb.Shared.Extensions;

namespace PokeOneWeb.DataSync.Import.SheetMappers
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