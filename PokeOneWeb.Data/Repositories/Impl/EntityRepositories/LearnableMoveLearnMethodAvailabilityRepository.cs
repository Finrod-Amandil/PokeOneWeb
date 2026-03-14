using PokeOneWeb.Data.Entities;

namespace PokeOneWeb.Data.Repositories.Impl.EntityRepositories
{
    public class LearnableMoveLearnMethodAvailabilityRepository : HashedEntityRepository<LearnableMoveLearnMethodAvailability>
    {
        public LearnableMoveLearnMethodAvailabilityRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }
    }
}
