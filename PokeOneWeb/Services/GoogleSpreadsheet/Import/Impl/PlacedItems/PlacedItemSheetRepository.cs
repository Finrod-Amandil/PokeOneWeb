using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Logging;
using PokeOneWeb.Data;
using PokeOneWeb.Data.Entities;
using PokeOneWeb.Extensions;

namespace PokeOneWeb.Services.GoogleSpreadsheet.Import.Impl.PlacedItems
{
    public class PlacedItemSheetRepository : ISheetRepository
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly ILogger<PlacedItemSheetRepository> _logger;
        private readonly ISheetRowParser<PlacedItemSheetDto> _parser;
        private readonly ISpreadsheetEntityMapper<PlacedItemSheetDto, PlacedItem> _mapper;

        public PlacedItemSheetRepository(
            ApplicationDbContext dbContext,
            ILogger<PlacedItemSheetRepository> logger,
            ISheetRowParser<PlacedItemSheetDto> parser,
            ISpreadsheetEntityMapper<PlacedItemSheetDto, PlacedItem> mapper)
        {
            _dbContext = dbContext;
            _logger = logger;
            _parser = parser;
            _mapper = mapper;
        }

        public List<RowHash> ReadDbHashes(ImportSheet sheet)
        {
            var rowHashes = _dbContext.PlacedItems
                .Where(x => x.ImportSheetId == sheet.Id)
                .Select(x => new RowHash { ContentHash = x.Hash, IdHash = x.IdHash, ImportSheetId = sheet.Id })
                .ToList();

            return rowHashes;
        }

        public int Delete(List<RowHash> hashes)
        {
            var idHashes = hashes.Select(rh => rh.IdHash).ToList();
            var entities = _dbContext.PlacedItems
                .Where(x => idHashes.Contains(x.IdHash));

            _dbContext.PlacedItems.RemoveRange(entities);
            _dbContext.SaveChanges();

            return entities.Count();
        }

        public int Insert(Dictionary<RowHash, List<object>> rowData)
        {
            var dtosWithHashes = ReadWithHashes(rowData);
            var entities = _mapper.Map(dtosWithHashes).ToList();

            entities = AttachRelatedEntities(entities);

            _dbContext.PlacedItems.AddRange(entities);
            _dbContext.SaveChanges();

            return entities.Count;
        }

        public int Update(Dictionary<RowHash, List<object>> rowData)
        {
            var idHashes = rowData.Keys.Select(rh => rh.IdHash);

            var dtosWithHashes = ReadWithHashes(rowData);
            var entities = _dbContext.PlacedItems
                .Where(x => idHashes.Contains(x.IdHash))
                .ToList();

            var updatedEntities = _mapper.MapOnto(entities, dtosWithHashes).ToList();

            AttachRelatedEntities(updatedEntities);

            _dbContext.SaveChanges();

            return updatedEntities.Count;
        }

        private Dictionary<RowHash, PlacedItemSheetDto> ReadWithHashes(Dictionary<RowHash, List<object>> rowData)
        {
            return rowData
                .Select(row => new { hash = row.Key, values = _parser.ReadRow(row.Value) })
                .ToDictionary(x => x.hash, x => x.values);
        }

        private List<PlacedItem> AttachRelatedEntities(List<PlacedItem> entities)
        {
            var items = _dbContext.Items.ToList();
            var locations = _dbContext.Locations.ToList();

            if (!items.Any())
            {
                throw new Exception("Tried to import placed items, but no" +
                                    "Items were present in the database.");
            }

            if (!locations.Any())
            {
                throw new Exception("Tried to import placed items, but no" +
                                    "Locations were present in the database.");
            }

            for (var i = 0; i < entities.Count; i++)
            {
                var entity = entities[i];

                var item = items.SingleOrDefault(i => i.Name.EqualsExact(entity.Item.Name));
                if (item is null)
                {
                    _logger.LogWarning($"Could not find matching Item {entity.Item.Name}, skipping placed item.");
                    entities.Remove(entity);
                    i--;
                    continue;
                }

                entity.ItemId = item.Id;
                entity.Item = item;

                var location = locations.SingleOrDefault(l => l.Name.EqualsExact(entity.Location.Name));
                if (location is null)
                {
                    _logger.LogWarning($"Could not find matching Location {entity.Location.Name}, skipping placed item.");
                    entities.Remove(entity);
                    i--;
                    continue;
                }

                entity.LocationId = location.Id;
                entity.Location = location;
            }

            return entities;
        }
    }
}
