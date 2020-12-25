using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Logging;
using PokeOneWeb.Data;
using PokeOneWeb.Data.Entities;

namespace PokeOneWeb.Services.GoogleSpreadsheet.Import.Impl.MasterData.Pokemon
{
    public class PokemonImporter : SpreadsheetEntityImporter<PokemonDto, PokemonForm>
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly ILogger<PokemonImporter> _logger;

        public PokemonImporter(
            ISpreadsheetEntityReader<PokemonDto> reader, 
            ISpreadsheetEntityMapper<PokemonDto, PokemonForm> mapper,
            ApplicationDbContext dbContext,
            ILogger<PokemonImporter> logger) : base(reader, mapper)
        {
            _dbContext = dbContext;
            _logger = logger;
        }

        protected override string GetSheetPrefix()
        {
            return Constants.SHEET_PREFIX_POKEMON;
        }

        protected override void WriteToDatabase(IEnumerable<PokemonForm> entities)
        {
            var availabilities = _dbContext.PokemonAvailabilities.ToList();
            var pvpTiers = _dbContext.PvpTiers.ToList();
            var abilities = _dbContext.Abilities.ToList();
            var types = _dbContext.ElementalTypes.ToList();

            if (!availabilities.Any() || !pvpTiers.Any() || !abilities.Any() || !types.Any())
            {
                throw new Exception("Tried to import Pokemon, but no availabilities, no pvp tiers, " +
                                    "no abilities or no types were present in the database.");
            }

            var defaultVarieties = new Dictionary<PokemonSpecies, PokemonVariety>();
            var defaultForms = new Dictionary<PokemonVariety, PokemonForm>();

            foreach (var pokemonForm in entities)
            {
                var availability = availabilities.SingleOrDefault(a =>
                    a.Name.Equals(pokemonForm.Availability.Name, StringComparison.Ordinal));

                if (availability is null)
                {
                    _logger.LogWarning($"No unique matching availability could be found for Pokemon " +
                                       $"availability {pokemonForm.Availability.Name}. Skipping.");
                    continue;
                }

                var pvpTier = pvpTiers.SingleOrDefault(p =>
                    p.Name.Equals(pokemonForm.PokemonVariety.PvpTier.Name, StringComparison.Ordinal));

                if (pvpTier is null)
                {
                    _logger.LogWarning($"No unique matching pvp tier could be found for Pokemon " +
                                       $"pvp tier {pokemonForm.PokemonVariety.PvpTier.Name}. Skipping.");
                    continue;
                }

                var primaryAbility = abilities.SingleOrDefault(a =>
                    a.Name.Equals(pokemonForm.PokemonVariety.PrimaryAbility.Name, StringComparison.Ordinal));

                if (primaryAbility is null)
                {
                    _logger.LogWarning($"No unique matching ability could be found for Pokemon " +
                                       $"primary ability {pokemonForm.PokemonVariety.PrimaryAbility.Name}. Skipping.");
                    continue;
                }

                Ability secondaryAbility = null;
                if (pokemonForm.PokemonVariety.SecondaryAbility != null)
                {
                    secondaryAbility = abilities.SingleOrDefault(a =>
                        a.Name.Equals(pokemonForm.PokemonVariety.SecondaryAbility.Name, StringComparison.Ordinal));

                    if (secondaryAbility is null)
                    {
                        _logger.LogWarning($"No unique matching ability could be found for Pokemon " +
                                           $"secondary ability {pokemonForm.PokemonVariety.SecondaryAbility.Name}. Skipping.");
                        continue;
                    }
                }

                Ability hiddenAbility = null;
                if (pokemonForm.PokemonVariety.HiddenAbility != null)
                {
                    hiddenAbility = abilities.SingleOrDefault(a =>
                        a.Name.Equals(pokemonForm.PokemonVariety.HiddenAbility.Name, StringComparison.Ordinal));

                    if (hiddenAbility is null)
                    {
                        _logger.LogWarning($"No unique matching ability could be found for Pokemon " +
                                           $"hidden ability {pokemonForm.PokemonVariety.HiddenAbility.Name}. Skipping.");
                        continue;
                    }
                }

                var primaryType = types.SingleOrDefault(t =>
                    t.Name.Equals(pokemonForm.PokemonVariety.PrimaryType.Name, StringComparison.Ordinal));

                if (primaryType is null)
                {
                    _logger.LogWarning($"No unique matching type could be found for Pokemon " +
                                       $"primary type {pokemonForm.PokemonVariety.PrimaryType.Name}. Skipping.");
                    continue;
                }

                ElementalType secondaryType = null;
                if (pokemonForm.PokemonVariety.SecondaryType != null)
                {
                    secondaryType = types.SingleOrDefault(t =>
                        t.Name.Equals(pokemonForm.PokemonVariety.SecondaryType.Name, StringComparison.Ordinal));

                    if (secondaryType is null)
                    {
                        _logger.LogWarning($"No unique matching type could be found for Pokemon " +
                                           $"secondary type {pokemonForm.PokemonVariety.SecondaryType.Name}. Skipping.");
                        continue;
                    }
                }

                pokemonForm.Availability = availability;
                pokemonForm.PokemonVariety.PvpTier = pvpTier;
                pokemonForm.PokemonVariety.PrimaryAbility = primaryAbility;
                pokemonForm.PokemonVariety.SecondaryAbility = secondaryAbility;
                pokemonForm.PokemonVariety.HiddenAbility = hiddenAbility;
                pokemonForm.PokemonVariety.PrimaryType = primaryType;
                pokemonForm.PokemonVariety.SecondaryType = secondaryType;

                //Remove default varieties and forms to avoid cyclic reference (cf. "Favored child problem")
                if (!defaultVarieties.ContainsKey(pokemonForm.PokemonVariety.PokemonSpecies))
                {
                    defaultVarieties.Add(pokemonForm.PokemonVariety.PokemonSpecies, pokemonForm.PokemonVariety.PokemonSpecies.DefaultVariety);
                }

                if (!defaultForms.ContainsKey(pokemonForm.PokemonVariety))
                {
                    defaultForms.Add(pokemonForm.PokemonVariety, pokemonForm.PokemonVariety.DefaultForm);
                }

                pokemonForm.PokemonVariety.PokemonSpecies.DefaultVariety = null;
                pokemonForm.PokemonVariety.DefaultForm = null;

                _dbContext.PokemonForms.AddRange(pokemonForm);
            }

            _dbContext.SaveChanges();

            foreach (var (species, defaultVariety) in defaultVarieties)
            {
                species.DefaultVariety = defaultVariety;
            }

            foreach (var (variety, defaultForm) in defaultForms)
            {
                variety.DefaultForm = defaultForm;
            }

            _dbContext.SaveChanges();
        }
    }
}
