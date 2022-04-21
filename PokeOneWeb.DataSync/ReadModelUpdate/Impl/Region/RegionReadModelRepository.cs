using PokeOneWeb.Data;
using PokeOneWeb.Data.ReadModels;
using System.Collections.Generic;
using System.Linq;

namespace PokeOneWeb.DataSync.ReadModelUpdate.Impl.Region
{
    public class RegionReadModelRepository : IReadModelRepository<RegionReadModel>
    {
        private readonly ReadModelDbContext _dbContext;

        public RegionReadModelRepository(ReadModelDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void Update(IDictionary<RegionReadModel, DbAction> entities)
        {
            foreach (var entity in entities.Keys)
            {
                var existingEntity = _dbContext.RegionReadModels
                    .SingleOrDefault(l => l.ApplicationDbId == entity.ApplicationDbId);

                if (existingEntity != null)
                {
                    UpdateExistingEntity(existingEntity, entity);
                }
                else
                {
                    _dbContext.RegionReadModels.Add(entity);
                }
            }

            _dbContext.SaveChanges();
        }

        private static void UpdateExistingEntity(RegionReadModel existingEntity, RegionReadModel entity)
        {
            existingEntity.ApplicationDbId = entity.ApplicationDbId;
            existingEntity.ResourceName = entity.ResourceName;
            existingEntity.Name = entity.Name;
            existingEntity.Color = entity.Color;
            existingEntity.Description = entity.Description;
            existingEntity.IsReleased = entity.IsReleased;
            existingEntity.IsMainRegion = entity.IsMainRegion;
            existingEntity.IsSideRegion = entity.IsSideRegion;
            existingEntity.IsEventRegion = entity.IsEventRegion;
            existingEntity.EventName = entity.EventName;
            existingEntity.EventStartDate = entity.EventStartDate;
            existingEntity.EventEndDate = entity.EventEndDate;
        }
    }
}