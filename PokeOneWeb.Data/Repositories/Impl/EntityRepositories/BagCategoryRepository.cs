using PokeOneWeb.Data.Entities;

namespace PokeOneWeb.Data.Repositories.Impl.EntityRepositories
{
    public class BagCategoryRepository : HashedEntityRepository<BagCategory>
    {
        public BagCategoryRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }
    }
}