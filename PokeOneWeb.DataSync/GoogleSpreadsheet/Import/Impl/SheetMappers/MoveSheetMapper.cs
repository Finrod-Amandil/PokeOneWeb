using System;
using System.Collections.Generic;
using PokeOneWeb.Data.Entities;
using PokeOneWeb.Shared.Extensions;

namespace PokeOneWeb.DataSync.GoogleSpreadsheet.Import.Impl.SheetMappers
{
    public class MoveSheetMapper : SheetMapper<Move>
    {
        public MoveSheetMapper(ISpreadsheetImportReporter reporter) : base(reporter)
        {
        }

        protected override Dictionary<string, Action<Move, object>> ValueToEntityMappings => new()
        {
            { "Name", (e, v) => e.Name = v.ParseAsNonEmptyString() },
            { "DoInclude", (e, v) => e.DoInclude = v.ParseAsBoolean(defaultValue: true) },
            { "ResourceName", (e, v) => e.ResourceName = v.ParseAsNonEmptyString() },
            { "DamageClass", (e, v) => e.DamageClassName = v.ParseAsNonEmptyString() },
            { "Type", (e, v) => e.ElementalTypeName = v.ParseAsNonEmptyString() },
            { "Power", (e, v) => e.AttackPower = v.ParseAsInt(defaultValue: 0) },
            { "Accuracy", (e, v) => e.Accuracy = v.ParseAsInt(defaultValue: 100) },
            { "PowerPoints", (e, v) => e.PowerPoints = v.ParseAsInt() },
            { "Priority", (e, v) => e.Priority = v.ParseAsInt(defaultValue: 0) },
            { "Effect", (e, v) => e.Effect = v.ParseAsString() },
        };
    }
}