using PokeOneWeb.Data.Entities;

namespace PokeOneWeb.Data.Repositories.Impl.EntityRepositories
{
    public class AbilityRepository : HashedEntityRepository<Ability>
    {
        public AbilityRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }
    }
}