using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Logging;
using PokeOneWeb.Data;
using PokeOneWeb.Data.Entities;
using PokeOneWeb.Extensions;

namespace PokeOneWeb.Services.GoogleSpreadsheet.Import.Impl.Items
{
    public class ItemSheetRepository : ISheetRepository
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly ILogger<ItemSheetRepository> _logger;
        private readonly ISheetRowParser<ItemDto> _parser;
        private readonly ISpreadsheetEntityMapper<ItemDto, Item> _mapper;

        public ItemSheetRepository(
            ApplicationDbContext dbContext,
            ILogger<ItemSheetRepository> logger,
            ISheetRowParser<ItemDto> parser,
            ISpreadsheetEntityMapper<ItemDto, Item> mapper)
        {
            _dbContext = dbContext;
            _logger = logger;
            _parser = parser;
            _mapper = mapper;
        }

        public List<RowHash> ReadDbHashes(ImportSheet sheet)
        {
            var rowHashes = _dbContext.Items
                .Where(x => x.ImportSheetId == sheet.Id)
                .Select(x => new RowHash { ContentHash = x.Hash, IdHash = x.IdHash, ImportSheetId = sheet.Id })
                .ToList();

            return rowHashes;
        }

        public int Delete(List<RowHash> hashes)
        {
            var idHashes = hashes.Select(rh => rh.IdHash).ToList();
            var entities = _dbContext.Items
                .Where(x => idHashes.Contains(x.IdHash));

            _dbContext.Items.RemoveRange(entities);
            _dbContext.SaveChanges();

            return entities.Count();
        }

        public int Insert(Dictionary<RowHash, List<object>> rowData)
        {
            var dtosWithHashes = ReadWithHashes(rowData);
            var entities = _mapper.Map(dtosWithHashes).ToList();

            entities = AttachRelatedEntities(entities);

            _dbContext.Items.AddRange(entities);
            _dbContext.SaveChanges();

            return entities.Count;
        }

        public int Update(Dictionary<RowHash, List<object>> rowData)
        {
            var idHashes = rowData.Keys.Select(rh => rh.IdHash);

            var dtosWithHashes = ReadWithHashes(rowData);
            var entities = _dbContext.Items
                .Where(x => idHashes.Contains(x.IdHash))
                .ToList();

            var updatedEntities = _mapper.MapOnto(entities, dtosWithHashes).ToList();

            AttachRelatedEntities(updatedEntities);

            _dbContext.SaveChanges();

            return updatedEntities.Count;
        }

        private Dictionary<RowHash, ItemDto> ReadWithHashes(Dictionary<RowHash, List<object>> rowData)
        {
            return rowData
                .Select(row => new { hash = row.Key, values = _parser.ReadRow(row.Value) })
                .ToDictionary(x => x.hash, x => x.values);
        }

        private List<Item> AttachRelatedEntities(List<Item> entities)
        {
            var bagCategories = _dbContext.BagCategories.ToList();

            if (!bagCategories.Any())
            {
                throw new Exception("Tried to import items, but no" +
                                    "Bag Categories were present in the database.");
            }

            for (var i = 0; i < entities.Count; i++)
            {
                var entity = entities[i];
                var bagCategory = bagCategories.SingleOrDefault(b => b.Name.EqualsExact(entity.BagCategory.Name));

                if (bagCategory is null)
                {
                    _logger.LogWarning($"Could not find matching BagCategory {entity.BagCategory.Name}, skipping item.");
                    entities.Remove(entity);
                    i--;
                    continue;
                }

                entity.BagCategoryId = bagCategory.Id;
                entity.BagCategory = bagCategory;
            }

            return entities;
        }
    }
}
