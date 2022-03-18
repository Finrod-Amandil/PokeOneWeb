using Microsoft.EntityFrameworkCore;
using PokeOneWeb.Data;
using PokeOneWeb.Data.ReadModels;
using PokeOneWeb.DataSync.GoogleSpreadsheet.Import.Impl.Reporting;

namespace PokeOneWeb.DataSync.ReadModelUpdate.Impl.Region
{
    public class RegionReadModelMapper : IReadModelMapper<RegionReadModel>
    {
        private readonly ApplicationDbContext _dbContext;

        public RegionReadModelMapper(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IDictionary<RegionReadModel, DbAction> MapFromDatabase(SpreadsheetImportReport importReport)
        {
            return _dbContext.Regions
                .Include(m => m.Event)
                .AsNoTracking()
                .Select(region => new RegionReadModel()
                {
                    ApplicationDbId = region.Id,
                    Name = region.Name,
                    // TODO: As soon as the Resource Name is available in the import db the line afterwards can be uncommented.
                    //ResourceName = region.ResourceName,
                    Color = region.Color,
                    IsEventRegion = region.IsEventRegion,
                    EventName = region.Event.Name,
                    EventStartDate = region.Event.StartDate,
                    EventEndDate = region.Event.EndDate,
                })
                .ToDictionary(x => x, _ => DbAction.Create);
        }
    }
}
