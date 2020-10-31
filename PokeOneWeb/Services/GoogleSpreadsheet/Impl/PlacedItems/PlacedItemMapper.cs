using System;
using System.Collections.Generic;
using Microsoft.Extensions.Logging;
using PokeOneWeb.Data.Entities;

namespace PokeOneWeb.Services.GoogleSpreadsheet.Impl.PlacedItems
{
    public class PlacedItemMapper : ISpreadsheetMapper<PlacedItemDto, PlacedItem>
    {
        private readonly ILogger<PlacedItemMapper> _logger;

        private IDictionary<string, Item> _items;
        private IDictionary<string, Location> _locations;

        public PlacedItemMapper(ILogger<PlacedItemMapper> logger)
        {
            _logger = logger;
        }

        public IEnumerable<PlacedItem> Map(IEnumerable<PlacedItemDto> dtos)
        {
            if (dtos is null)
            {
                throw new ArgumentNullException(nameof(dtos));
            }

            _items = new Dictionary<string, Item>();
            _locations = new Dictionary<string, Location>();
            var placedItems = new List<PlacedItem>();

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

        private Location MapLocation(PlacedItemDto dto)
        {
            Location location;
            if (!_locations.ContainsKey(dto.LocationName))
            {
                location = new Location {Name = dto.LocationName};
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

        private PlacedItem MapPlacedItem(PlacedItemDto dto, Location location, Item item)
        {
            return new PlacedItem
            {
                Item = item,
                Location = location,
                PlacementDescription = dto.PlacementDescription,
                Quantity = dto.Quantity,
                Notes = dto.Notes,
                IsHidden = dto.IsHidden,
                IsConfirmed = dto.IsConfirmed
            };
        }
    }
}
