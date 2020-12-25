using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Logging;
using PokeOneWeb.Data;
using PokeOneWeb.Data.Entities;

namespace PokeOneWeb.Services.GoogleSpreadsheet.Import.Impl.MainData.HuntingConfigurations
{
    public class HuntingConfigurationImporter : SpreadsheetEntityImporter<HuntingConfigurationDto, HuntingConfiguration>
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly ILogger<HuntingConfigurationImporter> _logger;

        public HuntingConfigurationImporter(
            ISpreadsheetEntityReader<HuntingConfigurationDto> reader,
            ISpreadsheetEntityMapper<HuntingConfigurationDto, HuntingConfiguration> mapper,
            ApplicationDbContext dbContext,
            ILogger<HuntingConfigurationImporter> logger) : base(reader, mapper)
        {
            _dbContext = dbContext;
            _logger = logger;
        }

        protected override string GetSheetPrefix()
        {
            return Constants.SHEET_PREFIX_HUNTINGCONFIGURATIONS;
        }

        protected override void WriteToDatabase(IEnumerable<HuntingConfiguration> newHuntingConfigurations)
        {
            var pokemonVarieties = _dbContext.PokemonVarieties.ToList();
            var natures = _dbContext.Natures.ToList();
            var abilities = _dbContext.Abilities.ToList();

            foreach (var huntingConfiguration in newHuntingConfigurations)
            {
                var pokemonVariety = pokemonVarieties
                    .SingleOrDefault(p => p.Name.Equals(huntingConfiguration.PokemonVariety.Name));
                if (pokemonVariety is null)
                {
                    _logger.LogWarning($"No unique matching pokemon variety could be found for pokemon " +
                                       $"variety name {huntingConfiguration.PokemonVariety.Name}. Skipping.");
                    continue;
                }

                var nature = natures
                    .SingleOrDefault(n => n.Name.Equals(huntingConfiguration.Nature.Name));
                if (nature is null)
                {
                    _logger.LogWarning($"No unique matching nature could be found for nature " +
                                       $"name {huntingConfiguration.Nature.Name}. Skipping.");
                    continue;
                }

                var ability = abilities
                    .SingleOrDefault(a => a.Name.Equals(huntingConfiguration.Ability.Name));
                if (ability is null)
                {
                    _logger.LogWarning($"No unique matching ability could be found for ability " +
                                       $"name {huntingConfiguration.Ability.Name}. Skipping.");
                    continue;
                }

                huntingConfiguration.PokemonVariety = pokemonVariety;
                huntingConfiguration.PokemonVarietyId = pokemonVariety.Id;
                huntingConfiguration.Nature = nature;
                huntingConfiguration.NatureId = nature.Id;
                huntingConfiguration.Ability = ability;
                huntingConfiguration.AbilityId = ability.Id;

                _dbContext.HuntingConfigurations.Add(huntingConfiguration);
            }

            _dbContext.SaveChanges();
        }
    }
}
