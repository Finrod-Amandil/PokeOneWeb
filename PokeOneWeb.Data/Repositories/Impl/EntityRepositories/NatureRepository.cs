using PokeOneWeb.Data.Entities;

namespace PokeOneWeb.Data.Repositories.Impl.EntityRepositories
{
    public class NatureRepository : HashedEntityRepository<Nature>
    {
        public NatureRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }
    }
}