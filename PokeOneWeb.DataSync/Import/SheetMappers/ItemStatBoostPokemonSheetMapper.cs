using System;
using System.Collections.Generic;
using PokeOneWeb.Data.Entities;
using PokeOneWeb.DataSync.Import.Interfaces;
using PokeOneWeb.Shared.Extensions;

namespace PokeOneWeb.DataSync.Import.SheetMappers
{
    public class ItemStatBoostPokemonSheetMapper : SheetMapper<ItemStatBoostPokemon>
    {
        public ItemStatBoostPokemonSheetMapper(ISpreadsheetImportReporter reporter) : base(reporter)
        {
        }

        protected override Dictionary<string, Action<ItemStatBoostPokemon, object>> ValueToEntityMappings => new()
        {
            { "Item", (e, v) => e.ItemStatBoost.ItemName = v.ParseAsNonEmptyString() },
            { "AtkBoost", (e, v) => e.ItemStatBoost.AttackBoost = v.ParseAsDecimal(defaultValue: 1M) },
            { "SpaBoost", (e, v) => e.ItemStatBoost.SpecialAttackBoost = v.ParseAsDecimal(defaultValue: 1M) },
            { "DefBoost", (e, v) => e.ItemStatBoost.DefenseBoost = v.ParseAsDecimal(defaultValue: 1M) },
            { "SpdBoost", (e, v) => e.ItemStatBoost.SpecialDefenseBoost = v.ParseAsDecimal(defaultValue: 1M) },
            { "SpeBoost", (e, v) => e.ItemStatBoost.SpeedBoost = v.ParseAsDecimal(defaultValue: 1M) },
            { "HpBoost", (e, v) => e.ItemStatBoost.HitPointsBoost = v.ParseAsDecimal(defaultValue: 1M) },
            { "RequiredPokemon", (e, v) => e.PokemonVarietyName = v.ParseAsString() },
        };
    }
}