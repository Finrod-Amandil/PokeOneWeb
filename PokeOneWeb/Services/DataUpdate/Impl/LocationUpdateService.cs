using Microsoft.EntityFrameworkCore;
using PokeOneWeb.Data;
using PokeOneWeb.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PokeOneWeb.Services.DataUpdate.Impl
{
    public class LocationUpdateService : IDataUpdateService<Location>
    {
        private readonly ApplicationDbContext _dbContext;

        public LocationUpdateService(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void Update(IEnumerable<Location> newLocations, bool deleteExisting = false)
        {
            newLocations = newLocations.ToList();

            UpdateRegions(newLocations);
            UpdateLocationGroups(newLocations);
            UpdateLocations(newLocations);
        }

        private void UpdateRegions(IEnumerable<Location> newLocations)
        {
            var dbRegions = _dbContext.Regions.ToList();
            var newRegions = newLocations.Select(l => l.LocationGroup.Region).Distinct().ToList();

            foreach (var newRegion in newRegions)
            {
                var matchingDbRegion = dbRegions.SingleOrDefault(dbRegion =>
                    dbRegion.Name.Equals(newRegion.Name, StringComparison.Ordinal));

                if (matchingDbRegion is null)
                {
                    _dbContext.Regions.Add(newRegion);
                }
                else
                {
                    matchingDbRegion.IsEventRegion = newRegion.IsEventRegion;
                }
            }

            var regionsToRemove = dbRegions
                .Where(dbRegion => !newRegions.Select(r => r.Name).Contains(dbRegion.Name, StringComparer.Ordinal));

            _dbContext.Regions.RemoveRange(regionsToRemove);

            _dbContext.SaveChanges();
        }

        private void UpdateLocationGroups(IEnumerable<Location> newLocations)
        {
            var dbLocationGroups = _dbContext.LocationGroups.Include(lg => lg.Region).ToList();
            var dbRegions = _dbContext.Regions.ToList();
            var newLocationGroups = newLocations.Select(l => l.LocationGroup).Distinct().ToList();

            foreach (var newLocationGroup in newLocationGroups)
            {
                newLocationGroup.Region = dbRegions.Single(r => r.Name.Equals(newLocationGroup.Region.Name));

                var matchingDbLocationGroup = dbLocationGroups.SingleOrDefault(dbLocationGroup =>
                    dbLocationGroup.Name.Equals(newLocationGroup.Name, StringComparison.Ordinal));

                if (matchingDbLocationGroup is null)
                {
                    _dbContext.LocationGroups.Add(newLocationGroup);
                }
                else
                {
                    matchingDbLocationGroup.Region = newLocationGroup.Region;
                    matchingDbLocationGroup.RegionId = newLocationGroup.Region.Id;
                }
            }

            var locationGroupsToRemove = dbLocationGroups.Where(dbLocationGroup =>
                !newLocationGroups.Select(lg => lg.Name).Contains(dbLocationGroup.Name, StringComparer.Ordinal));

            _dbContext.LocationGroups.RemoveRange(locationGroupsToRemove);

            _dbContext.SaveChanges();
        }

        private void UpdateLocations(IEnumerable<Location> newLocations)
        {
            var dbLocations = _dbContext.Locations.Include(l => l.LocationGroup).ToList();
            var dbLocationGroups = _dbContext.LocationGroups.ToList();

            foreach (var newLocation in newLocations)
            {
                newLocation.LocationGroup = dbLocationGroups.Single(lg => lg.Name.Equals(newLocation.LocationGroup.Name));

                var matchingDbLocation = dbLocations.SingleOrDefault(dbLocation =>
                    dbLocation.Name.Equals(newLocation.Name, StringComparison.Ordinal));

                if (matchingDbLocation is null)
                {
                    _dbContext.Locations.Add(newLocation);
                }
                else
                {
                    matchingDbLocation.SortIndex = newLocation.SortIndex;
                    matchingDbLocation.LocationGroup = newLocation.LocationGroup;
                    matchingDbLocation.LocationGroupId = newLocation.LocationGroup.Id;
                    matchingDbLocation.IsDiscoverable = newLocation.IsDiscoverable;
                    matchingDbLocation.Notes = newLocation.Notes;
                }
            }

            var locationsToRemove = dbLocations.Where(dbLocation =>
                !newLocations.Select(l => l.Name).Contains(dbLocation.Name, StringComparer.Ordinal));

            _dbContext.Locations.RemoveRange(locationsToRemove);

            _dbContext.SaveChanges();
        }
    }
}
