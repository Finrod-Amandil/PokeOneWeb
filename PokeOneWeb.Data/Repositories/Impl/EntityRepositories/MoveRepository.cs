using PokeOneWeb.Data.Entities;

namespace PokeOneWeb.Data.Repositories.Impl.EntityRepositories
{
    public class MoveRepository : HashedEntityRepository<Move>
    {
        public MoveRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }

        protected override void AddIdsForNames(Move entity)
        {
            entity.DamageClassId = GetRequiredIdForName<MoveDamageClass>(entity.DamageClassName);
            entity.ElementalTypeId = GetRequiredIdForName<ElementalType>(entity.ElementalTypeName);
        }
    }
}