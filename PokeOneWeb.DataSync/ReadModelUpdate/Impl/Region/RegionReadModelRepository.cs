﻿using PokeOneWeb.Data;
using PokeOneWeb.Data.ReadModels;

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

        private void UpdateExistingEntity(RegionReadModel existingEntity, RegionReadModel entity)
        {
            existingEntity.ApplicationDbId = entity.ApplicationDbId;
            existingEntity.ResourceName = entity.ResourceName;
            existingEntity.Name = entity.Name;
            existingEntity.Color = entity.Color;
            existingEntity.IsEventRegion = entity.IsEventRegion;
            existingEntity.EventName = entity.EventName;
            existingEntity.EventStartDate = entity.EventStartDate;
            existingEntity.EventEndDate = entity.EventEndDate;
        }
    }
}