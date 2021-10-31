using System;
using Microsoft.Extensions.Logging;
using PokeOneWeb.Data;
using PokeOneWeb.Extensions;
using System.Collections.Generic;
using System.Linq;
using PokeOneWeb.Data.Entities;

namespace PokeOneWeb.Services.GoogleSpreadsheet.Import.Impl.Evolutions
{
    public class EvolutionSheetRepository : ISheetRepository
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly ILogger<EvolutionSheetRepository> _logger;
        private readonly ISheetRowParser<EvolutionSheetDto> _parser;
        private readonly ISpreadsheetEntityMapper<EvolutionSheetDto, Evolution> _mapper;

        public EvolutionSheetRepository(
            ApplicationDbContext dbContext,
            ILogger<EvolutionSheetRepository> logger,
            ISheetRowParser<EvolutionSheetDto> parser,
            ISpreadsheetEntityMapper<EvolutionSheetDto, Evolution> mapper)
        {
            _dbContext = dbContext;
            _logger = logger;
            _parser = parser;
            _mapper = mapper;
        }

        public List<RowHash> ReadDbHashes(ImportSheet sheet)
        {
            var rowHashes = _dbContext.Evolutions
                .Where(x => x.ImportSheetId == sheet.Id)
                .Select(x => new RowHash { ContentHash = x.Hash, IdHash = x.IdHash, ImportSheetId = sheet.Id })
                .ToList();

            return rowHashes;
        }

        public int Delete(List<RowHash> hashes)
        {
            var idHashes = hashes.Select(rh => rh.IdHash).ToList();
            var entities = _dbContext.Evolutions
                .Where(x => idHashes.Contains(x.IdHash));

            _dbContext.Evolutions.RemoveRange(entities);
            _dbContext.SaveChanges();

            return entities.Count();
        }

        public int Insert(Dictionary<RowHash, List<object>> rowData)
        {
            var dtosWithHashes = ReadWithHashes(rowData);
            var entities = _mapper.Map(dtosWithHashes).ToList();

            entities = AttachRelatedEntities(entities);

            _dbContext.Evolutions.AddRange(entities);
            _dbContext.SaveChanges();

            return entities.Count;
        }

        public int Update(Dictionary<RowHash, List<object>> rowData)
        {
            var idHashes = rowData.Keys.Select(rh => rh.IdHash);

            var dtosWithHashes = ReadWithHashes(rowData);
            var entities = _dbContext.Evolutions
                .Where(x => idHashes.Contains(x.IdHash))
                .ToList();

            var updatedEntities = _mapper.MapOnto(entities, dtosWithHashes).ToList();

            AttachRelatedEntities(updatedEntities);

            _dbContext.SaveChanges();

            return updatedEntities.Count;
        }

        private Dictionary<RowHash, EvolutionSheetDto> ReadWithHashes(Dictionary<RowHash, List<object>> rowData)
        {
            return rowData
                .Select(row => new { hash = row.Key, values = _parser.ReadRow(row.Value) })
                .ToDictionary(x => x.hash, x => x.values);
        }

        private List<Evolution> AttachRelatedEntities(List<Evolution> entities)
        {
            var varieties = _dbContext.PokemonVarieties.ToList();
            var species = _dbContext.PokemonSpecies.ToList();

            if (!varieties.Any())
            {
                throw new Exception("Tried to import evolutions, but no" +
                                    "Pokemon Varieties were present in the database.");
            }

            if (!species.Any())
            {
                throw new Exception("Tried to import evolutions, but no" +
                                    "Pokemon Species were present in the database.");
            }

            for (var i = 0; i < entities.Count; i++)
            {
                var entity = entities[i];
                var baseSpecies = species.SingleOrDefault(s => s.Name.EqualsExact(entity.BasePokemonSpecies.Name));

                if (baseSpecies is null)
                {
                    _logger.LogWarning($"Could not find matching PokemonSpecies {entity.BasePokemonSpecies.Name}, skipping evolution.");
                    entities.Remove(entity);
                    i--;
                    continue;
                }

                entity.BasePokemonSpeciesId = baseSpecies.Id;
                entity.BasePokemonSpecies = baseSpecies;

                var baseVariety = varieties.SingleOrDefault(v => v.Name.EqualsExact(entity.BasePokemonVariety.Name));

                if (baseVariety is null)
                {
                    _logger.LogWarning($"Could not find matching base Variety {entity.BasePokemonVariety.Name}, skipping evolution.");
                    entities.Remove(entity);
                    i--;
                    continue;
                }

                entity.BasePokemonVarietyId = baseVariety.Id;
                entity.BasePokemonVariety = baseVariety;

                var evolvedVariety = varieties.SingleOrDefault(v => v.Name.EqualsExact(entity.EvolvedPokemonVariety.Name));

                if (evolvedVariety is null)
                {
                    _logger.LogWarning($"Could not find matching evolved Variety {entity.EvolvedPokemonVariety.Name}, skipping evolution.");
                    entities.Remove(entity);
                    i--;
                    continue;
                }

                entity.EvolvedPokemonVarietyId = evolvedVariety.Id;
                entity.EvolvedPokemonVariety = evolvedVariety;
            }

            return entities;
        }
    }
}
