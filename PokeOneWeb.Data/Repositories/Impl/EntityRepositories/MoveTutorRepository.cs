using PokeOneWeb.Data.Entities;

namespace PokeOneWeb.Data.Repositories.Impl.EntityRepositories
{
    public class MoveTutorRepository : HashedEntityRepository<MoveTutor>
    {
        public MoveTutorRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }

        protected override void AddIdsForNames(MoveTutor entity)
        {
            entity.LocationId = GetRequiredIdForName<Location>(entity.LocationName);
        }
    }
}