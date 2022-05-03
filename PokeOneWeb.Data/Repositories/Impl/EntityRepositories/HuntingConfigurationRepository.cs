using PokeOneWeb.Data.Entities;

namespace PokeOneWeb.Data.Repositories.Impl.EntityRepositories
{
    public class HuntingConfigurationRepository : HashedEntityRepository<HuntingConfiguration>
    {
        public HuntingConfigurationRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }

        protected override void PrepareEntitiesForInsertOrUpdate(HuntingConfiguration entity)
        {
            entity.PokemonVarietyId = GetRequiredIdForName<PokemonVariety>(entity.PokemonVarietyName);
            entity.AbilityId = GetRequiredIdForName<Ability>(entity.AbilityName);
            entity.NatureId = GetRequiredIdForName<Nature>(entity.NatureName);
        }
    }
}