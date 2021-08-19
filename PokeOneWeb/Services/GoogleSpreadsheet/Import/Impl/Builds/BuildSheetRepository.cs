using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Logging;
using PokeOneWeb.Data;
using PokeOneWeb.Data.Entities;
using PokeOneWeb.Extensions;

namespace PokeOneWeb.Services.GoogleSpreadsheet.Import.Impl.Builds
{
    public class BuildSheetRepository : ISheetRepository
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly ILogger<BuildSheetRepository> _logger;
        private readonly ISheetRowParser<BuildSheetDto> _parser;
        private readonly ISpreadsheetEntityMapper<BuildSheetDto, Build> _mapper;

        public BuildSheetRepository(
            ApplicationDbContext dbContext,
            ILogger<BuildSheetRepository> logger,
            ISheetRowParser<BuildSheetDto> parser,
            ISpreadsheetEntityMapper<BuildSheetDto, Build> mapper)
        {
            _dbContext = dbContext;
            _logger = logger;
            _parser = parser;
            _mapper = mapper;
        }

        public List<RowHash> ReadDbHashes(ImportSheet sheet)
        {
            var rowHashes = _dbContext.Builds
                .Where(x => x.ImportSheetId == sheet.Id)
                .Select(x => new RowHash { ContentHash = x.Hash, IdHash = x.IdHash, ImportSheetId = sheet.Id })
                .ToList();

            return rowHashes;
        }

        public int Delete(List<RowHash> hashes)
        {
            var idHashes = hashes.Select(rh => rh.IdHash).ToList();
            var entities = _dbContext.Builds
                .Where(x => idHashes.Contains(x.IdHash));

            _dbContext.Builds.RemoveRange(entities);
            _dbContext.SaveChanges();

            return entities.Count();
        }

        public int Insert(Dictionary<RowHash, List<object>> rowData)
        {
            var dtosWithHashes = ReadWithHashes(rowData);
            var entities = _mapper.Map(dtosWithHashes).ToList();

            entities = AttachRelatedEntities(entities);

            _dbContext.Builds.AddRange(entities);
            _dbContext.SaveChanges();

            return entities.Count;
        }

        public int Update(Dictionary<RowHash, List<object>> rowData)
        {
            var idHashes = rowData.Keys.Select(rh => rh.IdHash);

            var dtosWithHashes = ReadWithHashes(rowData);
            var entities = _dbContext.Builds
                .Where(x => idHashes.Contains(x.IdHash))
                .ToList();

            var updatedEntities = _mapper.MapOnto(entities, dtosWithHashes).ToList();

            AttachRelatedEntities(updatedEntities);

            _dbContext.SaveChanges();

            return updatedEntities.Count;
        }

        private Dictionary<RowHash, BuildSheetDto> ReadWithHashes(Dictionary<RowHash, List<object>> rowData)
        {
            return rowData
                .Select(row => new { hash = row.Key, values = _parser.ReadRow(row.Value) })
                .ToDictionary(x => x.hash, x => x.values);
        }

        private List<Build> AttachRelatedEntities(List<Build> entities)
        {
            var varieties = _dbContext.PokemonVarieties.ToList();
            var abilities = _dbContext.Abilities.ToList();
            var moves = _dbContext.Moves.ToList();
            var items = _dbContext.Items.ToList();
            var natures = _dbContext.Natures.ToList();

            if (!varieties.Any())
            {
                throw new Exception("Tried to import builds, but no" +
                                    "Pokemon Varieties were present in the database.");
            }

            if (!abilities.Any())
            {
                throw new Exception("Tried to import builds, but no" +
                                    "Abilities were present in the database.");
            }

            if (!moves.Any())
            {
                throw new Exception("Tried to import builds, but no" +
                                    "Moves were present in the database.");
            }

            if (!items.Any())
            {
                throw new Exception("Tried to import builds, but no" +
                                    "Items were present in the database.");
            }

            if (!natures.Any())
            {
                throw new Exception("Tried to import builds, but no" +
                                    "Natures were present in the database.");
            }

            for (var i = 0; i < entities.Count; i++)
            {
                var entity = entities[i];

                var variety = varieties.SingleOrDefault(v => v.Name.EqualsExact(entity.PokemonVariety.Name));
                if (variety is null)
                {
                    _logger.LogWarning($"Could not find matching PokemonVariety {entity.PokemonVariety.Name}, skipping build.");
                    entities.Remove(entity);
                    i--;
                    continue;
                }

                entity.PokemonVarietyId = variety.Id;
                entity.PokemonVariety = variety;

                var ability = abilities.SingleOrDefault(a => a.Name.EqualsExact(entity.Ability.Name));
                if (ability is null)
                {
                    _logger.LogWarning($"Could not find matching Ability {entity.Ability.Name}, skipping build.");
                    entities.Remove(entity);
                    i--;
                    continue;
                }

                entity.AbilityId = ability.Id;
                entity.Ability = ability;

                AttachMoves(entity, moves);
                AttachItems(entity, items);
                AttachNatures(entity, natures);
            }

            return entities;
        }

        private void AttachMoves(Build build, List<Move> moves)
        {
            foreach (var moveOption in new List<MoveOption>(build.MoveOptions))
            {
                var move = moves.SingleOrDefault(m => m.Name.Equals(moveOption.Move.Name));

                if (move is null)
                {
                    _logger.LogWarning($"No unique matching move could be found for move option {moveOption.Move.Name}. Skipping.");
                    build.MoveOptions.Remove(moveOption);
                    continue;
                }

                moveOption.Move = move;
                moveOption.MoveId = move.Id;
            }
        }

        private void AttachItems(Build build, List<Item> items)
        {
            foreach (var itemOption in new List<ItemOption>(build.ItemOptions))
            {
                var item = items.SingleOrDefault(i => i.Name.Equals(itemOption.Item.Name));

                if (item is null)
                {
                    _logger.LogWarning($"No unique matching item could be found for item option {itemOption.Item.Name}. Skipping.");
                    build.ItemOptions.Remove(itemOption);
                    continue;
                }

                itemOption.Item = item;
                itemOption.ItemId = item.Id;
            }
        }

        private void AttachNatures(Build build, List<Nature> natures)
        {
            foreach (var natureOption in new List<NatureOption>(build.NatureOptions))
            {
                var nature = natures.SingleOrDefault(n => n.Name.Equals(natureOption.Nature.Name));

                if (nature is null)
                {
                    _logger.LogWarning($"No unique matching nature could be found for nature option {natureOption.Nature.Name}. Skipping.");
                    build.NatureOptions.Remove(natureOption);
                    continue;
                }

                natureOption.Nature = nature;
                natureOption.NatureId = nature.Id;
            }
        }
    }
}
