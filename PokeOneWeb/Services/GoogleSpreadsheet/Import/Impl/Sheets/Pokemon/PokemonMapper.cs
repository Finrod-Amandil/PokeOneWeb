using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Logging;
using PokeOneWeb.Data;
using PokeOneWeb.Data.Entities;
using PokeOneWeb.Extensions;

namespace PokeOneWeb.Services.GoogleSpreadsheet.Import.Impl.Sheets.Pokemon
{
    public class PokemonMapper : SpreadsheetEntityMapper<PokemonSheetDto, PokemonForm>
    {
        private readonly string SmogonUrlName = "Smogon";
        private readonly string BulbapediaUrlName = "Bulbapedia";
        private readonly string PokeoneCommunityUrlName = "PokeoneCommunity";
        private readonly string PokemonShowdownUrlName = "Pokemon Showdown";
        private readonly string SerebiiUrlName = "Serebii";
        private readonly string PokemonDbUrlName = "PokemonDB";

        private readonly Dictionary<string, PokemonAvailability> _availabilities = new();
        private readonly Dictionary<string, PvpTier> _pvpTiers = new();
        private readonly Dictionary<string, ElementalType> _types = new();
        private readonly Dictionary<string, Ability> _abilities = new();

        private readonly Dictionary<string, PokemonSpecies> _species = new();
        private readonly Dictionary<string, PokemonVariety> _varieties = new();

        public PokemonMapper(ISpreadsheetImportReporter reporter) : base(reporter) { }

        protected override Entity Entity => Entity.PokemonForm;

        protected override bool IsValid(PokemonSheetDto dto)
        {
            return
                dto.PokedexNumber != 0 &&
                !string.IsNullOrWhiteSpace(dto.PokemonSpeciesName) &&
                !string.IsNullOrWhiteSpace(dto.PokemonVarietyName) &&
                !string.IsNullOrWhiteSpace(dto.PokemonFormName) &&
                !string.IsNullOrWhiteSpace(dto.ResourceName) &&
                !string.IsNullOrWhiteSpace(dto.AvailabilityName) &&
                !string.IsNullOrWhiteSpace(dto.DefaultVarietyName) &&
                !string.IsNullOrWhiteSpace(dto.DefaultFormName) &&
                !string.IsNullOrWhiteSpace(dto.Type1Name) &&
                !string.IsNullOrWhiteSpace(dto.PrimaryAbilityName) &&
                !string.IsNullOrWhiteSpace(dto.PvpTierName) &&
                dto.Generation != 0 &&
                dto.CatchRate != 0;
        }

        protected override string GetUniqueName(PokemonSheetDto dto)
        {
            return dto.PokemonFormName;
        }

        protected override PokemonForm MapEntity(PokemonSheetDto dto, RowHash rowHash, PokemonForm form = null)
        {
            form ??= new PokemonForm();

            form.IdHash = rowHash.IdHash;
            form.Hash = rowHash.ContentHash;
            form.ImportSheetId = rowHash.ImportSheetId;
            form.PokeApiName = dto.PokemonFormPokeApiName;
            form.SortIndex = dto.SortIndex;
            form.Name = dto.PokemonFormName;
            form.PokemonVariety = MapPokemonVariety(dto, form.PokemonVariety);
            form.Availability = MapAvailability(dto, form.Availability);
            form.SpriteName = dto.SpriteName;

            return form;
        }

        private PokemonVariety MapPokemonVariety(PokemonSheetDto dto, PokemonVariety variety = null)
        {
            variety ??= new PokemonVariety();

            var pvpTier = MapPvpTier(dto, variety.PvpTier);
            var primaryAbility = MapPrimaryAbility(dto, variety.PrimaryAbility);
            var secondaryAbility = MapSecondaryAbility(dto, variety.SecondaryAbility);
            var hiddenAbility = MapHiddenAbility(dto, variety.HiddenAbility);
            var primaryType = MapPrimaryType(dto, variety.PrimaryType);
            var secondaryType = MapSecondaryType(dto, variety.SecondaryType);

            var species = MapPokemonSpecies(dto, variety.PokemonSpecies);

            if (_varieties.ContainsKey(dto.PokemonVarietyName))
            {
                variety = _varieties[dto.PokemonVarietyName];
            }
            else
            {
                variety.ResourceName = dto.ResourceName;
                variety.PokeApiName = dto.PokemonVarietyPokeApiName;
                variety.Name = dto.PokemonVarietyName;
                variety.PokemonSpecies = species;
                variety.Attack = dto.Attack;
                variety.SpecialAttack = dto.SpecialAttack;
                variety.Defense = dto.Defense;
                variety.SpecialDefense = dto.SpecialDefense;
                variety.Speed = dto.Speed;
                variety.HitPoints = dto.HitPoints;
                variety.AttackEv = dto.AttackEvYield;
                variety.SpecialAttackEv = dto.SpecialAttackEvYield;
                variety.DefenseEv = dto.DefenseEvYield;
                variety.SpecialDefenseEv = dto.SpecialDefenseEvYield;
                variety.SpeedEv = dto.SpeedEvYield;
                variety.HitPointsEv = dto.HitPointEvYield;
                variety.PrimaryType = primaryType;
                variety.SecondaryType = secondaryType;
                variety.PrimaryAbility = primaryAbility;
                variety.SecondaryAbility = secondaryAbility;
                variety.HiddenAbility = hiddenAbility;
                variety.PvpTier = pvpTier;
                variety.DoInclude = dto.DoInclude;
                variety.IsMega = dto.IsMega;
                variety.IsFullyEvolved = dto.IsFullyEvolved;
                variety.Generation = dto.Generation;
                variety.CatchRate = dto.CatchRate;
                variety.HasGender = dto.HasGender;
                variety.MaleRatio = dto.MaleRatio;
                variety.EggCycles = dto.EggCycles;
                variety.Height = dto.Height;
                variety.Weight = dto.Weight;
                variety.ExpYield = dto.ExpYield;
                variety.Notes = dto.Notes;

                variety.DefaultForm = MapDefaultForm(dto, variety.DefaultForm);

                _varieties.Add(dto.PokemonVarietyName, variety);
            }

            AddUrls(variety, dto);

            return variety;
        }

