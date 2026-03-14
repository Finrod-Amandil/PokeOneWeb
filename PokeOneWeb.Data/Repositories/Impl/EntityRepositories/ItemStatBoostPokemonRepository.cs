using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using PokeOneWeb.Data.Entities;
using PokeOneWeb.Data.Exceptions;

namespace PokeOneWeb.Data.Repositories.Impl.EntityRepositories
{
    public class ItemStatBoostPokemonRepository : HashedEntityRepository<ItemStatBoostPokemon>
    {
        public ItemStatBoostPokemonRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }

        public override int Insert(ICollection<ItemStatBoostPokemon> entities)
        {
            var insertedCount = base.Insert(entities);
            DeleteUnusedParentEntities();
            return insertedCount;
        }

        public override int Update(ICollection<ItemStatBoostPokemon> entities)
        {
            var updatedCount = base.Update(entities);
            DeleteUnusedParentEntities();
            return updatedCount;
        }

        public override int DeleteByIdHashes(ICollection<string> idHashes)
        {
            var deletedCount = base.DeleteByIdHashes(idHashes);
            DeleteUnusedParentEntities();
            return deletedCount;
        }

        protected override ICollection<ItemStatBoostPokemon> PrepareEntitiesForInsertOrUpdate(
            ICollection<ItemStatBoostPokemon> entities)
        {
            AddOrUpdateItemStatBoosts(entities);

            var itemStatBoosts = DbContext.ItemStatBoosts
                .Include(x => x.Item)
                .ToDictionary(x => x.ItemName, x => x.Id);

            var verifiedEntities = new List<ItemStatBoostPokemon>(entities);
            foreach (var entity in entities)
            {
                var canInsertOrUpdate = true;

                entity.PokemonVarietyId = GetOptionalIdForName<PokemonVariety>(entity.PokemonVarietyName);

                canInsertOrUpdate &= itemStatBoosts.TryGetValue(
                    entity.ItemStatBoost.ItemName, out var id);

                if (!canInsertOrUpdate)
                {
                    var exception = new RelatedEntityNotFoundException(
                        nameof(ItemStatBoostPokemon),
                        nameof(ItemStatBoost),
                        entity.ItemStatBoost.ItemName);

                    ReportInsertOrUpdateException(typeof(ItemStatBoostPokemon), exception);

                    verifiedEntities.Remove(entity);
                }
                else
                {
                    entity.ItemStatBoostId = id;
                    entity.ItemStatBoost = null;
                }
            }

            return base.PrepareEntitiesForInsertOrUpdate(verifiedEntities);
        }

        private void DeleteUnusedParentEntities()
        {
            DbContext.ItemStatBoosts
                .Where(x => x.RequiredPokemon.Count == 0)
                .DeleteFromQuery();
        }

        private void AddOrUpdateItemStatBoosts(ICollection<ItemStatBoostPokemon> entities)
        {
            // Update/Insert item stat boosts
            var distinctItemStatBoosts = entities
                .Select(x => x.ItemStatBoost)
                .DistinctBy(x => x.ItemName)
                .ToList() ?? new List<ItemStatBoost>();

            var existingItemStatBoosts = DbContext.ItemStatBoosts
                .Include(x => x.Item)
                .AsNoTracking()
                .ToDictionary(x => x.ItemName, x => x.Id);

            var verifiedItemStatBoosts = new List<ItemStatBoost>(distinctItemStatBoosts);
            foreach (var itemStatBoost in distinctItemStatBoosts)
            {
                var canInsertOrUpdate = true;

                canInsertOrUpdate &= TrySetIdForName<Item>(
                    itemStatBoost.ItemName,
                    id => itemStatBoost.ItemId = id);

                // If entity exists, find Id so that EF updates the corresponding entry.
                // Else set Id to zero, which tells EF to treat it as new entry.
                itemStatBoost.Id = existingItemStatBoosts.TryGetValue(
                    itemStatBoost.ItemName, out var id) ? id : 0;

                if (!canInsertOrUpdate)
                {
                    verifiedItemStatBoosts.Remove(itemStatBoost);
                }
            }

            DbContext.ItemStatBoosts.UpdateRange(verifiedItemStatBoosts);
            DbContext.SaveChanges();
        }
    }
}