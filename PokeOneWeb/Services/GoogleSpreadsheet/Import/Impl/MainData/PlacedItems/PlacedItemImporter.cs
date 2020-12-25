using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Logging;
using PokeOneWeb.Data;
using PokeOneWeb.Data.Entities;

namespace PokeOneWeb.Services.GoogleSpreadsheet.Import.Impl.MainData.PlacedItems
{
    public class PlacedItemImporter : SpreadsheetEntityImporter<PlacedItemDto, PlacedItem>
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly ILogger<PlacedItemImporter> _logger;

        public PlacedItemImporter(
            ISpreadsheetEntityReader<PlacedItemDto> reader,
            ISpreadsheetEntityMapper<PlacedItemDto, PlacedItem> mapper,
            ApplicationDbContext dbContext,
            ILogger<PlacedItemImporter> logger) : base(reader, mapper)
        {
            _dbContext = dbContext;
            _logger = logger;
        }

        protected override string GetSheetPrefix()
        {
            return Constants.SHEET_PREFIX_PLACEDITEMS;
        }

        protected override void WriteToDatabase(IEnumerable<PlacedItem> newPlacedItems)
        {
            var locations = _dbContext.Locations.ToList();
            var items = _dbContext.Items.ToList();

            if (!locations.Any() || !items.Any())
            {
                throw new Exception("Tried to import Placed items, but no locations or no items were" +
                                    "present in the database.");
            }

            foreach (var placedItem in newPlacedItems)
            {
                var location = locations.SingleOrDefault(l => 
                    l.Name.Equals(placedItem.Location.Name, StringComparison.Ordinal));

                if (location is null)
                {
                    _logger.LogWarning($"No unique matching location could be found for PlacedItem location {placedItem.Location.Name}. Skipping.");
                    continue;
                }

                var item = items.SingleOrDefault(i => 
                    i.Name.Equals(placedItem.Item.Name, StringComparison.Ordinal));

                if (item is null)
                {
                    _logger.LogWarning($"No unique matching item could be found for PlacedItem item {placedItem.Item.Name}. Skipping.");
                    continue;
                }

                placedItem.Location = location;
                placedItem.Item = item;

                _dbContext.PlacedItems.Add(placedItem);
            }

            _dbContext.SaveChanges();
        }
    }
}