        private PokemonSpecies MapPokemonSpecies(PokemonSheetDto dto, PokemonSpecies species = null)
        {
            species ??= new PokemonSpecies();
            if (_species.ContainsKey(dto.PokemonSpeciesName))
            {
                species = _species[dto.PokemonSpeciesName];
            }
            else
            {
                species.PokedexNumber = dto.PokedexNumber;
                species.Name = dto.PokemonSpeciesName;
                species.PokeApiName = dto.PokemonSpeciesPokeApiName;

                species.DefaultVariety = MapDefaultVariety(dto, species.DefaultVariety);

                _species.Add(dto.PokemonSpeciesName, species);
            }

            return species;
        }

        private PokemonAvailability MapAvailability(PokemonSheetDto dto, PokemonAvailability availability = null)
        {
            availability = string.Equals(dto.AvailabilityName, availability?.Name, StringComparison.Ordinal)
                ? availability
                : new PokemonAvailability {Name = dto.AvailabilityName};
            if (!_availabilities.ContainsKey(dto.AvailabilityName))
            {
                _availabilities.Add(dto.AvailabilityName, availability);
            }
            else
            {
                availability = _availabilities[dto.AvailabilityName];
            }

            return availability;
        }

        private PvpTier MapPvpTier(PokemonSheetDto dto, PvpTier pvpTier = null)
        {
            pvpTier = string.Equals(dto.PvpTierName, pvpTier?.Name, StringComparison.Ordinal)
                ? pvpTier
                : new PvpTier {Name = dto.PvpTierName};
            if (!_pvpTiers.ContainsKey(dto.PvpTierName))
            {
                _pvpTiers.Add(dto.PvpTierName, pvpTier);
            }
            else
            {
                pvpTier = _pvpTiers[dto.PvpTierName];
            }

            return pvpTier;
        }

        private Ability MapPrimaryAbility(PokemonSheetDto dto, Ability ability = null)
        {
            ability = string.Equals(dto.PrimaryAbilityName, ability?.Name, StringComparison.Ordinal)
                ? ability
                : new Ability {Name = dto.PrimaryAbilityName};
            if (!_abilities.ContainsKey(dto.PrimaryAbilityName))
            {
                _abilities.Add(dto.PrimaryAbilityName, ability);
            }
            else
            {
                ability = _abilities[dto.PrimaryAbilityName];
            }

            return ability;
        }

        private Ability MapSecondaryAbility(PokemonSheetDto dto, Ability ability = null)
        {
            if (string.IsNullOrWhiteSpace(dto.SecondaryAbilityName))
            {
                return null;
            }

            ability = string.Equals(dto.SecondaryAbilityName, ability?.Name)
                ? ability
                : new Ability {Name = dto.SecondaryAbilityName};
            if (!_abilities.ContainsKey(dto.SecondaryAbilityName))
            {
                _abilities.Add(dto.SecondaryAbilityName, ability);
            }
            else
            {
                ability = _abilities[dto.SecondaryAbilityName];
            }

            return ability;
        }

        private Ability MapHiddenAbility(PokemonSheetDto dto, Ability ability = null)
        {
            if (string.IsNullOrWhiteSpace(dto.HiddenAbilityName))
            {
                return null;
            }

            ability = string.Equals(dto.HiddenAbilityName, ability?.Name, StringComparison.Ordinal)
                ? ability
                : new Ability {Name = dto.HiddenAbilityName};
            if (!_abilities.ContainsKey(dto.HiddenAbilityName))
            {
                _abilities.Add(dto.HiddenAbilityName, ability);
            }
            else
            {
                ability = _abilities[dto.HiddenAbilityName];
            }

            return ability;
        }

        private ElementalType MapPrimaryType(PokemonSheetDto dto, ElementalType type = null)
        {
            type = string.Equals(dto.Type1Name, type?.Name, StringComparison.Ordinal) ? 
                type : new ElementalType {Name = dto.Type1Name};
            if (!_types.ContainsKey(dto.Type1Name))
            {
                _types.Add(dto.Type1Name, type);
            }
            else
            {
                type = _types[dto.Type1Name];
            }

            return type;
        }

