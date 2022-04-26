using PokeOneWeb.Data.Entities;

namespace PokeOneWeb.Data.Repositories.Impl.EntityRepositories
{
    public class RegionRepository : HashedEntityRepository<Region>
    {
        public RegionRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }

        protected override void AddIdsForNames(Region entity)
        {
            entity.EventId = GetOptionalIdForName<Event>(entity.EventName);
        }
    }
}