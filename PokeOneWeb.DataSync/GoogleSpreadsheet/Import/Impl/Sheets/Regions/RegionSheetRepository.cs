using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using PokeOneWeb.Data;
using PokeOneWeb.Data.Entities;
using PokeOneWeb.Shared.Extensions;

namespace PokeOneWeb.DataSync.GoogleSpreadsheet.Import.Impl.Sheets.Regions
{
    public class RegionSheetRepository : SheetRepository<RegionSheetDto, Region>
    {
        public RegionSheetRepository(
            ApplicationDbContext dbContext,
            ISheetRowParser<RegionSheetDto> parser,
            ISpreadsheetEntityMapper<RegionSheetDto, Region> mapper,
            ISpreadsheetImportReporter reporter) : base(dbContext, parser, mapper, reporter)
        {
        }

        protected override DbSet<Region> DbSet => DbContext.Regions;

        protected override Entity Entity => Entity.Region;

        protected override List<Region> AttachRelatedEntities(List<Region> entities)
        {
            var events = DbContext.Events.ToList();

            foreach (var entity in entities)
            {
                if (entity.Event == null)
                {
                    continue;
                }

                var eventEntity = events.SingleOrDefault(s => s.Name.EqualsExact(entity.Event.Name));

                if (eventEntity is null)
                {
                    Reporter.ReportError(Entity, entity.IdHash,
                        $"Could not find matching Event {entity.Event.Name}, setting null.");

                    continue;
                }

                entity.EventId = eventEntity.Id;
                entity.Event = eventEntity;
            }

            return entities;
        }
    }
}