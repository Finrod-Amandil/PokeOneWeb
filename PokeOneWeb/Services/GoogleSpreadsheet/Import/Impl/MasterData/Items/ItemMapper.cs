using System;
using System.Collections.Generic;
using Microsoft.Extensions.Logging;
using PokeOneWeb.Data.Entities;

namespace PokeOneWeb.Services.GoogleSpreadsheet.Import.Impl.MasterData.Items
{
    public class ItemMapper : ISpreadsheetEntityMapper<ItemDto, Item>
    {
        private readonly ILogger<ItemMapper> _logger;

        private IDictionary<string, BagCategory> _bagCategories;

        public ItemMapper(ILogger<ItemMapper> logger)
        {
            _logger = logger;
        }

        public IEnumerable<Item> Map(IEnumerable<ItemDto> dtos)
        {
            if (dtos is null)
            {
                throw new ArgumentNullException(nameof(dtos));
            }

            _bagCategories = new Dictionary<string, BagCategory>();

            foreach (var dto in dtos)
            {
                if (!IsValid(dto))
                {
                    _logger.LogWarning($"Found invalid Item DTO with item name {dto.ItemName} and resource name {dto.ResourceName}. Skipping.");
                    continue;
                }

                BagCategory bagCategory;
                if (_bagCategories.ContainsKey(dto.BagCategoryName))
                {
                    bagCategory = _bagCategories[dto.BagCategoryName];
                }
                else
                {
                    bagCategory = new BagCategory
                    {
                        Name = dto.BagCategoryName,
                        SortIndex = dto.BagCategorySortIndex
                    };
                    _bagCategories.Add(dto.BagCategoryName, bagCategory);
                }

                yield return new Item
                {
                    ResourceName = dto.ResourceName,
                    PokeApiName = dto.PokeApiName,
                    PokeoneItemId = dto.PokeOneItemId,
                    SortIndex = dto.SortIndex,
                    Name = dto.ItemName,
                    Description = dto.Description,
                    Effect = dto.Effect,
                    IsAvailable = dto.IsAvailable,
                    DoInclude = dto.DoInclude,
                    SpriteName = dto.SpriteName,
                    BagCategory = bagCategory
                };
            }
        }

        private bool IsValid(ItemDto dto)
        {
            return
                !string.IsNullOrWhiteSpace(dto.ItemName) &&
                !string.IsNullOrWhiteSpace(dto.ResourceName) &&
                !string.IsNullOrWhiteSpace(dto.BagCategoryName);
        }
    }
}
