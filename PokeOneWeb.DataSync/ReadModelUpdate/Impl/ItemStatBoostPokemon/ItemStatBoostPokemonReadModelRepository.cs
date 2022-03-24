using PokeOneWeb.Data;
using PokeOneWeb.Data.ReadModels;
using System.Collections.Generic;
using System.Linq;

namespace PokeOneWeb.DataSync.ReadModelUpdate.Impl.ItemStatBoostPokemon
{
    public class ItemStatBoostPokemonReadModelRepository : IReadModelRepository<ItemStatBoostPokemonReadModel>
    {
        private readonly ReadModelDbContext _dbContext;

        public ItemStatBoostPokemonReadModelRepository(ReadModelDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void Update(IDictionary<ItemStatBoostPokemonReadModel, DbAction> entities)
        {
            foreach (var entity in entities.Keys)
            {
                var existingEntity = _dbContext.ItemStatBoostPokemonReadModels
                    .SingleOrDefault(l => l.ApplicationDbId == entity.ApplicationDbId);

                if (existingEntity != null)
                {
                    UpdateExistingEntity(existingEntity, entity);
                }
                else
                {
                    _dbContext.ItemStatBoostPokemonReadModels.Add(entity);
                }
            }

            _dbContext.SaveChanges();
        }

        private void UpdateExistingEntity(
            ItemStatBoostPokemonReadModel existingEntity, 
            ItemStatBoostPokemonReadModel entity)
        {
            existingEntity.ApplicationDbId = entity.ApplicationDbId;
            existingEntity.ItemName = entity.ItemName;
            existingEntity.ItemResourceName = entity.ItemResourceName;
            existingEntity.ItemEffect = entity.ItemEffect;
            existingEntity.AttackBoost = entity.AttackBoost;
            existingEntity.DefenseBoost = entity.DefenseBoost;
            existingEntity.SpecialAttackBoost = entity.SpecialAttackBoost;
            existingEntity.SpecialDefenseBoost = entity.SpecialDefenseBoost;
            existingEntity.SpeedBoost = entity.SpeedBoost;
            existingEntity.HitPointsBoost = entity.HitPointsBoost;
            existingEntity.HasRequiredPokemon = entity.HasRequiredPokemon;
            existingEntity.RequiredPokemonName = entity.RequiredPokemonName;
            existingEntity.RequiredPokemonResourceName = entity.RequiredPokemonResourceName;
        }
    }
}
