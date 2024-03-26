using System;
using System.Collections.Generic;
using PokeOneWeb.Data.Entities;
using PokeOneWeb.DataSync.Import.Interfaces;
using PokeOneWeb.Shared.Extensions;

namespace PokeOneWeb.DataSync.Import.SheetMappers
{
    public class ElementalTypeSheetMapper : SheetMapper<ElementalType>
    {
        public ElementalTypeSheetMapper(ISpreadsheetImportReporter reporter) : base(reporter)
        {
        }

        protected override Dictionary<string, Action<ElementalType, object>> ValueToEntityMappings => new()
        {
            { "Name", (e, v) => e.Name = v.ParseAsNonEmptyString() }
        };
    }
}