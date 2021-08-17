using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Logging;
using PokeOneWeb.Data;
using PokeOneWeb.Data.Entities;
using PokeOneWeb.Extensions;
using Z.EntityFramework.Plus;

namespace PokeOneWeb.Services.GoogleSpreadsheet.Import.Impl.ItemStatBoosts
{
    public class ItemStatBoostSheetRepository : ISheetRepository
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly ILogger<ItemStatBoostSheetRepository> _logger;
        private readonly ISheetRowParser<ItemStatBoostSheetDto> _parser;
        private readonly ISpreadsheetEntityMapper<ItemStatBoostSheetDto, ItemStatBoostPokemon> _mapper;

        public ItemStatBoostSheetRepository(
            ApplicationDbContext dbContext,
            ILogger<ItemStatBoostSheetRepository> logger,
            ISheetRowParser<ItemStatBoostSheetDto> parser,
            ISpreadsheetEntityMapper<ItemStatBoostSheetDto, ItemStatBoostPokemon> mapper)
        {
            _dbContext = dbContext;
            _logger = logger;
            _parser = parser;
            _mapper = mapper;
        }

        public List<RowHash> ReadDbHashes(ImportSheet sheet)
        {
            var rowHashes = _dbContext.ItemStatBoostPokemon
                .Where(x => x.ImportSheetId == sheet.Id)
                .Select(x => new RowHash { ContentHash = x.Hash, IdHash = x.IdHash, ImportSheetId = sheet.Id })
                .ToList();

            return rowHashes;
        }

        public int Delete(List<RowHash> hashes)
        {
            var idHashes = hashes.Select(rh => rh.IdHash).ToList();
            var entities = _dbContext.ItemStatBoostPokemon
                .Where(x => idHashes.Contains(x.IdHash));

            _dbContext.ItemStatBoostPokemon.RemoveRange(entities);
            _dbContext.SaveChanges();

            return entities.Count();
        }

        public int Insert(Dictionary<RowHash, List<object>> rowData)
        {
            var dtosWithHashes = ReadWithHashes(rowData);
            var entities = _mapper.Map(dtosWithHashes).ToList();

            entities = AttachRelatedEntities(entities);

            _dbContext.ItemStatBoostPokemon.AddRange(entities);
            _dbContext.SaveChanges();

            return entities.Count;
        }

        public int Update(Dictionary<RowHash, List<object>> rowData)
        {
            var idHashes = rowData.Keys.Select(rh => rh.IdHash);

            var dtosWithHashes = ReadWithHashes(rowData);
            var entities = _dbContext.ItemStatBoostPokemon
                .Where(x => idHashes.Contains(x.IdHash))
                .ToList();

            var updatedEntities = _mapper.MapOnto(entities, dtosWithHashes).ToList();

            AttachRelatedEntities(updatedEntities);

            _dbContext.SaveChanges();

            return updatedEntities.Count;
        }

        private Dictionary<RowHash, ItemStatBoostSheetDto> ReadWithHashes(Dictionary<RowHash, List<object>> rowData)
        {
            return rowData
                .Select(row => new { hash = row.Key, values = _parser.ReadRow(row.Value) })
                .ToDictionary(x => x.hash, x => x.values);
        }

        private List<ItemStatBoostPokemon> AttachRelatedEntities(List<ItemStatBoostPokemon> entities)
        {
            var varieties = _dbContext.PokemonVarieties.ToList();
            var items = _dbContext.Items.ToList();
            var itemStatBoosts = _dbContext.ItemStatBoosts
                .IncludeOptimized(i => i.Item)
                .ToList();

            if (!varieties.Any())
            {
                throw new Exception("Tried to import ïtem stat boosts, but no" +
                                    "Pokemon Varieties were present in the database.");
            }

            for (var i = 0; i < entities.Count; i++)
            {
                var entity = entities[i];

                // Pokemon Variety (optional)
                if (entity.PokemonVariety != null)
                {
                    var pokemonVariety = varieties.SingleOrDefault(v => v.Name.EqualsExact(entity.PokemonVariety.Name));

                    if (pokemonVariety is null)
                    {
                        _logger.LogWarning($"Could not find matching variety {entity.PokemonVariety.Name}, skipping item stat boost pokemon.");
                        entities.Remove(entity);
                        i--;
                        continue;
                    }

                    entity.PokemonVarietyId = pokemonVariety.Id;
                    entity.PokemonVariety = pokemonVariety;
                }

                // Item Stat boost, may already exist --> update, else insert
                var existingItemStatBoost = itemStatBoosts
                    .SingleOrDefault(i => i.Item.Name.EqualsExact(entity.ItemStatBoost.Item.Name));

                if (existingItemStatBoost != null)
                {
                    existingItemStatBoost.ItemId = entity.ItemStatBoost.Item.Id;
                    existingItemStatBoost.Item = entity.ItemStatBoost.Item;
                    existingItemStatBoost.AttackBoost = entity.ItemStatBoost.AttackBoost;
                    existingItemStatBoost.SpecialAttackBoost = entity.ItemStatBoost.SpecialAttackBoost;
                    existingItemStatBoost.DefenseBoost = entity.ItemStatBoost.SpecialDefenseBoost;
                    existingItemStatBoost.SpeedBoost = entity.ItemStatBoost.SpeedBoost;
                    existingItemStatBoost.HitPointsBoost = entity.ItemStatBoost.HitPointsBoost;

                    entity.ItemStatBoostId = existingItemStatBoost.Id;
                    entity.ItemStatBoost = existingItemStatBoost;
                }

                // Item
                var item = items.SingleOrDefault(i => i.Name.EqualsExact(entity.ItemStatBoost.Item.Name));

                if (item is null)
                {
                    _logger.LogWarning($"Could not find matching Item {entity.ItemStatBoost.Item.Name}, skipping item stat boost pokemon.");
                    entities.Remove(entity);
                    i--;
                    continue;
                }

                entity.ItemStatBoost.ItemId = item.Id;
                entity.ItemStatBoost.Item = item;
            }

            return entities;
        }
    }
}
