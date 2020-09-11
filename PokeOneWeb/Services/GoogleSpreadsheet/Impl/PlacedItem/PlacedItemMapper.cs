using System;
using System.Collections.Generic;
using Microsoft.Extensions.Logging;
using PokeOneWeb.Data.Entities;

namespace PokeOneWeb.Services.GoogleSpreadsheet.Impl.PlacedItem
{
    public class PlacedItemMapper : ISpreadsheetMapper<PlacedItemDto, Data.Entities.PlacedItem>
    {
        private readonly ILogger<PlacedItemMapper> _logger;

        private IDictionary<string, Item> _items;
        private IDictionary<string, Data.Entities.Location> _locations;

        public PlacedItemMapper(ILogger<PlacedItemMapper> logger)
        {
            _logger = logger;
        }

        public IEnumerable<Data.Entities.PlacedItem> Map(IEnumerable<PlacedItemDto> dtos)
        {
            if (dtos is null)
            {
                throw new ArgumentNullException(nameof(dtos));
            }

            _items = new Dictionary<string, Item>();
            _locations = new Dictionary<string, Data.Entities.Location>();
            var placedItems = new List<Data.Entities.PlacedItem>();

            foreach (var dto in dtos)
            {
                if (!IsValid(dto))
                {
                    _logger.LogWarning($"Found invalid PlacedItem DTO with item name {dto.ItemName} and location name {dto.LocationName}. Skipping.");
                    continue;
                }

                var location = MapLocation(dto);
                var item = MapItem(dto);

                var placedItem = MapPlacedItem(dto, location, item);
                location.PlacedItems.Add(placedItem);
                item.PlacedItems.Add(placedItem);

                placedItems.Add(placedItem);
            }

            return placedItems;
        }

        private bool IsValid(PlacedItemDto dto)
        {
            return
                !string.IsNullOrWhiteSpace(dto.ItemName) &&
                !string.IsNullOrWhiteSpace(dto.LocationName) &&
                dto.Quantity != 0;
        }

        private Data.Entities.Location MapLocation(PlacedItemDto dto)
        {
            Data.Entities.Location location;
            if (!_locations.ContainsKey(dto.LocationName))
            {
                location = new Data.Entities.Location {Name = dto.LocationName};
                _locations.Add(dto.LocationName, location);
            }
            else
            {
                location = _locations[dto.LocationName];
            }

            return location;
        }

        private Item MapItem(PlacedItemDto dto)
        {
            Item item;
            if (!_items.ContainsKey(dto.ItemName))
            {
                item = new Item { Name = dto.ItemName };
                _items.Add(dto.ItemName, item);
            }
            else
            {
                item = _items[dto.ItemName];
            }

            return item;
        }

        private Data.Entities.PlacedItem MapPlacedItem(PlacedItemDto dto, Data.Entities.Location location, Item item)
        {
            return new Data.Entities.PlacedItem
            {
                Item = item,
                Location = location,
                PlacementDescription = dto.PlacementDescription,
                Notes = dto.Notes,
                IsHidden = dto.IsHidden,
                IsConfirmed = dto.IsConfirmed
            };
        }
    }
}
