using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Logging;
using PokeOneWeb.Data;
using PokeOneWeb.Data.Entities;

namespace PokeOneWeb.Services.GoogleSpreadsheet.Import.Impl.MainData.Locations
{
    public class LocationImporter : SpreadsheetEntityImporter<LocationDto, Location>
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly ILogger<LocationImporter> _logger;

        public LocationImporter(
            ISpreadsheetEntityReader<LocationDto> reader,
            ISpreadsheetEntityMapper<LocationDto, Location> mapper,
            ApplicationDbContext dbContext,
            ILogger<LocationImporter> logger) : base(reader, mapper)
        {
            _dbContext = dbContext;
            _logger = logger;
        }

        protected override string GetSheetPrefix()
        {
            return Constants.SHEET_PREFIX_LOCATIONS;
        }

        protected override void WriteToDatabase(IEnumerable<Location> newLocations)
        {
            var regions = _dbContext.Regions.ToList();

            if (!regions.Any())
            {
                throw new Exception("Locations could not be imported as no regions were present in the database.");
            }

            foreach (var newLocation in newLocations)
            {
                var region = regions.SingleOrDefault(r =>
                    r.Name.Equals(newLocation.LocationGroup.Region.Name, StringComparison.Ordinal));

                if (region is null)
                {
                    _logger.LogWarning($"No unique matching region could be found for " +
                                       $"LocationGroup {newLocation.LocationGroup.Name}. Skipping.");
                    continue;
                }

                newLocation.LocationGroup.Region = region;
                newLocation.LocationGroup.RegionId = region.Id;

                _dbContext.Locations.Add(newLocation);
            }

            _dbContext.SaveChanges();
        }
    }
}
