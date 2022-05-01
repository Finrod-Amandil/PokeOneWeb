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

        public override void Insert(ICollection<ItemStatBoostPokemon> entities)
        {
            // Update/Insert item stat boosts
            var distinctItemStatBoosts = entities
                .Select(x => x.ItemStatBoost)
                .DistinctBy(x => x.ItemName)
                .ToList();

            var existingItemStatBoosts = DbContext.ItemStatBoosts
                .Include(x => x.Item)
                .ToDictionary(x => x.ItemName, x => x.Id);

            foreach (var itemStatBoost in distinctItemStatBoosts)
            {
                itemStatBoost.ItemId = GetRequiredIdForName<Item>(itemStatBoost.ItemName);

                // If entity exists, find Id so that EF updates the corresponding entry.
                // Else set Id to zero, which tells EF to treat it as new entry.
                itemStatBoost.Id = existingItemStatBoosts.TryGetValue(itemStatBoost.ItemName, out var id) ? id : 0;
            }

            DbContext.ItemStatBoosts.UpdateRange(distinctItemStatBoosts);
            DbContext.SaveChanges();

            var itemStatBoosts = DbContext.ItemStatBoosts
                .Include(x => x.Item)
                .ToDictionary(x => x.ItemName, x => x.Id);

            foreach (var entity in entities)
            {
                entity.PokemonVarietyId = GetOptionalIdForName<PokemonVariety>(entity.PokemonVarietyName);
                entity.ItemStatBoostId = itemStatBoosts.TryGetValue(entity.ItemStatBoost.ItemName, out var id)
                    ? id
                    : throw new RelatedEntityNotFoundException(
                        nameof(ItemStatBoostPokemon),
                        nameof(ItemStatBoost),
                        entity.ItemStatBoost.ItemName);

                entity.ItemStatBoost = null;
            }

            base.Insert(entities);
            DbContext.ItemStatBoosts.Where(x => x.RequiredPokemon.Count == 0).DeleteFromQuery();
        }
    }
}