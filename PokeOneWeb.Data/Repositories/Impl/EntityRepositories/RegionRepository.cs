using System;
using System.Collections.Generic;
using PokeOneWeb.Data.Entities;

namespace PokeOneWeb.Data.Repositories.Impl.EntityRepositories
{
    public class RegionRepository : HashedEntityRepository<Region>
    {
        public RegionRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }

        protected override List<Func<Region, bool>> PreparationSteps => new()
        {
            entity =>
            {
                entity.EventId = GetOptionalIdForName<Event>(entity.EventName);
                return true;
            }
        };
    }
}