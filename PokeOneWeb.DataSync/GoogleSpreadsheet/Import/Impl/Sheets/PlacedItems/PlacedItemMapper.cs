using PokeOneWeb.Data;
using PokeOneWeb.Data.Entities;
using System.Collections.Generic;

namespace PokeOneWeb.DataSync.GoogleSpreadsheet.Import.Impl.Sheets.PlacedItems
{
    public class PlacedItemMapper : SpreadsheetEntityMapper<PlacedItemSheetDto, PlacedItem>
    {
        private readonly Dictionary<string, Item> _items = new();
        private readonly Dictionary<string, Location> _locations = new();

        public PlacedItemMapper(ISpreadsheetImportReporter reporter) : base(reporter) { }

        protected override Entity Entity => Entity.PlacedItem;

        protected override bool IsValid(PlacedItemSheetDto dto)
        {
            return
                !string.IsNullOrWhiteSpace(dto.ItemName) &&
                !string.IsNullOrWhiteSpace(dto.LocationName) &&
                dto.Index != 0;
        }

        protected override string GetUniqueName(PlacedItemSheetDto dto)
        {
            return dto.ItemName + dto.LocationName + dto.Index;
        }

        protected override PlacedItem MapEntity(PlacedItemSheetDto dto, RowHash rowHash, PlacedItem placedItem = null)
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

        private Location MapLocation(PlacedItemSheetDto dto)
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

        private Item MapItem(PlacedItemSheetDto dto)
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
