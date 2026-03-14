using System;
using System.Collections.Generic;
using PokeOneWeb.Data.Entities;

namespace PokeOneWeb.Data.Repositories.Impl.EntityRepositories
{
    public class MoveRepository : HashedEntityRepository<Move>
    {
        public MoveRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }

        protected override List<Func<Move, bool>> PreparationSteps => new()
        {
            entity => TrySetIdForName<MoveDamageClass>(entity.DamageClassName, id => entity.DamageClassId = id),
            entity => TrySetIdForName<ElementalType>(entity.ElementalTypeName, id => entity.ElementalTypeId = id),
        };
    }
}