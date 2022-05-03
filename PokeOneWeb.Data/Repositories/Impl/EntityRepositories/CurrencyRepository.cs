using PokeOneWeb.Data.Entities;

namespace PokeOneWeb.Data.Repositories.Impl.EntityRepositories
{
    public class CurrencyRepository : HashedEntityRepository<Currency>
    {
        public CurrencyRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }

        protected override void PrepareEntitiesForInsertOrUpdate(Currency entity)
        {
            entity.ItemId = GetRequiredIdForName<Item>(entity.ItemName);
        }
    }
}