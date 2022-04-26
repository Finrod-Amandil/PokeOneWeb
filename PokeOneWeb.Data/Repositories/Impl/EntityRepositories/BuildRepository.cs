using PokeOneWeb.Data.Entities;

namespace PokeOneWeb.Data.Repositories.Impl.EntityRepositories
{
    public class BuildRepository : HashedEntityRepository<Build>
    {
        public BuildRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }

        protected override void AddIdsForNames(Build entity)
        {
            entity.PokemonVarietyId = GetRequiredIdForName<PokemonVariety>(entity.PokemonVarietyName);
            entity.AbilityId = GetRequiredIdForName<Ability>(entity.AbilityName);

            entity.NatureOptions.ForEach(natureOption =>
            {
                natureOption.NatureId = GetRequiredIdForName<Nature>(natureOption.NatureName);
            });

            entity.ItemOptions.ForEach(itemOption =>
            {
                itemOption.ItemId = GetRequiredIdForName<Item>(itemOption.ItemName);
            });

            entity.MoveOptions.ForEach(moveOption =>
            {
                moveOption.MoveId = GetRequiredIdForName<Move>(moveOption.MoveName);
            });
        }
    }
}