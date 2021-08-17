using Microsoft.Extensions.Logging;
using PokeOneWeb.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using PokeOneWeb.Extensions;

namespace PokeOneWeb.Services.GoogleSpreadsheet.Import.Impl.Pokemon
{
    public class PokemonMapper : ISpreadsheetEntityMapper<PokemonSheetDto, PokemonForm>
    {
        private readonly string SmogonUrlName = "Smogon";
        private readonly string BulbapediaUrlName = "Bulbapedia";
        private readonly string PokeoneCommunityUrlName = "PokeoneCommunity";
        private readonly string PokemonShowdownUrlName = "Pokemon Showdown";
        private readonly string SerebiiUrlName = "Serebii";
        private readonly string PokemonDbUrlName = "PokemonDB";


        private readonly ILogger<PokemonMapper> _logger;

        private IDictionary<string, PokemonAvailability> _availabilities;
        private IDictionary<string, PvpTier> _pvpTiers;
        private IDictionary<string, ElementalType> _types;
        private IDictionary<string, Ability> _abilities;

        private IDictionary<string, PokemonSpecies> _species;
        private IDictionary<string, PokemonVariety> _varieties;

        public PokemonMapper(ILogger<PokemonMapper> logger)
        {
            _logger = logger;
        }

        public IEnumerable<PokemonForm> Map(IDictionary<RowHash, PokemonSheetDto> dtosWithHashes)
        {
            if (dtosWithHashes is null)
            {
                throw new ArgumentNullException(nameof(dtosWithHashes));
            }

            ResetLists();

            foreach (var (rowHash, dto) in dtosWithHashes)
            {
                if (!IsValid(dto))
                {
                    _logger.LogWarning($"Found invalid Pokemon DTO with form name {dto.PokemonFormName}. Skipping.");
                    continue;
                }

                yield return MapPokemonForm(dto, rowHash);
            }
        }

        public IEnumerable<PokemonForm> MapOnto(IList<PokemonForm> entities, IDictionary<RowHash, PokemonSheetDto> dtosWithHashes)
        {
            if (dtosWithHashes is null)
            {
                throw new ArgumentNullException(nameof(dtosWithHashes));
            }

            ResetLists();

            foreach (var (rowHash, dto) in dtosWithHashes)
            {
                if (!IsValid(dto))
                {
                    _logger.LogWarning($"Found invalid Pokemon DTO with Hash {rowHash.IdHash}. Skipping.");
                    continue;
                }

                var matchingEntity = entities.SingleOrDefault(x => x.IdHash.EqualsExact(rowHash.IdHash));

                if (matchingEntity is null)
                {
                    _logger.LogWarning($"Found no single matching PokemonForm entity for ID hash {rowHash.IdHash}. Skipping.");
                    continue;
                }

                MapPokemonForm(dto, rowHash, matchingEntity);
            }

            return entities;
        }

        private void ResetLists()
        {
            _availabilities = new Dictionary<string, PokemonAvailability>();
            _pvpTiers = new Dictionary<string, PvpTier>();
            _types = new Dictionary<string, ElementalType>();
            _abilities = new Dictionary<string, Ability>();

            _species = new Dictionary<string, PokemonSpecies>();
            _varieties = new Dictionary<string, PokemonVariety>();
        }

        private bool IsValid(PokemonSheetDto dto)
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

        private PokemonForm MapPokemonForm(PokemonSheetDto dto, RowHash rowHash, PokemonForm form = null)
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
            availability ??= new PokemonAvailability { Name = dto.AvailabilityName };
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
            pvpTier ??= new PvpTier { Name = dto.PvpTierName };
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
            ability ??= new Ability { Name = dto.PrimaryAbilityName };
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

            ability ??= new Ability { Name = dto.SecondaryAbilityName };
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

            ability ??= new Ability { Name = dto.HiddenAbilityName };
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
            type ??= new ElementalType { Name = dto.Type1Name };
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

            type ??= new ElementalType { Name = dto.Type2Name };
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
            return form ?? new PokemonForm { Name = dto.DefaultFormName };
        }

        private PokemonVariety MapDefaultVariety(PokemonSheetDto dto, PokemonVariety variety = null)
        {
            return variety ?? new PokemonVariety { Name = dto.DefaultVarietyName };
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
