﻿using Microsoft.EntityFrameworkCore;
using PokeOneWeb.Data;
using PokeOneWeb.Data.Entities;
using PokeOneWeb.Extensions;
using System.Collections.Generic;
using System.Linq;
using Z.EntityFramework.Plus;

namespace PokeOneWeb.Services.GoogleSpreadsheet.Import.Impl.Sheets.Locations
{
    public class LocationSheetRepository : SheetRepository<LocationSheetDto, Location>
    {
        public LocationSheetRepository(
            ApplicationDbContext dbContext,
            ISheetRowParser<LocationSheetDto> parser,
            ISpreadsheetEntityMapper<LocationSheetDto, Location> mapper,
            ISpreadsheetImportReporter reporter) : base(dbContext, parser, mapper, reporter) { }

        protected override DbSet<Location> DbSet => DbContext.Locations;

        protected override Entity Entity => Entity.Location;

        protected override List<Location> AttachRelatedEntities(List<Location> entities)
        {
            var regions = DbContext.Regions.ToList();
            var locationGroups = DbContext.LocationGroups
                .IncludeOptimized(lg => lg.Region)
                .ToList();

            for (var i = 0; i < entities.Count; i++)
            {
                var entity = entities[i];

                var region = regions.SingleOrDefault(r => r.Name.EqualsExact(entity.LocationGroup.Region.Name));
                if (region is null)
                {
                    Reporter.ReportError(Entity, entity.IdHash,
                        $"Could not find matching Region {entity.LocationGroup.Region.Name}, skipping location.");

                    entities.Remove(entity);
                    i--;
                    continue;
                }

                entity.LocationGroup.RegionId = region.Id;
                entity.LocationGroup.Region = region;

                var locationGroup = locationGroups.SingleOrDefault(lg =>
                    lg.ResourceName.EqualsExact(entity.LocationGroup.ResourceName));

                if (locationGroup != null)
                {
                    locationGroup.Name = entity.LocationGroup.Name;
                    locationGroup.ResourceName = entity.LocationGroup.ResourceName;
                    locationGroup.Region = entity.LocationGroup.Region;

                    entity.LocationGroupId = locationGroup.Id;
                    entity.LocationGroup = locationGroup;
                }
            }

            return entities;
        }

        protected override void DeleteOrphans()
        {
            DbContext.LocationGroups
                .IncludeOptimized(l => l.Locations)
                .Where(l => l.Locations.Count == 0)
                .DeleteFromQuery();

            DbContext.SaveChanges();
        }
    }
}
