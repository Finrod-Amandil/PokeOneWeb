using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Logging;
using PokeOneWeb.Data;
using PokeOneWeb.Data.Entities;
using PokeOneWeb.Extensions;

namespace PokeOneWeb.Services.GoogleSpreadsheet.Import.Impl.HuntingConfigurations
{
    public class HuntingConfigurationSheetRepository : ISheetRepository
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly ILogger<HuntingConfigurationSheetRepository> _logger;
        private readonly ISheetRowParser<HuntingConfigurationSheetDto> _parser;
        private readonly ISpreadsheetEntityMapper<HuntingConfigurationSheetDto, HuntingConfiguration> _mapper;

        public HuntingConfigurationSheetRepository(
            ApplicationDbContext dbContext,
            ILogger<HuntingConfigurationSheetRepository> logger,
            ISheetRowParser<HuntingConfigurationSheetDto> parser,
            ISpreadsheetEntityMapper<HuntingConfigurationSheetDto, HuntingConfiguration> mapper)
        {
            _dbContext = dbContext;
            _logger = logger;
            _parser = parser;
            _mapper = mapper;
        }

        public List<RowHash> ReadDbHashes(ImportSheet sheet)
        {
            var rowHashes = _dbContext.HuntingConfigurations
                .Where(x => x.ImportSheetId == sheet.Id)
                .Select(x => new RowHash { ContentHash = x.Hash, IdHash = x.IdHash, ImportSheetId = sheet.Id })
                .ToList();

            return rowHashes;
        }

        public int Delete(List<RowHash> hashes)
        {
            var idHashes = hashes.Select(rh => rh.IdHash).ToList();
            var entities = _dbContext.HuntingConfigurations
                .Where(x => idHashes.Contains(x.IdHash));

            _dbContext.HuntingConfigurations.RemoveRange(entities);
            _dbContext.SaveChanges();

            return entities.Count();
        }

        public int Insert(Dictionary<RowHash, List<object>> rowData)
        {
            var dtosWithHashes = ReadWithHashes(rowData);
            var entities = _mapper.Map(dtosWithHashes).ToList();

            entities = AttachRelatedEntities(entities);

            _dbContext.HuntingConfigurations.AddRange(entities);
            _dbContext.SaveChanges();

            return entities.Count;
        }

        public int Update(Dictionary<RowHash, List<object>> rowData)
        {
            var idHashes = rowData.Keys.Select(rh => rh.IdHash);

            var dtosWithHashes = ReadWithHashes(rowData);
            var entities = _dbContext.HuntingConfigurations
                .Where(x => idHashes.Contains(x.IdHash))
                .ToList();

            var updatedEntities = _mapper.MapOnto(entities, dtosWithHashes).ToList();

            AttachRelatedEntities(updatedEntities);

            _dbContext.SaveChanges();

            return updatedEntities.Count;
        }

        private Dictionary<RowHash, HuntingConfigurationSheetDto> ReadWithHashes(Dictionary<RowHash, List<object>> rowData)
        {
            return rowData
                .Select(row => new { hash = row.Key, values = _parser.ReadRow(row.Value) })
                .ToDictionary(x => x.hash, x => x.values);
        }

        private List<HuntingConfiguration> AttachRelatedEntities(List<HuntingConfiguration> entities)
        {
            var varieties = _dbContext.PokemonVarieties.ToList();
            var natures = _dbContext.Natures.ToList();
            var abilities = _dbContext.Abilities.ToList();

            if (!varieties.Any())
            {
                throw new Exception("Tried to import hunting configuration, but no" +
                                    "Pokemon Varieties were present in the database.");
            }

            if (!natures.Any())
            {
                throw new Exception("Tried to import hunting configuration, but no" +
                                    "natures were present in the database.");
            }

            if (!abilities.Any())
            {
                throw new Exception("Tried to import hunting configuration, but no" +
                                    "abilities were present in the database.");
            }

            for (var i = 0; i < entities.Count; i++)
            {
                var entity = entities[i];

                var variety = varieties.SingleOrDefault(v => v.Name.EqualsExact(entity.PokemonVariety.Name));
                if (variety is null)
                {
                    _logger.LogWarning($"Could not find matching PokemonVariety {entity.PokemonVariety.Name}, skipping hunting configuration.");
                    entities.Remove(entity);
                    i--;
                    continue;
                }

                entity.PokemonVarietyId = variety.Id;
                entity.PokemonVariety = variety;

                var nature = natures.SingleOrDefault(n => n.Name.EqualsExact(entity.Nature.Name));
                if (nature is null)
                {
                    _logger.LogWarning($"Could not find matching Nature {entity.Nature.Name}, skipping hunting configuration.");
                    entities.Remove(entity);
                    i--;
                    continue;
                }

                entity.NatureId = nature.Id;
                entity.Nature = nature;

                var ability = abilities.SingleOrDefault(a => a.Name.EqualsExact(entity.Ability.Name));
                if (ability is null)
                {
                    _logger.LogWarning($"Could not find matching Ability {entity.Ability.Name}, skipping hunting configuration.");
                    entities.Remove(entity);
                    i--;
                    continue;
                }

                entity.AbilityId = ability.Id;
                entity.Ability = ability;
            }

            return entities;
        }
    }
}
