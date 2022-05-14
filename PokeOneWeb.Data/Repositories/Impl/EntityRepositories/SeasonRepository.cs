using PokeOneWeb.Data.Entities;

namespace PokeOneWeb.Data.Repositories.Impl.EntityRepositories
{
    public class SeasonRepository : HashedEntityRepository<Season>
    {
        public SeasonRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }
    }
}