using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Logging;
using PokeOneWeb.Data;
using PokeOneWeb.Data.Entities;
using PokeOneWeb.Extensions;
using Z.EntityFramework.Plus;

namespace PokeOneWeb.Services.GoogleSpreadsheet.Import.Impl.Locations
{
    public class LocationSheetRepository : ISheetRepository
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly ILogger<LocationSheetRepository> _logger;
        private readonly ISheetRowParser<LocationDto> _parser;
        private readonly ISpreadsheetEntityMapper<LocationDto, Location> _mapper;

        public LocationSheetRepository(
            ApplicationDbContext dbContext,
            ILogger<LocationSheetRepository> logger,
            ISheetRowParser<LocationDto> parser,
            ISpreadsheetEntityMapper<LocationDto, Location> mapper)
        {
            _dbContext = dbContext;
            _logger = logger;
            _parser = parser;
            _mapper = mapper;
        }

        public List<RowHash> ReadDbHashes(ImportSheet sheet)
        {
            var rowHashes = _dbContext.Locations
                .Where(x => x.ImportSheetId == sheet.Id)
                .Select(x => new RowHash { ContentHash = x.Hash, IdHash = x.IdHash, ImportSheetId = sheet.Id })
                .ToList();

            return rowHashes;
        }

        public int Delete(List<RowHash> hashes)
        {
            var idHashes = hashes.Select(rh => rh.IdHash).ToList();
            var entities = _dbContext.Locations
                .Where(x => idHashes.Contains(x.IdHash));

            _dbContext.Locations.RemoveRange(entities);
            _dbContext.SaveChanges();

            DeleteOrphans();

            return entities.Count();
        }

        public int Insert(Dictionary<RowHash, List<object>> rowData)
        {
            var dtosWithHashes = ReadWithHashes(rowData);
            var entities = _mapper.Map(dtosWithHashes).ToList();

            entities = AttachRelatedEntities(entities);

            _dbContext.Locations.AddRange(entities);
            _dbContext.SaveChanges();

            return entities.Count;
        }

        public int Update(Dictionary<RowHash, List<object>> rowData)
        {
            var idHashes = rowData.Keys.Select(rh => rh.IdHash);

            var dtosWithHashes = ReadWithHashes(rowData);
            var entities = _dbContext.Locations
                .Where(x => idHashes.Contains(x.IdHash))
                .ToList();

            var updatedEntities = _mapper.MapOnto(entities, dtosWithHashes).ToList();

            AttachRelatedEntities(updatedEntities);

            _dbContext.SaveChanges();

            return updatedEntities.Count;
        }

        private Dictionary<RowHash, LocationDto> ReadWithHashes(Dictionary<RowHash, List<object>> rowData)
        {
            return rowData
                .Select(row => new { hash = row.Key, values = _parser.ReadRow(row.Value) })
                .ToDictionary(x => x.hash, x => x.values);
        }

        private List<Location> AttachRelatedEntities(List<Location> entities)
        {
            var regions = _dbContext.Regions.ToList();
            var locationGroups = _dbContext.LocationGroups
                .IncludeOptimized(lg => lg.Region)
                .ToList();

            if (!regions.Any())
            {
                throw new Exception("Tried to import locations, but no" +
                                    "Regions were present in the database.");
            }

            for (var i = 0; i < entities.Count; i++)
            {
                var entity = entities[i];

                var region = regions.SingleOrDefault(r => r.Name.EqualsExact(entity.LocationGroup.Region.Name));
                if (region is null)
                {
                    _logger.LogWarning($"Could not find matching Region {entity.LocationGroup.Region.Name}, skipping location.");
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

        private void DeleteOrphans()
        {
            _dbContext.LocationGroups
                .IncludeOptimized(l => l.Locations)
                .Where(l => l.Locations.Count == 0)
                .DeleteFromQuery();
        }
    }
}
