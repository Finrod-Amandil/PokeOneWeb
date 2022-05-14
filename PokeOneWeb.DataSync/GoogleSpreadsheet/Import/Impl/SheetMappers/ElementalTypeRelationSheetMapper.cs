using System;
using System.Collections.Generic;
using PokeOneWeb.Data.Entities;
using PokeOneWeb.Shared.Extensions;

namespace PokeOneWeb.DataSync.GoogleSpreadsheet.Import.Impl.SheetMappers
{
    public class ElementalTypeRelationSheetMapper : SheetMapper<ElementalTypeRelation>
    {
        public ElementalTypeRelationSheetMapper(ISpreadsheetImportReporter reporter) : base(reporter)
        {
        }

        protected override Dictionary<string, Action<ElementalTypeRelation, object>> ValueToEntityMappings => new()
        {
            { "AttackingType", (e, v) => e.AttackingTypeName = v.ParseAsNonEmptyString() },
            { "DefendingType", (e, v) => e.DefendingTypeName = v.ParseAsNonEmptyString() },
            { "Effectivity", (e, v) => e.AttackEffectivity = v.ParseAsDecimal() },
        };
    }
}