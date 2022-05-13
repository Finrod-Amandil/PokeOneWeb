using System;
using System.Collections.Generic;
using PokeOneWeb.Data.Entities;
using PokeOneWeb.Shared.Extensions;

namespace PokeOneWeb.DataSync.GoogleSpreadsheet.Import.Impl.SheetMappers
{
    public class NatureSheetMapper : SheetMapper<Nature>
    {
        public NatureSheetMapper(ISpreadsheetImportReporter reporter) : base(reporter)
        {
        }

        protected override Dictionary<string, Action<Nature, object>> ValueToEntityMappings => new()
        {
            { "Name", (e, v) => e.Name = v.ParseAsNonEmptyString() },
            { "Atk", (e, v) => e.Attack = v.ParseAsInt() },
            { "Spa", (e, v) => e.SpecialAttack = v.ParseAsInt() },
            { "Def", (e, v) => e.Defense = v.ParseAsInt() },
            { "Spd", (e, v) => e.SpecialDefense = v.ParseAsInt() },
            { "Spe", (e, v) => e.Speed = v.ParseAsInt() },
        };
    }
}