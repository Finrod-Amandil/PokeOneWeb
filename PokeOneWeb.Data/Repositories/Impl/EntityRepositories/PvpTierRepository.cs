using PokeOneWeb.Data.Entities;

namespace PokeOneWeb.Data.Repositories.Impl.EntityRepositories
{
    public class PvpTierRepository : HashedEntityRepository<PvpTier>
    {
        public PvpTierRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }
    }
}