using PokeOneWeb.Data;
using PokeOneWeb.Data.ReadModels;

namespace PokeOneWeb.DataSync.ReadModelUpdate.Impl.Natures
{
    public class NatureReadModelRepository : IReadModelRepository<NatureReadModel>
    {
        private readonly ReadModelDbContext _dbContext;

        public NatureReadModelRepository(ReadModelDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void Update(IDictionary<NatureReadModel, DbAction> entities)
        {
            foreach (var entity in entities.Keys)
            {
                var existingEntity = _dbContext.NatureReadModels
                    .SingleOrDefault(l => l.ApplicationDbId == entity.ApplicationDbId);

                if (existingEntity != null)
                {
                    UpdateExistingEntity(existingEntity, entity);
                }
                else
                {
                    _dbContext.NatureReadModels.Add(entity);
                }
            }

            _dbContext.SaveChanges();
        }

        private void UpdateExistingEntity(NatureReadModel existingEntity, NatureReadModel entity)
        {
            existingEntity.ApplicationDbId = entity.ApplicationDbId;
            existingEntity.Name = entity.Name;
            existingEntity.Effect = entity.Effect;
            existingEntity.Attack = entity.Attack;
            existingEntity.SpecialAttack = entity.SpecialAttack;
            existingEntity.Defense = entity.Defense;
            existingEntity.SpecialDefense = entity.SpecialDefense;
            existingEntity.Speed = entity.Speed;
        }
    }
}
