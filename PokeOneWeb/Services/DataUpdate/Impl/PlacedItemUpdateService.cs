using Microsoft.Extensions.Logging;
using PokeOneWeb.Data;
using PokeOneWeb.Data.Entities;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace PokeOneWeb.Services.DataUpdate.Impl
{
    public class PlacedItemUpdateService : IDataUpdateService<PlacedItem>
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly ILogger<PlacedItemUpdateService> _logger;

        public PlacedItemUpdateService(ApplicationDbContext dbContext, ILogger<PlacedItemUpdateService> logger)
        {
            _dbContext = dbContext;
            _logger = logger;
        }

        public void Update(IEnumerable<PlacedItem> newPlacedItems)
        {
            //Delete all existing placed items
            _dbContext.PlacedItems.RemoveRange(_dbContext.PlacedItems);
            _dbContext.Database.ExecuteSqlRaw("DBCC CHECKIDENT ('PokeOneWeb.dbo.PlacedItem',RESEED, 0)");

            _dbContext.SaveChanges();

            var locations = _dbContext.Locations.ToList();
            var items = _dbContext.Items.ToList();

            foreach (var placedItem in newPlacedItems)
            {
                var location = locations.SingleOrDefault(l => l.Name.Equals(placedItem.Location.Name));

                if (location is null)
                {
                    _logger.LogWarning($"No unique matching location could be found for PlacedItem location {placedItem.Location.Name}. Skipping.");
                    continue;
                }

                var item = items.SingleOrDefault(i => i.Name.Equals(placedItem.Item.Name));

                if (item is null)
                {
                    _logger.LogWarning($"No unique matching item could be found for PlacedItem item {placedItem.Item.Name}. Skipping.");
                    continue;
                }

                placedItem.Location = location;
                placedItem.LocationId = location.Id;
                placedItem.Item = item;
                placedItem.ItemId = item.Id;
                item.IsAvailable = true;

                _dbContext.PlacedItems.Add(placedItem);
            }

            _dbContext.SaveChanges();
        }
    }
}
