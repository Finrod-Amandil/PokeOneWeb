using Microsoft.EntityFrameworkCore;
using PokeOneWeb.Data;
using PokeOneWeb.Data.Entities;
using PokeOneWeb.Shared.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PokeOneWeb.DataSync.GoogleSpreadsheet.Import.Impl.Sheets.Items
{
    public class ItemSheetRepository : SheetRepository<ItemSheetDto, Item>
    {
        public ItemSheetRepository(
            ApplicationDbContext dbContext,
            ISheetRowParser<ItemSheetDto> parser,
            ISpreadsheetEntityMapper<ItemSheetDto, Item> mapper,
            ISpreadsheetImportReporter reporter) : base(dbContext, parser, mapper, reporter) { }

        protected override DbSet<Item> DbSet => DbContext.Items;

        protected override Entity Entity => Entity.Item;

        protected override List<Item> AttachRelatedEntities(List<Item> entities)
        {
            var bagCategories = DbContext.BagCategories.ToList();

            if (!bagCategories.Any())
            {
                throw new Exception("Tried to import items, but no" +
                                    "Bag Categories were present in the database.");
            }

            for (var i = 0; i < entities.Count; i++)
            {
                var entity = entities[i];
                var bagCategory = bagCategories.SingleOrDefault(b => b.Name.EqualsExact(entity.BagCategory.Name));

                if (bagCategory is null)
                {
                    Reporter.ReportError(Entity, entity.IdHash,
                        $"Could not find matching BagCategory {entity.BagCategory.Name}, skipping item.");

                    entities.Remove(entity);
                    i--;
                    continue;
                }

                entity.BagCategoryId = bagCategory.Id;
                entity.BagCategory = bagCategory;
            }

            return entities;
        }
    }
}
