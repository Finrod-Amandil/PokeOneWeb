using System;
using System.Collections.Generic;
using PokeOneWeb.Data.Entities;

namespace PokeOneWeb.Data.Repositories.Impl.EntityRepositories
{
    public class ItemRepository : HashedEntityRepository<Item>
    {
        public ItemRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }

        protected override List<Func<Item, bool>> PreparationSteps => new()
        {
            entity => TrySetIdForName<BagCategory>(entity.BagCategoryName, id => entity.BagCategoryId = id)
        };
    }
}