        private ElementalType MapSecondaryType(PokemonSheetDto dto, ElementalType type = null)
        {
            if (string.IsNullOrWhiteSpace(dto.Type2Name))
            {
                return null;
            }

            type = string.Equals(dto.Type2Name, type?.Name, StringComparison.Ordinal) ? 
                type : new ElementalType {Name = dto.Type2Name};
            if (!_types.ContainsKey(dto.Type2Name))
            {
                _types.Add(dto.Type2Name, type);
            }
            else
            {
                type = _types[dto.Type2Name];
            }

            return type;
        }

        private PokemonForm MapDefaultForm(PokemonSheetDto dto, PokemonForm form = null)
        {
            return dto.DefaultFormName.EqualsExact(form?.Name) ? form : new PokemonForm {Name = dto.DefaultFormName};
        }

        private PokemonVariety MapDefaultVariety(PokemonSheetDto dto, PokemonVariety variety = null)
        {
            return dto.DefaultVarietyName.EqualsExact(variety?.Name) ? variety : new PokemonVariety {Name = dto.DefaultVarietyName};
        }

        private void AddUrls(PokemonVariety variety, PokemonSheetDto dto)
        {
            if (!string.IsNullOrWhiteSpace(dto.SmogonUrl))
            {
                var url = variety.Urls.SingleOrDefault(u => u.Name.EqualsExact(SmogonUrlName));
                url ??= new PokemonVarietyUrl();
                url.Name = SmogonUrlName;
                url.Url = dto.SmogonUrl;
                url.Variety = variety;
                if (!variety.Urls.Contains(url))
                {
                    variety.Urls.Add(url);
                }
            }
            else
            {
                variety.Urls.RemoveAll(u => u.Name.EqualsExact(SmogonUrlName));
            }

            if (!string.IsNullOrWhiteSpace(dto.BulbapediaUrl))
            {
                var url = variety.Urls.SingleOrDefault(u => u.Name.EqualsExact(BulbapediaUrlName));
                url ??= new PokemonVarietyUrl();
                url.Name = BulbapediaUrlName;
                url.Url = dto.BulbapediaUrl;
                url.Variety = variety;
                if (!variety.Urls.Contains(url))
                {
                    variety.Urls.Add(url);
                }
            }
            else
            {
                variety.Urls.RemoveAll(u => u.Name.EqualsExact(BulbapediaUrlName));
            }

            if (!string.IsNullOrWhiteSpace(dto.PokeoneCommunityUrl))
            {
                var url = variety.Urls.SingleOrDefault(u => u.Name.EqualsExact(PokeoneCommunityUrlName));
                url ??= new PokemonVarietyUrl();
                url.Name = PokeoneCommunityUrlName;
                url.Url = dto.PokeoneCommunityUrl;
                url.Variety = variety;
                if (!variety.Urls.Contains(url))
                {
                    variety.Urls.Add(url);
                }
            }
            else
            {
                variety.Urls.RemoveAll(u => u.Name.EqualsExact(PokeoneCommunityUrlName));
            }

            if (!string.IsNullOrWhiteSpace(dto.PokemonShowdownUrl))
            {
                var url = variety.Urls.SingleOrDefault(u => u.Name.EqualsExact(PokemonShowdownUrlName));
                url ??= new PokemonVarietyUrl();
                url.Name = PokemonShowdownUrlName;
                url.Url = dto.PokemonShowdownUrl;
                url.Variety = variety;
                if (!variety.Urls.Contains(url))
                {
                    variety.Urls.Add(url);
                }
            }
            else
            {
                variety.Urls.RemoveAll(u => u.Name.EqualsExact(PokemonShowdownUrlName));
            }

            if (!string.IsNullOrWhiteSpace(dto.SerebiiUrl))
            {
                var url = variety.Urls.SingleOrDefault(u => u.Name.EqualsExact(SerebiiUrlName));
                url ??= new PokemonVarietyUrl();
                url.Name = SerebiiUrlName;
                url.Url = dto.SerebiiUrl;
                url.Variety = variety;
                if (!variety.Urls.Contains(url))
                {
                    variety.Urls.Add(url);
                }
            }
            else
            {
                variety.Urls.RemoveAll(u => u.Name.EqualsExact(SerebiiUrlName));
            }

            if (!string.IsNullOrWhiteSpace(dto.PokemonDbUrl))
            {
                var url = variety.Urls.SingleOrDefault(u => u.Name.EqualsExact(PokemonDbUrlName));
                url ??= new PokemonVarietyUrl();
                url.Name = PokemonDbUrlName;
                url.Url = dto.PokemonDbUrl;
                url.Variety = variety;
                if (!variety.Urls.Contains(url))
                {
                    variety.Urls.Add(url);
                }
            }
            else
            {
                variety.Urls.RemoveAll(u => u.Name.EqualsExact(PokemonDbUrlName));
            }
        }
    }
}
