using PokeOneWeb.Data.Entities;

namespace PokeOneWeb.Data.Repositories.Impl.EntityRepositories
{
    public class MoveDamageClassRepository : HashedEntityRepository<MoveDamageClass>
    {
        public MoveDamageClassRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }
    }
}