using PokeOneWeb.Data.Entities;

namespace PokeOneWeb.Data.Repositories.Impl.EntityRepositories
{
    public class ItemAvailabilityRepository : HashedEntityRepository<ItemAvailability>
    {
        public ItemAvailabilityRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }
    }
}
