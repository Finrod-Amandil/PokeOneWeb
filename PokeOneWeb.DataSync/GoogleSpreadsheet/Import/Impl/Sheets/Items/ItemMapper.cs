using System.Collections.Generic;
using PokeOneWeb.Data;
using PokeOneWeb.Data.Entities;

namespace PokeOneWeb.DataSync.GoogleSpreadsheet.Import.Impl.Sheets.Items
{
    public class ItemMapper : XSpreadsheetEntityMapper<ItemSheetDto, Item>
    {
        private readonly Dictionary<string, BagCategory> _bagCategories = new();

        public ItemMapper(ISpreadsheetImportReporter reporter) : base(reporter)
        {
        }

        protected override Entity Entity => Entity.Item;

        protected override bool IsValid(ItemSheetDto dto)
        {
            return
                !string.IsNullOrWhiteSpace(dto.Name) &&
                !string.IsNullOrWhiteSpace(dto.ResourceName) &&
                !string.IsNullOrWhiteSpace(dto.BagCategoryName);
        }

        protected override string GetUniqueName(ItemSheetDto dto)
        {
            return dto.Name;
        }

        protected override Item MapEntity(ItemSheetDto dto, RowHash rowHash, Item item = null)
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
            item.Hash = rowHash.Hash;
            item.ImportSheetId = rowHash.ImportSheetId;
            item.ResourceName = dto.ResourceName;
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