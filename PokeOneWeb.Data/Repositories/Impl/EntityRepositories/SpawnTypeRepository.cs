using PokeOneWeb.Data.Entities;

namespace PokeOneWeb.Data.Repositories.Impl.EntityRepositories
{
    public class SpawnTypeRepository : HashedEntityRepository<SpawnType>
    {
        public SpawnTypeRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }
    }
}