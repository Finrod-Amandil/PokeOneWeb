using PokeOneWeb.Data.Entities;

namespace PokeOneWeb.Data.Repositories.Impl.EntityRepositories
{
    public class AvailabilityRepository : HashedEntityRepository<PokemonAvailability>
    {
        public AvailabilityRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }
    }
}