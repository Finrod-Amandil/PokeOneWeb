using System;
using System.Collections.Generic;
using PokeOneWeb.Data.Entities;

namespace PokeOneWeb.Data.Repositories.Impl.EntityRepositories
{
    public class CurrencyRepository : HashedEntityRepository<Currency>
    {
        public CurrencyRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }

        protected override List<Func<Currency, bool>> PreparationSteps => new()
        {
            entity => TrySetIdForName<Item>(entity.ItemName, id => entity.ItemId = id)
        };
    }
}