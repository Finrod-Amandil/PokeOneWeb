using System.Collections.Generic;
using System.Linq;
using PokeOneWeb.Data;
using PokeOneWeb.Data.ReadModels;

namespace PokeOneWeb.Services.ReadModelUpdate.Impl.Moves
{
    public class MoveReadModelRepository : IReadModelRepository<MoveReadModel>
    {
        private readonly ReadModelDbContext _dbContext;

        public MoveReadModelRepository(ReadModelDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void Update(IDictionary<MoveReadModel, DbAction> entities)
        {
            foreach (var entity in entities.Keys)
            {
                var existingEntity = _dbContext.MoveReadModels
                    .SingleOrDefault(l => l.ApplicationDbId == entity.ApplicationDbId);

                if (existingEntity != null)
                {
                    UpdateExistingEntity(existingEntity, entity);
                }
                else
                {
                    _dbContext.MoveReadModels.Add(entity);
                }
            }

            _dbContext.SaveChanges();
        }

        private void UpdateExistingEntity(MoveReadModel existingEntity, MoveReadModel entity)
        {
            existingEntity.ApplicationDbId = entity.ApplicationDbId;
            existingEntity.Name = entity.Name;
            existingEntity.ResourceName = entity.ResourceName;
            existingEntity.ElementalType = entity.ElementalType;
            existingEntity.DamageClass = entity.DamageClass;
            existingEntity.AttackPower = entity.AttackPower;
            existingEntity.Accuracy = entity.Accuracy;
            existingEntity.PowerPoints = entity.PowerPoints;
            existingEntity.Priority = entity.Priority;
            existingEntity.EffectDescription = entity.EffectDescription;
        }
    }
}
