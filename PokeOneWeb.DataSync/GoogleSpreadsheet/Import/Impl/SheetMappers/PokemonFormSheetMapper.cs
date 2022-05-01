using System;
using System.Collections.Generic;
using PokeOneWeb.Data.Entities;
using PokeOneWeb.Shared.Extensions;

namespace PokeOneWeb.DataSync.GoogleSpreadsheet.Import.Impl.SheetMappers
{
    public class PokemonFormSheetMapper : SheetMapper<PokemonForm>
    {
        public PokemonFormSheetMapper(ISpreadsheetImportReporter reporter) : base(reporter)
        {
        }

        protected override Dictionary<string, Action<PokemonForm, object>> ValueToEntityMappings => new()
        {
            { "SortIndex", (e, v) => e.SortIndex = v.ParseAsInt() },
            { "PokedexNumber", (e, v) => e.PokemonVariety.PokemonSpecies.PokedexNumber = v.ParseAsInt() },
            { "PokemonSpecies", (e, v) => e.PokemonVariety.PokemonSpecies.Name = v.ParseAsNonEmptyString() },
            { "DefaultVarietyName", (e, v) => e.PokemonVariety.PokemonSpecies.DefaultVarietyName = v.ParseAsNonEmptyString() },
            { "PokemonVariety", (e, v) => e.PokemonVariety.Name = v.ParseAsNonEmptyString() },
            { "PokemonVarietyResourceName", (e, v) => e.PokemonVariety.ResourceName = v.ParseAsNonEmptyString() },
            { "PokemonForm", (e, v) => e.Name = v.ParseAsNonEmptyString() },
            { "Availability", (e, v) => e.AvailabilityName = v.ParseAsNonEmptyString() },
            { "SpriteName", (e, v) => e.SpriteName = v.ParseAsNonEmptyString() },
            { "DoInclude", (e, v) => e.PokemonVariety.DoInclude = v.ParseAsBoolean() },
            { "DefaultFormName", (e, v) => e.PokemonVariety.DefaultFormName = v.ParseAsNonEmptyString() },
            { "Atk", (e, v) => e.PokemonVariety.Attack = v.ParseAsInt() },
            { "Spa", (e, v) => e.PokemonVariety.SpecialAttack = v.ParseAsInt() },
            { "Def", (e, v) => e.PokemonVariety.Defense = v.ParseAsInt() },
            { "Spd", (e, v) => e.PokemonVariety.SpecialDefense = v.ParseAsInt() },
            { "Spe", (e, v) => e.PokemonVariety.Speed = v.ParseAsInt() },
            { "Hp", (e, v) => e.PokemonVariety.HitPoints = v.ParseAsInt() },
            { "AtkEv", (e, v) => e.PokemonVariety.AttackEv = v.ParseAsInt() },
            { "SpaEv", (e, v) => e.PokemonVariety.SpecialAttackEv = v.ParseAsInt() },
            { "DefEv", (e, v) => e.PokemonVariety.DefenseEv = v.ParseAsInt() },
            { "SpdEv", (e, v) => e.PokemonVariety.SpecialDefenseEv = v.ParseAsInt() },
            { "SpeEv", (e, v) => e.PokemonVariety.SpeedEv = v.ParseAsInt() },
            { "HpEv", (e, v) => e.PokemonVariety.HitPointsEv = v.ParseAsInt() },
            { "Type1", (e, v) => e.PokemonVariety.PrimaryTypeName = v.ParseAsNonEmptyString() },
            { "Type2", (e, v) => e.PokemonVariety.SecondaryTypeName = v.ParseAsOptionalString() },
            { "PrimaryAbility", (e, v) => e.PokemonVariety.PrimaryAbilityName = v.ParseAsNonEmptyString() },
            { "SecondaryAbility", (e, v) => e.PokemonVariety.SecondaryAbilityName = v.ParseAsOptionalString() },
            { "HiddenAbility", (e, v) => e.PokemonVariety.HiddenAbilityName = v.ParseAsOptionalString() },
            { "PvpTier", (e, v) => e.PokemonVariety.PvpTierName = v.ParseAsNonEmptyString() },
            { "IsMega", (e, v) => e.PokemonVariety.IsMega = v.ParseAsBoolean() },
            { "IsFullyEvolved", (e, v) => e.PokemonVariety.IsFullyEvolved = v.ParseAsBoolean() },
            { "Generation", (e, v) => e.PokemonVariety.Generation = v.ParseAsInt() },
            { "CatchRate", (e, v) => e.PokemonVariety.CatchRate = v.ParseAsInt() },
            { "HasGender", (e, v) => e.PokemonVariety.HasGender = v.ParseAsBoolean() },
            { "MaleRatio", (e, v) => e.PokemonVariety.MaleRatio = v.ParseAsDecimal() },
            { "EggCycles", (e, v) => e.PokemonVariety.EggCycles = v.ParseAsInt() },
            { "Height", (e, v) => e.PokemonVariety.Height = v.ParseAsDecimal() },
            { "Weight", (e, v) => e.PokemonVariety.Weight = v.ParseAsDecimal() },
            { "XpYield", (e, v) => e.PokemonVariety.ExpYield = v.ParseAsInt() },
            { "SmogonUrl", (e, v) => AddUrl(e, v.ParseAsOptionalString(), "Smogon") },
            { "BulbapediaUrl", (e, v) => AddUrl(e, v.ParseAsOptionalString(), "Bulbapedia") },
            { "PokeoneCommunityUrl", (e, v) => AddUrl(e, v.ParseAsOptionalString(), "PokeoneCommunity") },
            { "PokemonShowdownUrl", (e, v) => AddUrl(e, v.ParseAsOptionalString(), "Pokemon Showdown") },
            { "SerebiiUrl", (e, v) => AddUrl(e, v.ParseAsOptionalString(), "Serebii") },
            { "PokemonDbUrl", (e, v) => AddUrl(e, v.ParseAsOptionalString(), "PokemonDB") },
            { "Notes", (e, v) => e.PokemonVariety.Notes = v.ParseAsString() },
        };

        private void AddUrl(PokemonForm entity, string url, string siteName)
        {
            if (!string.IsNullOrWhiteSpace(url))
            {
                entity.PokemonVariety.Urls.Add(new PokemonVarietyUrl
                {
                    Name = siteName,
                    Url = url
                });
            }
        }
    }
}