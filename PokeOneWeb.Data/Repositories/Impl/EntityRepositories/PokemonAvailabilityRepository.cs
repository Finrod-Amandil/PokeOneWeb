using PokeOneWeb.Data.Entities;

namespace PokeOneWeb.Data.Repositories.Impl.EntityRepositories
{
    public class PokemonAvailabilityRepository : HashedEntityRepository<PokemonAvailability>
    {
        public PokemonAvailabilityRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }
    }
}