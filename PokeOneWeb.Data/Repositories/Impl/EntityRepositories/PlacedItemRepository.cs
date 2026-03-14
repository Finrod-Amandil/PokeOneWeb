using System;
using System.Collections.Generic;
using PokeOneWeb.Data.Entities;

namespace PokeOneWeb.Data.Repositories.Impl.EntityRepositories
{
    public class PlacedItemRepository : HashedEntityRepository<PlacedItem>
    {
        public PlacedItemRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }

        protected override List<Func<PlacedItem, bool>> PreparationSteps => new()
        {
            entity => TrySetIdForName<Location>(entity.LocationName, id => entity.LocationId = id),
            entity => TrySetIdForName<Item>(entity.ItemName, id => entity.ItemId = id)
        };
    }
}