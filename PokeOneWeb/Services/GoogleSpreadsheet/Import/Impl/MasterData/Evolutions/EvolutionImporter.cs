using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Logging;
using PokeOneWeb.Data;
using PokeOneWeb.Data.Entities;

namespace PokeOneWeb.Services.GoogleSpreadsheet.Import.Impl.MasterData.Evolutions
{
    public class EvolutionImporter : SpreadsheetEntityImporter<EvolutionDto, Evolution>
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly ILogger<EvolutionImporter> _logger;

        public EvolutionImporter(
            ISpreadsheetEntityReader<EvolutionDto> reader, 
            ISpreadsheetEntityMapper<EvolutionDto, Evolution> mapper,
            ApplicationDbContext dbContext,
            ILogger<EvolutionImporter> logger) : base(reader, mapper)
        {
            _dbContext = dbContext;
            _logger = logger;
        }

        protected override string GetSheetPrefix()
        {
            return Constants.SHEET_PREFIX_EVOLUTIONS;
        }

        protected override void WriteToDatabase(IEnumerable<Evolution> entities)
        {
            var species = _dbContext.PokemonSpecies.ToList();
            var varieties = _dbContext.PokemonVarieties.ToList();

            if (!species.Any() || !species.Any())
            {
                throw new Exception("Tried to import Placed items, but no locations or no items were" +
                                    "present in the database.");
            }

            foreach (var evolution in entities)
            {
                var baseSpecies = species.SingleOrDefault(s =>
                    s.Name.Equals(evolution.BasePokemonSpecies.Name, StringComparison.Ordinal));

                if (baseSpecies is null)
                {
                    _logger.LogWarning($"No unique matching species could be found for Evolution " +
                                       $"base species {evolution.BasePokemonSpecies.Name}. Skipping.");
                    continue;
                }

                var baseVariety = varieties.SingleOrDefault(v =>
                    v.Name.Equals(evolution.BasePokemonVariety.Name, StringComparison.Ordinal));

                if (baseVariety is null)
                {
                    _logger.LogWarning($"No unique matching variety could be found for Evolution " +
                                       $"base variety {evolution.BasePokemonVariety.Name}. Skipping.");
                    continue;
                }

                var evolvedVariety = varieties.SingleOrDefault(v =>
                    v.Name.Equals(evolution.EvolvedPokemonVariety.Name, StringComparison.Ordinal));

                if (evolvedVariety is null)
                {
                    _logger.LogWarning($"No unique matching variety could be found for Evolution " +
                                       $"evolved variety {evolution.EvolvedPokemonVariety.Name}. Skipping.");
                    continue;
                }

                evolution.BasePokemonSpecies = baseSpecies;
                evolution.BasePokemonVariety = baseVariety;
                evolution.EvolvedPokemonVariety = evolvedVariety;

                _dbContext.Evolutions.AddRange(evolution);
            }

            _dbContext.SaveChanges();
        }
    }
}
