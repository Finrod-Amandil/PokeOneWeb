using System;
using Microsoft.Extensions.Logging;
using PokeOneWeb.Data;
using PokeOneWeb.Data.Entities;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace PokeOneWeb.Services.DataUpdate.Impl
{
    public class SpawnUpdateService : IDataUpdateService<Spawn>
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly ILogger<SpawnUpdateService> _logger;

        public SpawnUpdateService(ApplicationDbContext dbContext, ILogger<SpawnUpdateService> logger)
        {
            _dbContext = dbContext;
            _logger = logger;
        }

        public void Update(IEnumerable<Spawn> newSpawns)
        {
            //Remove all spawns (and SpawnOpportunities, by cascade), and spawn types.
            _dbContext.Spawns.RemoveRange(_dbContext.Spawns);
            _dbContext.SpawnTypes.RemoveRange(_dbContext.SpawnTypes);
            _dbContext.Database.ExecuteSqlRaw("DBCC CHECKIDENT ('PokeOneWeb.dbo.Spawn',RESEED, 0)");
            _dbContext.Database.ExecuteSqlRaw("DBCC CHECKIDENT ('PokeOneWeb.dbo.SpawnOpportunity',RESEED, 0)");
            _dbContext.Database.ExecuteSqlRaw("DBCC CHECKIDENT ('PokeOneWeb.dbo.SpawnType',RESEED, 0)");

            _dbContext.SaveChanges();

            var locations = _dbContext.Locations.ToList();
            var pokemonForms = _dbContext.PokemonForms.ToList();
            var seasons = _dbContext.Seasons.ToList();
            var timesOfDay = _dbContext.TimesOfDay.ToList();

            var spawnTypes = newSpawns.Select(s => s.SpawnType).Distinct().ToList();
            _dbContext.AddRange(spawnTypes);

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
                    newSpawnOpportunity.SeasonId = season.Id;
                    newSpawnOpportunity.TimeOfDay = timeOfDay;
                    newSpawnOpportunity.TimeOfDayId = timeOfDay.Id;
                    newSpawn.SpawnOpportunities.Add(newSpawnOpportunity);
                }

                if (newSpawn.SpawnOpportunities.Count == 0)
                {
                    _logger.LogWarning($"No valid spawn opportunities could be mapped for spawn of {newSpawn.PokemonForm.Name} in {newSpawn.Location.Name}. Skipping.");
                    continue;
                }

                newSpawn.Location = location;
                newSpawn.LocationId = location.Id;
                newSpawn.PokemonForm = pokemonForm;
                newSpawn.PokemonFormId = pokemonForm.Id;

                _dbContext.Spawns.Add(newSpawn);
            }

            _dbContext.SaveChanges();
        }
    }
}
