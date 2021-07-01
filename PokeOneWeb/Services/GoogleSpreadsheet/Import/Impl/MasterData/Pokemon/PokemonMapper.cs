using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Logging;
using PokeOneWeb.Data.Entities;

namespace PokeOneWeb.Services.GoogleSpreadsheet.Import.Impl.MasterData.Pokemon
{
    public class PokemonMapper : ISpreadsheetEntityMapper<PokemonDto, PokemonForm>
    {
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

        public IEnumerable<PokemonForm> Map(IEnumerable<PokemonDto> dtos)
        {
            if (dtos is null)
            {
                throw new ArgumentNullException(nameof(dtos));
            }

            _availabilities = new Dictionary<string, PokemonAvailability>();
            _pvpTiers = new Dictionary<string, PvpTier>();
            _types = new Dictionary<string, ElementalType>();
            _abilities = new Dictionary<string, Ability>();

            _species = new Dictionary<string, PokemonSpecies>();
            _varieties= new Dictionary<string, PokemonVariety>();

            var pokemonForms = new List<PokemonForm>();

            foreach (var dto in dtos)
            {
                if (!IsValid(dto))
                {
                    _logger.LogWarning($"Found invalid Pokemon DTO with form name {dto.PokemonFormName}. Skipping.");
                    continue;
                }

                var availability = MapAvailability(dto);
                var pvpTier = MapPvpTier(dto);
                var primaryAbility = MapPrimaryAbility(dto);
                var secondaryAbility = MapSecondaryAbility(dto);
                var hiddenAbility = MapHiddenAbility(dto);
                var primaryType = MapPrimaryType(dto);
                var secondaryType = MapSecondaryType(dto);

                PokemonSpecies species;
                if (_species.ContainsKey(dto.PokemonSpeciesName))
                {
                    species = _species[dto.PokemonSpeciesName];
                }
                else
                {
                    species = new PokemonSpecies
                    {
                        PokedexNumber = dto.PokedexNumber,
                        Name = dto.PokemonSpeciesName,
                        PokeApiName = dto.PokemonSpeciesPokeApiName,
                    };
                    _species.Add(dto.PokemonSpeciesName, species);
                }

                PokemonVariety variety;
                if (_varieties.ContainsKey(dto.PokemonVarietyName))
                {
                    variety = _varieties[dto.PokemonVarietyName];
                }
                else
                {
                    variety = new PokemonVariety
                    {
                        ResourceName = dto.ResourceName,
                        PokeApiName = dto.PokemonVarietyPokeApiName,
                        Name = dto.PokemonVarietyName,
                        PokemonSpecies = species,
                        BaseStats = new Stats
                        {
                            Attack = dto.Attack,
                            SpecialAttack = dto.SpecialAttack,
                            Defense = dto.Defense,
                            SpecialDefense = dto.SpecialDefense,
                            Speed = dto.Speed,
                            HitPoints = dto.HitPoints
                        },
                        EvYield = new Stats
                        {
                            Attack = dto.AttackEvYield,
                            SpecialAttack = dto.SpecialAttackEvYield,
                            Defense = dto.DefenseEvYield,
                            SpecialDefense = dto.SpecialDefenseEvYield,
                            Speed = dto.SpeedEvYield,
                            HitPoints = dto.HitPointEvYield
                        },
                        PrimaryType = primaryType,
                        SecondaryType = secondaryType,
                        PrimaryAbility = primaryAbility,
                        SecondaryAbility = secondaryAbility,
                        HiddenAbility = hiddenAbility,
                        PvpTier = pvpTier,
                        DoInclude = dto.DoInclude,
                        IsMega = dto.IsMega,
                        IsFullyEvolved = dto.IsFullyEvolved,
                        Generation = dto.Generation,
                        CatchRate = dto.CatchRate,
                        SmogonUrl = dto.SmogonUrl,
                        BulbapediaUrl = dto.BulbapediaUrl,
                        PokeOneCommunityUrl = dto.PokeoneCommunityUrl,
                        PokemonShowDownUrl = dto.PokemonShowdownUrl,
                        SerebiiUrl = dto.SerebiiUrl,
                        PokemonDbUrl = dto.PokemonDbUrl,
                        Notes = dto.Notes
                    };
                    _varieties.Add(dto.PokemonVarietyName, variety);
                }

                pokemonForms.Add(new PokemonForm
                {
                    PokeApiName = dto.PokemonFormPokeApiName,
                    SortIndex = dto.SortIndex,
                    Name = dto.PokemonFormName,
                    PokemonVariety = variety,
                    Availability = availability,
                    SpriteName = dto.SpriteName
                });
            }

            //Attach default forms / varieties
            foreach (var dto in dtos)
            {
                var pokemonForm = pokemonForms.Single(p =>
                    p.Name.Equals(dto.PokemonFormName, StringComparison.Ordinal));

                var defaultVariety = _varieties.Values.SingleOrDefault(p =>
                    p.Name.Equals(dto.DefaultVarietyName, StringComparison.Ordinal));

                if (defaultVariety is null)
                {
                    _logger.LogWarning($"Could not find default variety for species {dto.PokemonSpeciesName}.");
                    pokemonForms.Remove(pokemonForm);
                    continue;
                }

                var defaultForm = pokemonForms.SingleOrDefault(p =>
                    p.Name.Equals(dto.DefaultFormName, StringComparison.Ordinal));

                if (defaultForm is null)
                {
                    _logger.LogWarning($"Could not find default form for variety {dto.PokemonVarietyName}.");
                    pokemonForms.Remove(pokemonForm);
                    continue;
                }

                pokemonForm.PokemonVariety.PokemonSpecies.DefaultVariety = defaultVariety;
                pokemonForm.PokemonVariety.DefaultForm = defaultForm;
            }

            return pokemonForms;
        }

