using PokeOneWeb.Data.Entities;

namespace PokeOneWeb.Data.Repositories.Impl.EntityRepositories
{
    public class ElementalTypeRepository : HashedEntityRepository<ElementalType>
    {
        public ElementalTypeRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }
    }
}