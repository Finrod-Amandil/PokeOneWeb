using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Logging;
using PokeOneWeb.Data;
using PokeOneWeb.Data.Entities;

namespace PokeOneWeb.Services.GoogleSpreadsheet.Import.Impl.MainData.Spawns
{
    public class SpawnImporter : SpreadsheetEntityImporter<SpawnDto, Spawn>
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly ILogger<SpawnImporter> _logger;

        public SpawnImporter(
            ISpreadsheetEntityReader<SpawnDto> reader,
            ISpreadsheetEntityMapper<SpawnDto, Spawn> mapper,
            ApplicationDbContext dbContext, 
            ILogger<SpawnImporter> logger) : base(reader, mapper)
        {
            _dbContext = dbContext;
            _logger = logger;
        }

        protected override string GetSheetPrefix()
        {
            return Constants.SHEET_PREFIX_SPAWNS;
        }

        protected override void WriteToDatabase(IEnumerable<Spawn> newSpawns)
        {
            var locations = _dbContext.Locations.ToList();
            var pokemonForms = _dbContext.PokemonForms.ToList();
            var spawnTypes = _dbContext.SpawnTypes.ToList();
            var seasons = _dbContext.Seasons.ToList();
            var timesOfDay = _dbContext.TimesOfDay.ToList();

            foreach (var newSpawn in newSpawns)
            {
                var location = locations.SingleOrDefault(l => l.Name.Equals(newSpawn.Location.Name, StringComparison.Ordinal));

                if (location is null)
                {
                    _logger.LogWarning($"No unique matching location could be found for Spawn location {newSpawn.Location.Name}. Skipping.");
                    continue;
                }

                var pokemonForm = pokemonForms.SingleOrDefault(p =>
                    p.Name.Equals(newSpawn.PokemonForm.Name, StringComparison.Ordinal));

                if (pokemonForm is null)
                {
                    _logger.LogWarning($"No unique matching pokemon form could be found for Spawn pokemon form {newSpawn.PokemonForm.Name}. Skipping.");
                    continue;
                }

                var spawnType = spawnTypes.SingleOrDefault(s =>
                    s.Name.Equals(newSpawn.SpawnType.Name, StringComparison.Ordinal));

                if (spawnType is null)
                {
                    _logger.LogWarning($"No unique matching spawn type could be found for Spawn type {newSpawn.SpawnType.Name}. Skipping.");
                    continue;
                }

                var newSpawnOpportunities = new List<SpawnOpportunity>(newSpawn.SpawnOpportunities);
                newSpawn.SpawnOpportunities.Clear();

                foreach (var newSpawnOpportunity in newSpawnOpportunities)
                {
                    var season = seasons.SingleOrDefault(s =>
                        s.Abbreviation.Equals(newSpawnOpportunity.Season.Abbreviation, StringComparison.Ordinal));

                    if (season is null)
                    {
                        _logger.LogWarning($"No unique matching season could be found for Spawn opportunity season {newSpawnOpportunity.Season.Abbreviation}. Skipping.");
                        continue;
                    }

                    var timeOfDay = timesOfDay.SingleOrDefault(t =>
                        t.Abbreviation.Equals(newSpawnOpportunity.TimeOfDay.Abbreviation));

                    if (timeOfDay is null)
                    {
                        _logger.LogWarning($"No unique matching time of day could be found for Spawn opportunity time of day {newSpawnOpportunity.TimeOfDay.Abbreviation}. Skipping.");
                        continue;
                    }

                    newSpawnOpportunity.Season = season;
                    newSpawnOpportunity.TimeOfDay = timeOfDay;
                    newSpawn.SpawnOpportunities.Add(newSpawnOpportunity);
                }

                if (newSpawn.SpawnOpportunities.Count == 0)
                {
                    _logger.LogWarning($"No valid spawn opportunities could be mapped for spawn of {newSpawn.PokemonForm.Name} in {newSpawn.Location.Name}. Skipping.");
                    continue;
                }

                newSpawn.Location = location;
                newSpawn.PokemonForm = pokemonForm;
                newSpawn.SpawnType = spawnType;

                _dbContext.Spawns.Add(newSpawn);
            }

            _dbContext.SaveChanges();
        }
    }
}
