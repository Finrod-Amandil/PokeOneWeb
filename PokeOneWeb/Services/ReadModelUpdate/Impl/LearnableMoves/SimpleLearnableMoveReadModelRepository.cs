using PokeOneWeb.Data;
using PokeOneWeb.Data.ReadModels;
using System.Collections.Generic;
using System.Linq;

namespace PokeOneWeb.Services.ReadModelUpdate.Impl.LearnableMoves
{
    public class SimpleLearnableMoveReadModelRepository : IReadModelRepository<SimpleLearnableMoveReadModel>
    {
        private readonly ReadModelDbContext _dbContext;

        public SimpleLearnableMoveReadModelRepository(ReadModelDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void Update(IDictionary<SimpleLearnableMoveReadModel, DbAction> entities)
        {
            foreach (var entity in entities.Keys)
            {
                var existingEntity = _dbContext.SimpleLearnableMoveReadModels
                    .SingleOrDefault(l => l.ApplicationDbId == entity.ApplicationDbId);

                if (existingEntity != null)
                {
                    UpdateExistingEntity(existingEntity, entity);
                }
                else
                {
                    _dbContext.SimpleLearnableMoveReadModels.Add(entity);
                }
            }

            _dbContext.SaveChanges();
        }

        private void UpdateExistingEntity(SimpleLearnableMoveReadModel existingEntity, SimpleLearnableMoveReadModel entity)
        {
            existingEntity.ApplicationDbId = entity.ApplicationDbId;
            existingEntity.PokemonVarietyApplicationDbId = entity.PokemonVarietyApplicationDbId;
            existingEntity.MoveResourceName = entity.MoveResourceName;
        }
    }
}
