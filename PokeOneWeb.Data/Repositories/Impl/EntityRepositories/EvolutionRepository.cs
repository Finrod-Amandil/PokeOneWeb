using PokeOneWeb.Data.Entities;

namespace PokeOneWeb.Data.Repositories.Impl.EntityRepositories
{
    public class EvolutionRepository : HashedEntityRepository<Evolution>
    {
        public EvolutionRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }

        protected override void AddIdsForNames(Evolution entity)
        {
            entity.BasePokemonSpeciesId = GetRequiredIdForName<PokemonSpecies>(entity.BasePokemonSpeciesName);
            entity.BasePokemonVarietyId = GetRequiredIdForName<PokemonVariety>(entity.BasePokemonVarietyName);
            entity.EvolvedPokemonVarietyId = GetRequiredIdForName<PokemonVariety>(entity.EvolvedPokemonVarietyName);
        }
    }
}