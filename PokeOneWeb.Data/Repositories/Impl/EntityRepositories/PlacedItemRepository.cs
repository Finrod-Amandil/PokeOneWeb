using PokeOneWeb.Data.Entities;

namespace PokeOneWeb.Data.Repositories.Impl.EntityRepositories
{
    public class PlacedItemRepository : HashedEntityRepository<PlacedItem>
    {
        public PlacedItemRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }

        protected override void AddIdsForNames(PlacedItem entity)
        {
            entity.LocationId = GetRequiredIdForName<Location>(entity.LocationName);
            entity.ItemId = GetRequiredIdForName<Item>(entity.ItemName);
        }
    }
}