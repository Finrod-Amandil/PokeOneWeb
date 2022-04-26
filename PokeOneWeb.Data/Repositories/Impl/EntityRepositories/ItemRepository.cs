using PokeOneWeb.Data.Entities;

namespace PokeOneWeb.Data.Repositories.Impl.EntityRepositories
{
    public class ItemRepository : HashedEntityRepository<Item>
    {
        public ItemRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }

        protected override void AddIdsForNames(Item entity)
        {
            entity.BagCategoryId = GetRequiredIdForName<BagCategory>(entity.BagCategoryName);
        }
    }
}