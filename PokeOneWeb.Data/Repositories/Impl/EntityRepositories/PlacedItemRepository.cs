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
            entity.LocationId = GetIdForName<Location>(entity.LocationName);
            entity.ItemId = GetIdForName<Item>(entity.ItemName);
        }
    }
}