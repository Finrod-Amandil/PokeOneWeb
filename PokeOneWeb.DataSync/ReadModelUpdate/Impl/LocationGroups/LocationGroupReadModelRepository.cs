using System.Collections.Generic;
using System.Linq;
using PokeOneWeb.Data;
using PokeOneWeb.Data.ReadModels;

namespace PokeOneWeb.DataSync.ReadModelUpdate.Impl.LocationGroups
{
    public class LocationGroupReadModelRepository : IReadModelRepository<LocationGroupReadModel>
    {
        private readonly ReadModelDbContext _dbContext;

        public LocationGroupReadModelRepository(ReadModelDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void Update(IDictionary<LocationGroupReadModel, DbAction> entities)
        {
            var allEntities = _dbContext.LocationGroupReadModels
                .ToList();

            foreach (var entity in entities.Keys)
            {
                var existingEntity = allEntities
                    .SingleOrDefault(e => e.ApplicationDbId == entity.ApplicationDbId);

                if (existingEntity != null)
                {
                    UpdateExistingEntity(existingEntity, entity);
                }
                else
                {
                    _dbContext.LocationGroupReadModels.Add(entity);
                }
            }

            _dbContext.SaveChanges();
        }

        private void UpdateExistingEntity(LocationGroupReadModel existingEntity, LocationGroupReadModel entity)
        {
            existingEntity.ApplicationDbId = entity.ApplicationDbId;
            existingEntity.ResourceName = entity.ResourceName;
            existingEntity.Name = entity.Name;
            existingEntity.SortIndex = entity.SortIndex;
            existingEntity.RegionName = entity.RegionName;
            existingEntity.RegionResourceName = entity.RegionResourceName;
        }
    }
}
