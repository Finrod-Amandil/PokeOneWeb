using System;
using System.Collections.Generic;
using PokeOneWeb.Data.Entities;

namespace PokeOneWeb.Data.Repositories.Impl.EntityRepositories
{
    public class EvolutionRepository : HashedEntityRepository<Evolution>
    {
        public EvolutionRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }

        protected override List<Func<Evolution, bool>> PreparationSteps => new()
        {
            entity => TrySetIdForName<PokemonSpecies>(entity.BasePokemonSpeciesName, id => entity.BasePokemonSpeciesId = id),
            entity => TrySetIdForName<PokemonVariety>(entity.BasePokemonVarietyName, id => entity.BasePokemonVarietyId = id),
            entity => TrySetIdForName<PokemonVariety>(entity.EvolvedPokemonVarietyName, id => entity.EvolvedPokemonVarietyId = id),
        };
    }
}