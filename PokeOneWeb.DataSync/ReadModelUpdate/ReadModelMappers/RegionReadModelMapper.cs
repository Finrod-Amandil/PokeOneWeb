using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using PokeOneWeb.Data;
using PokeOneWeb.Data.ReadModels;
using PokeOneWeb.DataSync.ReadModelUpdate.Interfaces;

namespace PokeOneWeb.DataSync.ReadModelUpdate.ReadModelMappers
{
    public class RegionReadModelMapper : IReadModelMapper<RegionReadModel>
    {
        private readonly ApplicationDbContext _dbContext;

        public RegionReadModelMapper(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public List<RegionReadModel> MapFromDatabase()
        {
            return _dbContext.Regions
                .Include(m => m.Event)
                .AsNoTracking()
                .Select(region => new RegionReadModel()
                {
                    Name = region.Name,
                    ResourceName = region.ResourceName,
                    Color = region.Color,
                    Description = region.Description,
                    IsReleased = region.IsReleased,
                    IsMainRegion = region.IsMainRegion,
                    IsSideRegion = region.IsSideRegion,
                    IsEventRegion = region.IsEventRegion,
                    EventName = region.Event.Name,
                    EventStartDate = region.Event.StartDate,
                    EventEndDate = region.Event.EndDate,
                })
                .ToList();
        }
    }
}