        private bool IsValid(PokemonDto dto)
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

        private PokemonAvailability MapAvailability(PokemonDto dto)
        {
            PokemonAvailability availability;
            if (!_availabilities.ContainsKey(dto.AvailabilityName))
            {
                availability = new PokemonAvailability { Name = dto.AvailabilityName };
                _availabilities.Add(dto.AvailabilityName, availability);
            }
            else
            {
                availability = _availabilities[dto.AvailabilityName];
            }

            return availability;
        }

        private PvpTier MapPvpTier(PokemonDto dto)
        {
            PvpTier pvpTier;
            if (!_pvpTiers.ContainsKey(dto.PvpTierName))
            {
                pvpTier = new PvpTier { Name = dto.PvpTierName };
                _pvpTiers.Add(dto.PvpTierName, pvpTier);
            }
            else
            {
                pvpTier = _pvpTiers[dto.PvpTierName];
            }

            return pvpTier;
        }

        private Ability MapPrimaryAbility(PokemonDto dto)
        {
            Ability ability;
            if (!_abilities.ContainsKey(dto.PrimaryAbilityName))
            {
                ability = new Ability { Name = dto.PrimaryAbilityName };
                _abilities.Add(dto.PrimaryAbilityName, ability);
            }
            else
            {
                ability = _abilities[dto.PrimaryAbilityName];
            }

            return ability;
        }

        private Ability MapSecondaryAbility(PokemonDto dto)
        {
            if (string.IsNullOrWhiteSpace(dto.SecondaryAbilityName))
            {
                return null;
            }

            Ability ability;
            if (!_abilities.ContainsKey(dto.SecondaryAbilityName))
            {
                ability = new Ability { Name = dto.SecondaryAbilityName };
                _abilities.Add(dto.SecondaryAbilityName, ability);
            }
            else
            {
                ability = _abilities[dto.SecondaryAbilityName];
            }

            return ability;
        }

        private Ability MapHiddenAbility(PokemonDto dto)
        {
            if (string.IsNullOrWhiteSpace(dto.HiddenAbilityName))
            {
                return null;
            }

            Ability ability;
            if (!_abilities.ContainsKey(dto.HiddenAbilityName))
            {
                ability = new Ability { Name = dto.HiddenAbilityName };
                _abilities.Add(dto.HiddenAbilityName, ability);
            }
            else
            {
                ability = _abilities[dto.HiddenAbilityName];
            }

            return ability;
        }

        private ElementalType MapPrimaryType(PokemonDto dto)
        {
            ElementalType type;
            if (!_types.ContainsKey(dto.Type1Name))
            {
                type = new ElementalType { Name = dto.Type1Name };
                _types.Add(dto.Type1Name, type);
            }
            else
            {
                type = _types[dto.Type1Name];
            }

            return type;
        }

        private ElementalType MapSecondaryType(PokemonDto dto)
        {
            if (string.IsNullOrWhiteSpace(dto.Type2Name))
            {
                return null;
            }

            ElementalType type;
            if (!_types.ContainsKey(dto.Type2Name))
            {
                type = new ElementalType { Name = dto.Type2Name };
                _types.Add(dto.Type2Name, type);
            }
            else
            {
                type = _types[dto.Type2Name];
            }

            return type;
        }
    }
}
