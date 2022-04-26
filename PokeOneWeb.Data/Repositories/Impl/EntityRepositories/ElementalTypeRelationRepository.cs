using PokeOneWeb.Data.Entities;

namespace PokeOneWeb.Data.Repositories.Impl.EntityRepositories
{
    public class ElementalTypeRelationRepository : HashedEntityRepository<ElementalTypeRelation>
    {
        public ElementalTypeRelationRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }

        protected override void AddIdsForNames(ElementalTypeRelation entity)
        {
            entity.AttackingTypeId = GetRequiredIdForName<ElementalType>(entity.AttackingTypeName);
            entity.DefendingTypeId = GetRequiredIdForName<ElementalType>(entity.DefendingTypeName);
        }
    }
}