using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Logging;
using PokeOneWeb.Data.Entities;
using PokeOneWeb.Extensions;

namespace PokeOneWeb.Services.GoogleSpreadsheet.Import.Impl.Items
{
    public class ItemMapper : ISpreadsheetEntityMapper<ItemDto, Item>
    {
        private readonly ILogger<ItemMapper> _logger;

        private IDictionary<string, BagCategory> _bagCategories;

        public ItemMapper(ILogger<ItemMapper> logger)
        {
            _logger = logger;
        }

        public IEnumerable<Item> Map(IDictionary<RowHash, ItemDto> dtosWithHashes)
        {
            if (dtosWithHashes is null)
            {
                throw new ArgumentNullException(nameof(dtosWithHashes));
            }

            _bagCategories = new Dictionary<string, BagCategory>();

            foreach (var (rowHash, dto) in dtosWithHashes)
            {
                if (!IsValid(dto))
                {
                    _logger.LogWarning($"Found invalid Item DTO. Skipping.");
                    continue;
                }

                yield return MapItem(dto, rowHash);
            }
        }

        public IEnumerable<Item> MapOnto(IList<Item> entities, IDictionary<RowHash, ItemDto> dtosWithHashes)
        {
            if (dtosWithHashes is null)
            {
                throw new ArgumentNullException(nameof(dtosWithHashes));
            }

            _bagCategories = new Dictionary<string, BagCategory>();

            foreach (var (rowHash, dto) in dtosWithHashes)
            {
                if (!IsValid(dto))
                {
                    _logger.LogWarning($"Found invalid Evolution DTO. Skipping.");
                    continue;
                }

                var matchingEntity = entities.SingleOrDefault(x => x.IdHash.EqualsExact(rowHash.IdHash));

                if (matchingEntity is null)
                {
                    _logger.LogWarning($"Found no single matching Evolution entity for ID hash {rowHash.IdHash}. Skipping.");
                    continue;
                }

                MapItem(dto, rowHash, matchingEntity);
            }

            return entities;
        }

        private bool IsValid(ItemDto dto)
        {
            return
                !string.IsNullOrWhiteSpace(dto.Name) &&
                !string.IsNullOrWhiteSpace(dto.ResourceName) &&
                !string.IsNullOrWhiteSpace(dto.BagCategoryName);
        }

        private Item MapItem(ItemDto dto, RowHash rowHash, Item item = null)
        {
            item ??= new Item();

            BagCategory bagCategory;
            if (_bagCategories.ContainsKey(dto.BagCategoryName))
            {
                bagCategory = _bagCategories[dto.BagCategoryName];
            }
            else
            {
                bagCategory = new BagCategory
                {
                    Name = dto.BagCategoryName
                };
                _bagCategories.Add(dto.BagCategoryName, bagCategory);
            }

            item.BagCategory = bagCategory;

            item.IdHash = rowHash.IdHash;
            item.Hash = rowHash.ContentHash;
            item.ImportSheetId = rowHash.ImportSheetId;
            item.ResourceName = dto.ResourceName;
            item.PokeApiName = dto.PokeApiName;
            item.PokeoneItemId = dto.PokeOneItemId;
            item.SortIndex = dto.SortIndex;
            item.Name = dto.Name;
            item.Description = dto.Description;
            item.Effect = dto.Effect;
            item.IsAvailable = dto.IsAvailable;
            item.DoInclude = dto.DoInclude;
            item.SpriteName = dto.SpriteName;

            return item;
        }
    }
}
