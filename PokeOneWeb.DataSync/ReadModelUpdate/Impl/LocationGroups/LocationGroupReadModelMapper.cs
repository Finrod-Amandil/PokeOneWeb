using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using PokeOneWeb.Data;
using PokeOneWeb.Data.ReadModels;
using PokeOneWeb.DataSync.GoogleSpreadsheet.Import.Impl.Reporting;

namespace PokeOneWeb.DataSync.ReadModelUpdate.Impl.LocationGroups
{
    public class LocationGroupReadModelMapper : IReadModelMapper<LocationGroupReadModel>
    {
        private readonly ApplicationDbContext _dbContext;

        public LocationGroupReadModelMapper(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IDictionary<LocationGroupReadModel, DbAction> MapFromDatabase(SpreadsheetImportReport importReport)
        {
            return _dbContext.LocationGroups
                .Include(lg => lg.Locations)
                .Include(lg => lg.Region)
                .Select(lg => new LocationGroupReadModel
                {
                    ApplicationDbId = lg.Id,
                    ResourceName = lg.ResourceName,
                    Name = lg.Name,
                    RegionName = lg.Region.Name,
                    RegionResourceName = lg.Region.ResourceName,
                    SortIndex = lg.Locations.Min(l => l.SortIndex)
                })
                .AsNoTracking()
                .AsSingleQuery()
                .ToDictionary(x => x, _ => DbAction.Create);
        }
    }
}
