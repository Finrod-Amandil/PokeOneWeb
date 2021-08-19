using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Logging;
using PokeOneWeb.Data;
using PokeOneWeb.Data.Entities;
using PokeOneWeb.Extensions;

namespace PokeOneWeb.Services.GoogleSpreadsheet.Import.Impl.Spawns
{
    public class SpawnSheetRepository : ISheetRepository
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly ILogger<SpawnSheetRepository> _logger;
        private readonly ISheetRowParser<SpawnSheetDto> _parser;
        private readonly ISpreadsheetEntityMapper<SpawnSheetDto, Spawn> _mapper;

        public SpawnSheetRepository(
            ApplicationDbContext dbContext,
            ILogger<SpawnSheetRepository> logger,
            ISheetRowParser<SpawnSheetDto> parser,
            ISpreadsheetEntityMapper<SpawnSheetDto, Spawn> mapper)
        {
            _dbContext = dbContext;
            _logger = logger;
            _parser = parser;
            _mapper = mapper;
        }

        public List<RowHash> ReadDbHashes(ImportSheet sheet)
        {
            var rowHashes = _dbContext.Spawns
                .Where(x => x.ImportSheetId == sheet.Id)
                .Select(x => new RowHash { ContentHash = x.Hash, IdHash = x.IdHash, ImportSheetId = sheet.Id })
                .ToList();

            return rowHashes;
        }

        public int Delete(List<RowHash> hashes)
        {
            var idHashes = hashes.Select(rh => rh.IdHash).ToList();
            var entities = _dbContext.Spawns
                .Where(x => idHashes.Contains(x.IdHash));

            _dbContext.Spawns.RemoveRange(entities);
            _dbContext.SaveChanges();

            return entities.Count();
        }

        public int Insert(Dictionary<RowHash, List<object>> rowData)
        {
            var dtosWithHashes = ReadWithHashes(rowData);
            var entities = _mapper.Map(dtosWithHashes).ToList();

            entities = AttachRelatedEntities(entities);

            _dbContext.Spawns.AddRange(entities);
            _dbContext.SaveChanges();

            return entities.Count;
        }

        public int Update(Dictionary<RowHash, List<object>> rowData)
        {
            var idHashes = rowData.Keys.Select(rh => rh.IdHash);

            var dtosWithHashes = ReadWithHashes(rowData);
            var entities = _dbContext.Spawns
                .Where(x => idHashes.Contains(x.IdHash))
                .ToList();

            var updatedEntities = _mapper.MapOnto(entities, dtosWithHashes).ToList();

            AttachRelatedEntities(updatedEntities);

            _dbContext.SaveChanges();

            return updatedEntities.Count;
        }

        private Dictionary<RowHash, SpawnSheetDto> ReadWithHashes(Dictionary<RowHash, List<object>> rowData)
        {
            return rowData
                .Select(row => new { hash = row.Key, values = _parser.ReadRow(row.Value) })
                .ToDictionary(x => x.hash, x => x.values);
        }

        private List<Spawn> AttachRelatedEntities(List<Spawn> entities)
        {
            var spawnTypes = _dbContext.SpawnTypes.ToList();
            var pokemonForms = _dbContext.PokemonForms.ToList();
            var locations = _dbContext.Locations.ToList();
            var seasons = _dbContext.Seasons.ToList();
            var timesOfDay = _dbContext.TimesOfDay.ToList();

            if (!spawnTypes.Any())
            {
                throw new Exception("Tried to import spawns, but no spawn types were present in the database.");
            }

            if (!pokemonForms.Any())
            {
                throw new Exception("Tried to import spawns, but no pokemon forms were present in the database.");
            }

            if (!locations.Any())
            {
                throw new Exception("Tried to import spawns, but no locations were present in the database.");
            }

            if (!seasons.Any())
            {
                throw new Exception("Tried to import spawns, but no seasons were present in the database.");
            }

            if (!timesOfDay.Any())
            {
                throw new Exception("Tried to import spawns, but no times of day were present in the database.");
            }

            for (var i = 0; i < entities.Count; i++)
            {
                var entity = entities[i];

                var spawnType = spawnTypes.SingleOrDefault(s => s.Name.EqualsExact(entity.SpawnType.Name));
                if (spawnType is null)
                {
                    _logger.LogWarning($"Could not find matching SpawnType {entity.SpawnType.Name}, skipping spawn.");
                    entities.Remove(entity);
                    i--;
                    continue;
                }

                entity.SpawnTypeId = spawnType.Id;
                entity.SpawnType = spawnType;

                var pokemonForm = pokemonForms.SingleOrDefault(p => p.Name.EqualsExact(entity.PokemonForm.Name));
                if (pokemonForm is null)
                {
                    _logger.LogWarning($"Could not find matching PokemonForm {entity.PokemonForm.Name}, skipping spawn.");
                    entities.Remove(entity);
                    i--;
                    continue;
                }

                entity.PokemonFormId = pokemonForm.Id;
                entity.PokemonForm = pokemonForm;

                var location = locations.SingleOrDefault(l => l.Name.EqualsExact(entity.Location.Name));
                if (location is null)
                {
                    _logger.LogWarning($"Could not find matching Location {entity.Location.Name}, skipping spawn.");
                    entities.Remove(entity);
                    i--;
                    continue;
                }

                entity.LocationId = location.Id;
                entity.Location = location;

                for (int j = 0; j < entity.SpawnOpportunities.Count; j++)
                {
                    var spawnOpportunity = entity.SpawnOpportunities[j];

                    var season = seasons.SingleOrDefault(s => s.Abbreviation.EqualsExact(spawnOpportunity.Season.Abbreviation));
                    if (season is null)
                    {
                        _logger.LogWarning($"Could not find matching Season {spawnOpportunity.Season.Name}, skipping spawn opportunity.");
                        entity.SpawnOpportunities.Remove(spawnOpportunity);
                        j--;
                        continue;
                    }
                    spawnOpportunity.Season = season;

                    var timeOfDay = timesOfDay.SingleOrDefault(t => t.Abbreviation.EqualsExact(spawnOpportunity.TimeOfDay.Abbreviation));
                    if (timeOfDay is null)
                    {
                        _logger.LogWarning($"Could not find matching TimeOfDay {spawnOpportunity.TimeOfDay.Name}, skipping spawn opportunity.");
                        entity.SpawnOpportunities.Remove(spawnOpportunity);
                        j--;
                        continue;
                    }
                    spawnOpportunity.TimeOfDay = timeOfDay;
                }

                if (!entity.SpawnOpportunities.Any())
                {
                    _logger.LogWarning("Spawn had no valid spawn opportunities, skipping spawn.");
                    entities.Remove(entity);
                    i--;
                }
            }

            return entities;
        }
    }
}
