using System;
using System.Collections.Generic;
using PokeOneWeb.Data.Entities;

namespace PokeOneWeb.Data.Repositories.Impl.EntityRepositories
{
    public class BuildRepository : HashedEntityRepository<Build>
    {
        public BuildRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }

        protected override List<Func<Build, bool>> PreparationSteps => new()
        {
            entity => TrySetIdForName<PokemonVariety>(entity.PokemonVarietyName, id => entity.PokemonVarietyId = id),
            entity => TrySetIdForName<Ability>(entity.AbilityName, id => entity.AbilityId = id),
            entity =>
            {
                var canInsertOrUpdate = true;
                entity.NatureOptions.ForEach(natureOption =>
                {
                    canInsertOrUpdate &= TrySetIdForName<Nature>(natureOption.NatureName, id => natureOption.NatureId = id);
                });
                return canInsertOrUpdate;
            },
            entity =>
            {
                var canInsertOrUpdate = true;
                entity.MoveOptions.ForEach(moveOption =>
                {
                    canInsertOrUpdate &= TrySetIdForName<Move>(moveOption.MoveName, id => moveOption.MoveId = id);
                });
                return canInsertOrUpdate;
            },
            entity =>
            {
                var canInsertOrUpdate = true;
                entity.ItemOptions.ForEach(itemOption =>
                {
                    canInsertOrUpdate &= TrySetIdForName<Nature>(itemOption.ItemName, id => itemOption.ItemId = id);
                });
                return canInsertOrUpdate;
            }
        };
    }
}