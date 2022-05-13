using System;
using System.Collections.Generic;
using PokeOneWeb.Data.Entities;
using PokeOneWeb.Shared.Extensions;

namespace PokeOneWeb.DataSync.GoogleSpreadsheet.Import.Impl.SheetMappers
{
    public class AbilitySheetMapper : SheetMapper<Ability>
    {
        public AbilitySheetMapper(ISpreadsheetImportReporter reporter) : base(reporter)
        {
        }

        protected override Dictionary<string, Action<Ability, object>> ValueToEntityMappings => new()
        {
            { "Name", (e, v) => e.Name = v.ParseAsNonEmptyString() },
            { "ShortEffect", (e, v) => e.EffectShortDescription = v.ParseAsNonEmptyString() },
            { "Effect", (e, v) => e.EffectDescription = v.ParseAsNonEmptyString() },
            { "Atk", (e, v) => e.AttackBoost = v.ParseAsDecimal(defaultValue: 1) },
            { "Spa", (e, v) => e.SpecialAttackBoost = v.ParseAsDecimal(defaultValue: 1) },
            { "Def", (e, v) => e.DefenseBoost = v.ParseAsDecimal(defaultValue: 1) },
            { "Spd", (e, v) => e.SpecialDefenseBoost = v.ParseAsDecimal(defaultValue: 1) },
            { "Spe", (e, v) => e.SpeedBoost = v.ParseAsDecimal(defaultValue: 1) },
            { "Hp", (e, v) => e.HitPointsBoost = v.ParseAsDecimal(defaultValue: 1) },
            { "Condition", (e, v) => e.BoostConditions = v.ParseAsString() },
        };
    }
}