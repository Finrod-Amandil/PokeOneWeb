using PokeOneWeb.Data.Entities;

namespace PokeOneWeb.Data.Repositories.Impl.EntityRepositories
{
    public class ChangeLogRepository : HashedEntityRepository<ChangeLog>
    {
        public ChangeLogRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }
    }
}
