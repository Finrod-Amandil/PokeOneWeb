using Microsoft.Extensions.Logging;
using PokeOneWeb.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using PokeOneWeb.Extensions;

namespace PokeOneWeb.Services.GoogleSpreadsheet.Import.Impl.PlacedItems
{
    public class PlacedItemMapper : ISpreadsheetEntityMapper<PlacedItemDto, PlacedItem>
    {
        private readonly ILogger<PlacedItemMapper> _logger;

        private IDictionary<string, Item> _items;
        private IDictionary<string, Location> _locations;

        public PlacedItemMapper(ILogger<PlacedItemMapper> logger)
        {
            _logger = logger;
        }

        public IEnumerable<PlacedItem> Map(IDictionary<RowHash, PlacedItemDto> dtosWithHashes)
        {
            if (dtosWithHashes is null)
            {
                throw new ArgumentNullException(nameof(dtosWithHashes));
            }

            _items = new Dictionary<string, Item>();
            _locations = new Dictionary<string, Location>();

            foreach (var (rowHash, dto) in dtosWithHashes)
            {
                if (!IsValid(dto))
                {
                    _logger.LogWarning($"Found invalid PlacedItem DTO. Skipping.");
                    continue;
                }

                yield return MapPlacedItem(dto, rowHash);
            }
        }

        public IEnumerable<PlacedItem> MapOnto(IList<PlacedItem> entities, IDictionary<RowHash, PlacedItemDto> dtosWithHashes)
        {
            if (dtosWithHashes is null)
            {
                throw new ArgumentNullException(nameof(dtosWithHashes));
            }

            _items = new Dictionary<string, Item>();
            _locations = new Dictionary<string, Location>();

            foreach (var (rowHash, dto) in dtosWithHashes)
            {
                if (!IsValid(dto))
                {
                    _logger.LogWarning($"Found invalid PlacedItem DTO. Skipping.");
                    continue;
                }

                var matchingEntity = entities.SingleOrDefault(x => x.IdHash.EqualsExact(rowHash.IdHash));

                if (matchingEntity is null)
                {
                    _logger.LogWarning($"Found no single matching PlacedItem entity for ID hash {rowHash.IdHash}. Skipping.");
                    continue;
                }

                MapPlacedItem(dto, rowHash, matchingEntity);
            }

            return entities;
        }

        private bool IsValid(PlacedItemDto dto)
        {
            return
                !string.IsNullOrWhiteSpace(dto.ItemName) &&
                !string.IsNullOrWhiteSpace(dto.LocationName) &&
                dto.Index != 0;
        }

        private PlacedItem MapPlacedItem(PlacedItemDto dto, RowHash rowHash, PlacedItem placedItem = null)
        {
            placedItem ??= new PlacedItem();

            placedItem.IdHash = rowHash.IdHash;
            placedItem.Hash = rowHash.ContentHash;
            placedItem.ImportSheetId = rowHash.ImportSheetId;
            placedItem.Location = MapLocation(dto);
            placedItem.Quantity = dto.Quantity;
            placedItem.Item = MapItem(dto);
            placedItem.SortIndex = dto.SortIndex;
            placedItem.Index = dto.Index;
            placedItem.PlacementDescription = dto.PlacementDescription;
            placedItem.IsHidden = dto.IsHidden;
            placedItem.IsConfirmed = dto.IsConfirmed;
            placedItem.Requirements = dto.Requirements;
            placedItem.ScreenshotName = dto.ScreenshotName;
            placedItem.Notes = dto.Notes;

            return placedItem;
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
    }
}
