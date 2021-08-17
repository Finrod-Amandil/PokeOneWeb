﻿using System.Collections.Generic;
using System.Linq;
using PokeOneWeb.Data;
using PokeOneWeb.Data.ReadModels;

namespace PokeOneWeb.Services.ReadModelUpdate.Impl.EntityTypes
{
    public class EntityTypeReadModelRepository : IReadModelRepository<EntityTypeReadModel>
    {
        private readonly ReadModelDbContext _dbContext;

        public EntityTypeReadModelRepository(ReadModelDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void Update(IEnumerable<EntityTypeReadModel> entities)
        {
            foreach (var entity in entities)
            {
                var existingEntity = _dbContext.EntityTypeReadModels
                    .SingleOrDefault(e => 
                        e.ResourceName.Equals(entity.ResourceName) && 
                        e.EntityType == entity.EntityType);

                if (existingEntity != null)
                {
                    existingEntity.ResourceName = entity.ResourceName;
                    existingEntity.EntityType = entity.EntityType;
                }
                else
                {
                    _dbContext.EntityTypeReadModels.Add(entity);
                }
            }

            _dbContext.SaveChanges();
        }
    }
}