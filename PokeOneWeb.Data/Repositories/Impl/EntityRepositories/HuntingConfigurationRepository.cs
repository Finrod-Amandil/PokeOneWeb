using System;
using System.Collections.Generic;
using PokeOneWeb.Data.Entities;

namespace PokeOneWeb.Data.Repositories.Impl.EntityRepositories
{
    public class HuntingConfigurationRepository : HashedEntityRepository<HuntingConfiguration>
    {
        public HuntingConfigurationRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }

        protected override List<Func<HuntingConfiguration, bool>> PreparationSteps => new()
        {
            entity => TrySetIdForName<PokemonVariety>(entity.PokemonVarietyName, id => entity.PokemonVarietyId = id),
            entity => TrySetIdForName<Ability>(entity.AbilityName, id => entity.AbilityId = id),
            entity => TrySetIdForName<Nature>(entity.NatureName, id => entity.NatureId = id),
        };
    }
}