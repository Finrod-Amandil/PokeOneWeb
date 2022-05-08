using System;
using System.Collections.Generic;
using PokeOneWeb.Data.Entities;

namespace PokeOneWeb.Data.Repositories.Impl.EntityRepositories
{
    public class ElementalTypeRelationRepository : HashedEntityRepository<ElementalTypeRelation>
    {
        public ElementalTypeRelationRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }

        protected override List<Func<ElementalTypeRelation, bool>> PreparationSteps => new()
        {
            entity => TrySetIdForName<ElementalType>(entity.AttackingTypeName, id => entity.AttackingTypeId = id),
            entity => TrySetIdForName<ElementalType>(entity.DefendingTypeName, id => entity.DefendingTypeId = id),
        };
    }
}