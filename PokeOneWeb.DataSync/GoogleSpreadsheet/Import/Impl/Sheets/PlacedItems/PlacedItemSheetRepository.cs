using Microsoft.EntityFrameworkCore;
using PokeOneWeb.Data;
using PokeOneWeb.Data.Entities;
using PokeOneWeb.Shared.Extensions;
using System.Collections.Generic;
using System.Linq;

namespace PokeOneWeb.DataSync.GoogleSpreadsheet.Import.Impl.Sheets.PlacedItems
{
    public class PlacedItemSheetRepository : SheetRepository<PlacedItemSheetDto, PlacedItem>
    {
        public PlacedItemSheetRepository(
            ApplicationDbContext dbContext,
            ISheetRowParser<PlacedItemSheetDto> parser,
            ISpreadsheetEntityMapper<PlacedItemSheetDto, PlacedItem> mapper,
            ISpreadsheetImportReporter reporter) : base(dbContext, parser, mapper, reporter) { }

        protected override DbSet<PlacedItem> DbSet => DbContext.PlacedItems;

        protected override Entity Entity => Entity.PlacedItem;

        protected override List<PlacedItem> AttachRelatedEntities(List<PlacedItem> entities)
        {
            var items = DbContext.Items.ToList();
            var locations = DbContext.Locations.ToList();

            for (var i = 0; i < entities.Count; i++)
            {
                var entity = entities[i];

                var item = items.SingleOrDefault(i => i.Name.EqualsExact(entity.Item.Name));
                if (item is null)
                {
                    Reporter.ReportError(Entity, entity.IdHash,
                        $"Could not find matching Item {entity.Item.Name}, skipping placed item.");

                    entities.Remove(entity);
                    i--;
                    continue;
                }

                entity.ItemId = item.Id;
                entity.Item = item;

                var location = locations.SingleOrDefault(l => l.Name.EqualsExact(entity.Location.Name));
                if (location is null)
                {
                    Reporter.ReportError(Entity, entity.IdHash,
                        $"Could not find matching Location {entity.Location.Name}, skipping placed item.");

                    entities.Remove(entity);
                    i--;
                    continue;
                }

                entity.LocationId = location.Id;
                entity.Location = location;
            }

            return entities;
        }
    }
}
