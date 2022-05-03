using PokeOneWeb.Data.Entities;

namespace PokeOneWeb.Data.Repositories.Impl.EntityRepositories
{
    public class ItemRepository : HashedEntityRepository<Item>
    {
        public ItemRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }

        protected override void PrepareEntitiesForInsertOrUpdate(Item entity)
        {
            entity.BagCategoryId = GetRequiredIdForName<BagCategory>(entity.BagCategoryName);
        }
    }
}